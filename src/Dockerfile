FROM node:lts-alpine3.19 AS node_base
FROM mcr.microsoft.com/dotnet/aspnet:6.0.27-alpine3.19 AS base
COPY --from=node_base . . 
# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:current-alpine3.19 AS node_build
FROM mcr.microsoft.com/dotnet/sdk:6.0.419-alpine3.19 AS build
COPY --from=node_base . . 
# RUN apk add openssl ca-certificates
WORKDIR /src
COPY . .
RUN dotnet restore FrontendHost/FrontEndHost.csproj
COPY . .
# WORKDIR "/src/FrontendHostSpa"
RUN dotnet build "FrontendHost/FrontEndHost.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "FrontendHost/FrontEndHost.csproj" -c Release -o /app/publish
# COPY slnPoc-Bff-React/. ./slnPoc-Bff-React
# WORKDIR /source/slnPoc-Bff-React/
# RUN dotnet publish FrontendHost/FrontEndHost.csproj -c Release -o /app --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./

ENTRYPOINT ["dotnet", "FrontEndHost.dll"]
