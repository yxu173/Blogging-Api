FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BloggingApi/BloggingApi.csproj", "BloggingApi/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "BloggingApi/BloggingApi.csproj"
COPY . .
WORKDIR "/src/BloggingApi"
RUN dotnet build "BloggingApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BloggingApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Check if wwwroot exists and copy if it does
RUN if [ -d "/src/BloggingApi/wwwroot" ]; then cp -R /src/BloggingApi/wwwroot ./wwwroot; fi
ENTRYPOINT ["dotnet", "BloggingApi.dll"]