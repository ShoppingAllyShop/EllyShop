#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["FrontEnd/FrontEnd.csproj", "FrontEnd/"]
#RUN dotnet restore "./FrontEnd/FrontEnd.csproj"
#COPY . .
#WORKDIR "/src/FrontEnd"
#RUN dotnet build "./FrontEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./FrontEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "FrontEnd.dll"]

# 1. For build React app
FROM node:lts AS development
# Set working directory
WORKDIR /app
#

COPY ["FrontEnd/source/package.json" , "/app/package.json"]
COPY ["FrontEnd/source/package-lock.json" , "/app/package-lock.json"]
COPY ["FrontEnd/source/public" , "/app/public"]
COPY ["FrontEnd/source/src" , "/app/src"]
COPY ["FrontEnd/source/.nginx/nginx.conf" , "/app/.nginx/nginx.conf"]
#RUN npm install -g npm@10.9.0

# Same as npm install
#RUN npm install -g npm@10.9.0
RUN npm ci --legacy-peer-deps
COPY . /app	
ENV PORT=3000
CMD [ "npm", "start" ]
FROM development AS build
WORKDIR /app
RUN npm install --legacy-peer-deps

#Set env variables
ENV REACT_APP_BASE_API_URL=${REACT_APP_BASE_API_URL}

RUN npm run build
# 2. For Nginx setup
FROM nginx:alpine
# Copy config nginx
COPY --from=build /app/.nginx/nginx.conf /etc/nginx/conf.d/default.conf
WORKDIR /usr/share/nginx/html
# Remove default nginx static assets
RUN rm -rf ./*
# Copy static assets from builder stage
COPY --from=build /app/build .
# Containers run nginx with global directives and daemon off
ENTRYPOINT ["nginx", "-g", "daemon off;"]