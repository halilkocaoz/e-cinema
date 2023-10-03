FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY ./src/ECinema.Common/ ECinema.Common/ 
COPY ./src/ECinema.Movie.WebAPI/ ECinema.Movie.WebAPI/ 
# Restore as distinct layers
RUN dotnet restore ECinema.Movie.WebAPI/ 
# Build and publish a release
RUN dotnet publish ECinema.Movie.WebAPI/ -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "ECinema.Movie.WebAPI.dll"]