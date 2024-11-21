FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем и восстанавливаем зависимости проекта
COPY ["Developer/Developer.csproj", "Developer/"]
RUN dotnet restore "Developer/Developer.csproj"

COPY . .
WORKDIR "/src/Developer"
RUN dotnet build "Developer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "Developer.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Выполняем миграции при запуске контейнера
ENTRYPOINT ["dotnet", "Developer.dll"]