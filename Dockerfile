FROM mcr.microsoft.com/dotnet/core-nightly/sdk:3.0.100-preview5-alpine AS build
WORKDIR /app
 
COPY *.csproj ./
RUN dotnet restore
 
COPY . ./
RUN dotnet publish -c Release -o out
 
FROM mcr.microsoft.com/dotnet/core-nightly/aspnet:3.0.0-preview5-alpine AS runtime
 
WORKDIR /app
 
COPY --from=build /app/out .
 
ENTRYPOINT ["dotnet","Ads.Api.dll"]