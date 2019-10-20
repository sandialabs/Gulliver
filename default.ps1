properties {
   $global:config = "debug"

   $tag = $(git tag -l --points-at HEAD)
   $commitHash = $(git rev-parse --short HEAD)

   $base_dir = resolve-path .
   $source_dir = "$base_dir\src"
   $docs_dir = "$base\docs"
   $gulliver_dir = "$source_dir\Gulliver"
   $gulliver_tests_dir = "$source_dir\Gulliver.Tests"
   $docexamples_dir = "$source_dir\Gulliver.DocExamplers"

   $gulliver_sln = "$source_dir\Gulliver.sln"
   $gulliver_csproj = "$gulliver_dir\Gulliver.csproj"
}

task release {
   $global:config = "release"
}

task default -depends local
task local -depends build_src, build_docs, test

task build_src -depends clean {
   echo "building source..."
   echo "config: $config"
   echo "build: Tag is $tag"
   echo "build: CommitHash is $commitHash"
	
   exec { dotnet --version }
   exec { dotnet --info }

   exec { dotnet build $gulliver_sln -c $config }
}

task build_docs -depends clean {
   exec { "$docs_dir\make.bat html" }
}

task test {
   exec { & dotnet test $gulliver_sln -c $config --no-build --no-restore }
}

task clean {
   
   rd "$docs_dir\_build" -recurse -force  -ErrorAction SilentlyContinue | out-null

   rd "$gulliver_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   rd "$gulliver_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null

   rd "$gulliver_tests_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   rd "$gulliver_tests_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null

   rd "$docexamples_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   rd "$docexamples_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null
   
}

