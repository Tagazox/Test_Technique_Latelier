BackEnd test for l'Atelier

Pour lancer le projet, lancer les commandes qui suit à l'aide d'une cmd à la racine du projet :
> dotnet restore NetCoreTests.sln

> dotnet build --no-restore  NetCoreTests.sln

> cd NetCoreTests

> dotnet run

Ensuite se rendre sur l'url :http://localhost:5067/swagger/index.html pour connaitre l'accès aux différents endpoints

Pour lancer les projets de tests lancer les commandes à l'aide d'une cmd a la racine du projet :
> dotnet test --no-build --verbosity normal NetCoreTests.Queries.Tests/NetCoreTests.Queries.Tests.csproj

> dotnet test --no-build --verbosity normal NetCoreTests.Data.Acess.Tests/NetCoreTests.Data.Acess.Tests.csproj

