using ECinema.Common;
using ECinema.MovieHouse.Application.Commands.MovieHouses.Create;
using ECinema.MovieHouse.Application.Messaging.Consumers.Movie;
using ECinema.MovieHouse.Contracts.Messaging.Movie;
using ECinema.MovieHouse.Models.MovieHouses;
using MassTransit;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCommonSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMongoDbSettings(builder.Configuration);

var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
{
    cfg.Host("rabbitmq", "/");
    cfg.ReceiveEndpoint(MovieCreatedMessage.MessageName, e => { e.Consumer<MovieCreatedConsumer>(); });
});

var app = builder.Build();
app.UseCommonSwagger();

await busControl.StartAsync();
app.MapPost("/movieHouses", async (CreateMovieHouseModel createMovieHouseModel, ISender sender) =>
    {
        var command = new CreateMovieHouseCommand(createMovieHouseModel.Name, createMovieHouseModel.MovieGenres);
        var result = await sender.Send(command);
        return result;
    })
    .WithName("CreateMovieHouse")
    .WithOpenApi();

app.Run();