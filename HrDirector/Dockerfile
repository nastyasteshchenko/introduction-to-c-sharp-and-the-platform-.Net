FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем и восстанавливаем зависимости проекта
COPY ["HrDirector/HrDirector.csproj", "HrDirector/"]
RUN dotnet restore "HrDirector/HrDirector.csproj"

COPY . .
WORKDIR "/src/HrDirector"
RUN dotnet build "HrDirector.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "HrDirector.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Выполняем миграции при запуске контейнера
ENTRYPOINT ["dotnet", "HrDirector.dll"]