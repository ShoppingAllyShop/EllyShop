#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ContentManagement.API/ContentManagement.API.csproj", "ContentManagement.API/"]
COPY ["Comman.Domain/Comman.Domain.csproj", "Comman.Domain/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
COPY ["CommonLib.Constants/CommonLib.Constants.csproj", "CommonLib.Constants/"]
COPY ["CommonLib.Exceptions/CommonLib.Exceptions.csproj", "CommonLib.Exceptions/"]
COPY ["CommonLib.Helpers/CommonLib.Helpers.csproj", "CommonLib.Helpers/"]
COPY ["CommonLib.Models/CommonLib.Models.csproj", "CommonLib.Models/"]
COPY ["CommonLib.TestHelpers/CommonLib.TestHelpers.csproj", "CommonLib.TestHelpers/"]
RUN dotnet restore "./ContentManagement.API/ContentManagement.API.csproj"
COPY . .
WORKDIR "/src/ContentManagement.API"
RUN dotnet build "./ContentManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ContentManagement.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentManagement.API.dll"]