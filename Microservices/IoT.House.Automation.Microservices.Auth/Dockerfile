# Get SDK Image from Microsoft Image repository
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build-env
WORKDIR /app

# Copy csproj and restore nuget
COPY IoT.House.Automation.Microservices.Auth.Api/*.csproj ./
RUN dotnet restore

#Copy and build project files
COPY . ./
RUN dotnet publish IoT.House.Automation.Microservices.Auth.sln -c Release -o out

#Generate Image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/IoT.House.Automation.Microservices.Auth.Api/out/ .
ENTRYPOINT [ "dotnet", "IoT.House.Automation.Microservices.Auth.Api.dll" ]

