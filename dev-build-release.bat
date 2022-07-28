dotnet restore

dotnet build --configuration Debug
dotnet build --configuration Release

dotnet test -c Debug .\test\TauCode.Extensions.Tests\TauCode.Extensions.Tests.csproj
dotnet test -c Release .\test\TauCode.Extensions.Tests\TauCode.Extensions.Tests.csproj

nuget pack nuget\TauCode.Extensions.nuspec