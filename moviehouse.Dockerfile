FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY ./src/ECinema.Common/ ECinema.Common/ 
COPY ./src/ECinema.MovieHouse.Contracts/ ECinema.MovieHouse.Contracts/ 
COPY ./src/ECinema.MovieHouse.Service/ ECinema.MovieHouse.Service/ 
# Restore as distinct layers
RUN dotnet restore ECinema.MovieHouse.Service/
# Build and publish a release
RUN dotnet publish ECinema.MovieHouse.Service/ -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "ECinema.MovieHouse.Service.dll"]