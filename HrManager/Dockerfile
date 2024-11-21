FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY HrManager/HrManager.csproj ./
RUN dotnet restore

COPY HrManager/ ./
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "HrManager.dll"]
