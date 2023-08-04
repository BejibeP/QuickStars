# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY . .

# All in one packaging of the dotnet api (Restore Nuget, Build and Publish)
RUN dotnet publish "MVApi/MVApi.csproj" -c Release -o /app/publish

# Alpine Args : 
# --runtime alpine-x64 --self-contained true /p:PublishTrimmed=true /p:TrimMode=Link /p:PublishSingleFile=true

#RUN dotnet dev-certs https --trust

# Runtime stage (using ASPNET Runtime)
# For smaller image : use alpine based image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Alpine Globalization fix
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Runtime Environnment
ENV ASPNETCORE_ENVIRONMENT Development

# Optionnal Environnment variable : for Parent : { Children: value } use Parent__Children=value (__ for :)
# ENV Parent__Children=value

# Set the port on which the app will run (HTTPS only) #https://*:6512
ENV ASPNETCORE_URLS=http://*:6512
EXPOSE 6512

ENTRYPOINT ["dotnet", "QuickStars.MVApi.dll"]

# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# See https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?tabs=basicconfiguration&view=aspnetcore-6.0#non-prefixed-environment-variables for configuration
