FROM mcr.microsoft.com/dotnet/core/sdk:2.2.300-alpine3.9 AS build
WORKDIR /app
 
COPY *.csproj ./
RUN dotnet restore
 
COPY . ./
RUN dotnet publish -c Release -o out
 
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2.5-alpine3.9 AS runtime
 
WORKDIR /app
 
COPY --from=build /app/out .
 
ENTRYPOINT ["dotnet","Ads.Api.dll"]