FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY ./src/ECinema.Common/ ECinema.Common/ 
COPY ./src/ECinema.Movie.Contracts/ ECinema.Movie.Contracts/ 
COPY ./src/ECinema.MovieHouse/ ECinema.MovieHouse/ 
# Restore as distinct layers
RUN dotnet restore ECinema.MovieHouse/
# Build and publish a release
RUN dotnet publish ECinema.MovieHouse/ -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "ECinema.MovieHouse.dll"]