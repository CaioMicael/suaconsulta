# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore
COPY . .
RUN dotnet restore suaconsulta-api.csproj

# Build and publish
RUN dotnet publish suaconsulta-api.csproj -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:7156
EXPOSE 7156

ENTRYPOINT ["dotnet", "suaconsulta-api.dll"]