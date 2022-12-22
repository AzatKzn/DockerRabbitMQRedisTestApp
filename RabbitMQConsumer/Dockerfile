FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /App

# Copy everything
COPY . .

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:5.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "RabbitMQConsumer.dll"]

#FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
##COPY ./RabbitMQConsumer.csproj RabbitMQConsumer/RabbitMQConsumer.csproj
#COPY ["./RabbitMQConsumer.csproj", "RabbitMQConsumer/RabbitMQConsumer.csproj"]
#COPY . .
#WORKDIR "/src/RabbitMQConsumer"
#RUN dotnet build "RabbitMQConsumer.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "RabbitMQConsumer.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "RabbitMQConsumer.dll"]