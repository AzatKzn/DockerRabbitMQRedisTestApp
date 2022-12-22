FROM busybox AS base
WORKDIR /App
COPY .. .
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
#
#WORKDIR /App
## Copy everything
#COPY .. .
## Restore as distinct layers
#RUN dotnet restore
## Build and publish a release
#RUN dotnet build -c Release -o build
#RUN dotnet publish -c Release -o out
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/runtime:5.0
#WORKDIR /App
#COPY --from=build-env /App/out .
##RUN nohup dotnet RabbitMQPublisher.dll
#ENTRYPOINT ["dotnet", "RabbitMQPublisher.dll"]

#FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
##COPY ./RabbitMQPublisher.csproj RabbitMQPublisher/
#COPY ["./RabbitMQPublisher.csproj", "RabbitMQPublisher/RabbitMQPublisher.csproj"]
#COPY . .
#WORKDIR "/src/DockerRabbitMQRedisTestApp"
#RUN dotnet build "RabbitMQPublisher.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "RabbitMQPublisher.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "RabbitMQPublisher.dll"]
