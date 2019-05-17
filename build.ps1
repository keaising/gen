$ResultPath = 'pakcages'
$exePath = 'bin/Release/gen' 
$nugetPath = 'bin/Release'
$genExe = 'gen.csproj'
$genNuget = 'gen-nuget.csproj'

$xml = [Xml] (Get-Content $genExe)
$version = [Version] $xml.Project.PropertyGroup.Version
$zip = 'gen-v' + $version + '.zip'
$genExeName = 'gen.' + $version + '.exe'
$genNugetName = 'gen.' +$version + '.nupkg'

write '>>> 0. clean and restore <<<'
dotnet clean -o $exePath $genExe
rm 'obj' -Force -Recurse
dotnet restore $genExe

write '>>> 1. dotnet publish <<<'
dotnet publish -r win10-x64 --self-contained true -c Release -o $exePath $genExe

write '>>> 2. warp-packer package <<<'
warp-packer --arch windows-x64 --input_dir $exePath --exec gen.exe --output ($nugetPath + '/' + $genExeName)


write '>>> 3. dotnet pack <<<'
rm 'obj' -Force -Recurse
dotnet restore $genNuget
dotnet pack --output $nugetPath -c Release $genNuget

write '>>> 4. all files => zip <<<'

mkdir $ResultPath -ErrorAction SilentlyContinue
Compress-Archive -Path ($nugetPath + '/' + $genExeName), ($nugetPath + '/' + $genNugetName) -DestinationPath ($ResultPath + '/' + $zip) -Force

rm -Force -Recurse $nugetPath -ErrorAction SilentlyContinue

$finalName = ($ResultPath + '\' + $zip)
write ">>> 5. success! you can find zip file in $($ResultPath + '\' + $zip) <<<"