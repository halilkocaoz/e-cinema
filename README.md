# e-cinema

[![Movie Service](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieService.yml/badge.svg)](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieService.yml)

[![Movie House Service](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieHouseService.yml/badge.svg)](https://github.com/halilkocaoz/e-cinema/actions/workflows/MovieHouseService.yml)

For practicing microservice basics.

## Installation & Run

* `git clone https://github.com/halilkocaoz/e-cinema/`
* `cd e-cinema && docker-compose up --build`

## Techs & Programming Approaches

* .NET 8
* RabbitMQ
* MongoDB
* Docker
* MediatR
* RESTful web services
* Domain Driven Design
* CQRS
* SOLID
  
## Use cases

* Movie service POST /movies > inserts new movie to MongoDB, and publishing the [MovieCreatedEvent](/src/ECinema.Movie/Application/Movies/Events/MovieCreatedEvent.cs). MediatR handles the event and sends the new movie to RabbitMQ Queue.
* Movie service PUT /movies/{id} > updates exist movie, and publishes the [MovieUpdatedEvent](/src/ECinema.Movie/Application/Movies/Events/MovieUpdatedEvent.cs). MediatR handles the event and sends the updated movie to RabbitMQ Queue.
* Movie House service consumes the two queue and inform the movie houses.

![arch](/assets/e-cinema.jpg)
