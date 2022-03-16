dotnet pack --configuration Release /p:Version=0.0.2 --output ./nuget
dotnet nuget push nuget/OpenQuery.Core.0.0.2.nupkg -k $NUGET_API_KEY --source https://api.nuget.org/v3/index.json