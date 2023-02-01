@@ -0,0 +1,19 @@
#!/bin/bash

echo 'start to delete old packages...'

rm -rf ./nupkgs

echo 'old packages are deleted.'

echo 'start to pack nuget packages...'

dotnet pack --output nupkgs

echo 'package built.'

dotnet nuget push ./nupkgs/*.nupkg -k $NUGET_AUTH_KEY_GARRETT -s http://nuget.garrettmotion.io/v3/index.json

rm -rf ./nupkgs

echo 'package pushed and cleaned up.'
