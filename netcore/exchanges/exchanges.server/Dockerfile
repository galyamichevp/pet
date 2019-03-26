# The FROM instruction initializes a new build stage and sets the Base Image for the next instructions we’ll be running in the DockerFile
FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app
# copy csproj and restore as distinct layers
COPY *.csproj ./
COPY exchanges.server.xml ./
RUN dotnet restore
# copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
# build runtime image
FROM microsoft/dotnet:2.2-sdk
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80/tcp
# Configures the container to run as an executable.
ENTRYPOINT ["dotnet", "exchanges.server.dll"]