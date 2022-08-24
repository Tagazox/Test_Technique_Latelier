FROM centos:7 AS base
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm \
    && yum install -y aspnetcore-runtime-6.0

# Ensure we listen on any IP Address 
#ENV ASPNETCORE_URLS=http://*:${PORT}
ENV ASPNETCORE_ENVIRONMENT="production"
WORKDIR /app

# ... remainder of dockerfile as before
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NetCoreTests/data.json" , "./"]
COPY ["NetCoreTests/NetCoreTests.csproj" , "NetCoreTests/"]
COPY ["NetCoreTests.API.Common/NetCoreTests.API.Common.csproj" , "NetCoreTests.API.Common/"]
COPY ["NetCoreTests.Data.Acess/NetCoreTests.Data.Acess.csproj" , "NetCoreTests.Data.Acess/"]
COPY ["NetCoreTests.Data.Model/NetCoreTests.Data.Model.csproj" , "NetCoreTests.Data.Model/"]
COPY ["NetCoreTests.Queries/NetCoreTests.Queries.csproj" , "NetCoreTests.Queries/"]
RUN dotnet restore "NetCoreTests/NetCoreTests.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NetCoreTests/NetCoreTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreTests/NetCoreTests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreTests.dll"]
#CMD DOTNET_URLS=http://*:$PORT dotnet HerokuApp.dll


