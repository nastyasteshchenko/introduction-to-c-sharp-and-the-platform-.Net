FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем и восстанавливаем зависимости проекта
COPY ["HrManager/HrManager.csproj", "HrManager/"]
RUN dotnet restore "HrManager/HrManager.csproj"

COPY . .
WORKDIR "/src/HrManager"
RUN dotnet build "HrManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HrManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Выполняем миграции при запуске контейнера
ENTRYPOINT ["dotnet", "HrManager.dll"]