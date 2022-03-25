dotnet pack --configuration Release /p:Version=0.0.4 --output ./nuget
dotnet nuget push nuget/OpenQuery.Core.0.0.4.nupkg -k $NUGET_API_KEY --source https://api.nuget.org/v3/index.json