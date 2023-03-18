FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
COPY . /src
WORKDIR /src
RUN 
RUN dotnet publish "TeleAppApi.sln" -c Release -o /app/publish 
COPY TeleAppBot.WorkerEnviarMensagem/appsettings.json /app/publish/appsettings.json
COPY TeleAppBot.WorkerEnviarMensagem/appsettings.Development.json /app/publish/appsettings.Development.json

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish /app
RUN export ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT [ "dotnet", "TeleAppApi.WorkerEnviarMensagem.dll" ]