FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
WORKDIR /src
COPY . .
RUN dotnet publish "TeleAppBot.sln" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish . 
RUN export ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT [ "dotnet", "TeleAppBot.Bot.dll" ]