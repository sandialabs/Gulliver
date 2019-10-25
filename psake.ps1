properties {
   
   $global:config = "debug" # default to debug

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

# Default

task default -Depends build_debug

# Variable setting

task release -Depends build_src -Description "config overidden to release" {
   $global:config = "release" # override to release
}

# C# build, testing, and release
task clean {
   Remove-Item "$gulliver_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   Remove-Item "$gulliver_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null

   Remove-Item "$gulliver_tests_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   Remove-Item "$gulliver_tests_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null

   Remove-Item "$docexamples_dir\bin" -recurse -force  -ErrorAction SilentlyContinue | out-null
   Remove-Item "$docexamples_dir\obj" -recurse -force  -ErrorAction SilentlyContinue | out-null
}

task build_src -Depends clean -Description "Clean" {

   Write-Output "building $config..."
   Write-Output "Tag: $tag"
   Write-Output "CommitHash: $commitHash"
	
   exec { dotnet --version }
   exec { dotnet --info }

   exec { & dotnet build $gulliver_sln -c $config }
}

task test -Description "Unit Tests" {
   exec { & dotnet test $gulliver_sln -c $config --no-build --no-restore }
}

task pack -Depends clean -Description "create nuget packages" {
   Write-Output "packaging $config..."
   Write-Output "Tag: $tag"
   Write-Output "CommitHash: $commitHash"

   exec { & dotnet pack $gulliver_csproj -c $config --include-symbols --include-source --verbosity m }
}

task pack_debug -Depends clean, pack -Description "Package Debug"
task pack_release -Depends release, clean, pack -Description "Package Release"

task build_debug -Depends build_src, test -Description "Build Debug"
task build_release -Depends release, build_src -Description "Build Release (no tests run)"
   
# Documentation

task clean_docs -Description "Clean Sphnix Docs" {
   Remove-Item "$docs_dir\_build" -recurse -force  -ErrorAction SilentlyContinue | out-null
}

task build_docs -Depends clean_docs -Description "Build Sphnix Docs" {
   exec { & cmd.exe /c $docs_dir/make.bat html }
}