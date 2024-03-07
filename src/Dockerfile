FROM node:lts-alpine3.19 AS node_base
FROM mcr.microsoft.com/dotnet/aspnet:6.0.27-alpine3.19 AS base
COPY --from=node_base . . 
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 80
EXPOSE 443

FROM node:current-alpine3.19 AS node_build
FROM mcr.microsoft.com/dotnet/sdk:6.0.419-alpine3.19 AS build
COPY --from=node_base . . 
WORKDIR /src
COPY . .
RUN dotnet restore FrontendHost/FrontEndHost.csproj
COPY . .
RUN dotnet build "FrontendHost/FrontEndHost.csproj" -r linux-musl-arm64 -c Release -o /app/build

FROM build as publish
RUN dotnet publish "FrontendHost/FrontEndHost.csproj" -r linux-musl-arm64 -c Release  -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./

ENTRYPOINT ["dotnet", "FrontEndHost.dll"]
