FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["EcoreNetto.Website", "EcoreNetto.Website/"]

RUN dotnet restore "EcoreNetto.Website/EcoreNetto.Website.csproj"

WORKDIR "/src/EcoreNetto.Website"
RUN dotnet build "EcoreNetto.Website.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "EcoreNetto.Website.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0.10-alpine3.20 AS final
WORKDIR /app
RUN mkdir /app/logs
COPY --from=publish /app/publish .

# Create a non-root user and give this user access to the working directory
RUN chown -R "$APP_UID" /app
USER $APP_UID 

ENTRYPOINT ["dotnet", "EcoreNetto.Website.dll"]