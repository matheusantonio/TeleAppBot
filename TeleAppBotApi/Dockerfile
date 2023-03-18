FROM mcr.microsoft.com/dotnet/sdk:7.0 AS publish
COPY . /src
WORKDIR /src
RUN dotnet publish "TeleAppApi.sln" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
EXPOSE 2711
EXPOSE 2712
EXPOSE 80
COPY --from=publish /app/publish /app
RUN export ASPNETCORE_ENVIRONMENT=Docker
RUN dotnet dev-certs https
RUN \ 
  apt update && \
  apt install -y ca-certificates && \
  update-ca-certificates
ENTRYPOINT [ "dotnet", "TeleAppApi.Api.dll" ]