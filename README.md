# e-cinema

[![Movie Service](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieService.yml/badge.svg)](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieService.yml)

[![Movie House Service](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieHouseService.yml/badge.svg)](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieHouseService.yml)

For practicing microservice basics.

## Installation & Run

* `git clone https://github.com/halilkocaoz/e-cinema/`
* `cd e-cinema && docker pull  && docker-compose up --build`

[Movie service: http://localhost:9990](http://localhost:9990/swagger/index.html)

[Movie house service: http://localhost:9991](http://localhost:9991/swagger/index.html)

## Techs & Programming Approaches

* .NET 8
* RabbitMQ
* MongoDB
* Docker
* Mediator pattern with MediatR
* RESTful web services
* Domain Driven Design
* SOLID
  
## Use cases

* Movie service POST /movies > inserts new movie to MongoDB, and publishing the [MovieCreatedEvent](/src/ECinema.Movie/Application/Movies/Events/MovieCreatedEvent.cs). MediatR handles the event and sends the new movie to RabbitMQ Queue.
* Movie service PUT /movies/{id} > updates exist movie, and publishes the [MovieUpdatedEvent](/src/ECinema.Movie/Application/Movies/Events/MovieUpdatedEvent.cs). MediatR handles the event and sends the updated movie to RabbitMQ Queue.
* Movie House service consumes the two queue and inform the movie houses.

![e-cinema](/assets/e-cinema.jpg)

References:

[https://github.com/gizemcifguvercin/E-News](https://github.com/gizemcifguvercin/E-News)

[https://github.com/dotnet-architecture/eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)
