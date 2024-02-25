FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Locky.Keyholder.Api/Locky.Keyholder.Api.csproj", "Locky.Keyholder.Api/"]
RUN dotnet restore "Locky.Keyholder.Api/Locky.Keyholder.Api.csproj"
COPY . .
WORKDIR "/src/Locky.Keyholder.Api"
RUN dotnet build "Locky.Keyholder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Locky.Keyholder.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Locky.Keyholder.Api.dll"]
