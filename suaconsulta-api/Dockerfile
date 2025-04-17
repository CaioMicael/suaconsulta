# Imagem base para construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos de projeto e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o código fonte
COPY . ./

# Publicar a aplicação
RUN dotnet publish -c Release -o out

# Imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Definir variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Expor porta
EXPOSE 7156

# Comando de inicialização
ENTRYPOINT ["dotnet", "suaconsulta-api.dll"]