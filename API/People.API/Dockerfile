FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src
COPY ["People.API.csproj", "People.API/"]
RUN dotnet restore "People.API/People.API.csproj" -v q
COPY . .
WORKDIR "/src/People.API"
RUN dotnet publish "People.API.csproj" -c Release -o /app --no-restore --nologo -v q  /clp:"ErrorsOnly"

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app .
#ENV ASPNETCORE_URLS=http://+:80,https://+:443
#ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "People.API.dll"]