FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WeatherApp.WebAPI/WeatherApp.WebAPI.csproj", "WeatherApp.WebAPI/"]
RUN dotnet restore "WeatherApp.WebAPI/WeatherApp.WebAPI.csproj"
COPY . .
WORKDIR "/src/WeatherApp.WebAPI"
RUN dotnet build "WeatherApp.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherApp.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherApp.WebAPI.dll"]
