rem pushes one file to the NuGet feed
rem nuget.exe push -Source http://nuget.taucode.com -ApiKey 4adb822f9e46446f42dfa06457ee6be6 %1

dotnet nuget push %1 -k oy2fswonuoa7nsfv5hoa3cvgndpx555urfmvfsbun4rf6m -s https://api.nuget.org/v3/index.json