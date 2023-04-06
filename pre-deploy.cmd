dotnet restore

dotnet build TauCode.Extensions.sln -c Debug
dotnet build TauCode.Extensions.sln -c Release

dotnet test TauCode.Extensions.sln -c Debug
dotnet test TauCode.Extensions.sln -c Release

nuget pack nuget\TauCode.Extensions.nuspec