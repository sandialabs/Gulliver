properties {
   $global:config = "debug"

   $tag = $(git tag -l --points-at HEAD)
   $commitHash = $(git rev-parse --short HEAD)

   $base_dir = resolve-path .
   $source_dir = "$base_dir\src"
   $docs_dir = "$base_dir\docs"
   $gulliver_dir = "$source_dir\Gulliver"
   $gulliver_tests_dir = "$source_dir\Gulliver.Tests"
   $docexamples_dir = "$source_dir\Gulliver.DocExamplers"

   $gulliver_sln = "$source_dir\Gulliver.sln"
   $gulliver_csproj = "$gulliver_dir\Gulliver.csproj"
}

task release -depends build_src {
   $global:config = "release"
}

task default -depends build_debug

task build_release -depends release, build_src, build_docs

task build_debug -depends build_src, build_docs, test

task build_src -depends clean {
   echo "building $config..."
   echo "Tag: $tag"
   echo "CommitHash: $commitHash"
	
   exec { dotnet --version }
   exec { dotnet --info }

   exec { dotnet build $gulliver_sln -c $config }
}

task build_docs -depends clean_docs {

   exec { cmd.exe /c $docs_dir/make.bat html }
   
}

task test {
   exec { & dotnet test $gulliver_sln -c $config --no-build --no-restore }
}

task clean_docs {
   rd "$docs_dir\_build" -recurse -force  -ErrorAction SilentlyContinue | out-null
}

task clean {
   rd "$gulliver_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   rd "$gulliver_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null

   rd "$gulliver_tests_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   rd "$gulliver_tests_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null

   rd "$docexamples_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   rd "$docexamples_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null
}

task pack -depends release, clean {
   echo "releasing $config..."
   echo "Tag: $tag"
   echo "CommitHash: $commitHash"

   dotnet pack $gulliver_csproj -c $config --no-restore --include-symbols --include-source --verbosity m
}