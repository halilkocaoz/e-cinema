using ECinema.Common;
using ECinema.MovieHouse.Application.Commands.MovieHouses.Create;
using ECinema.MovieHouse.Application.Messaging.Consumers.Movie;
using ECinema.Movie.Contracts.Messaging.Movie;
using ECinema.MovieHouse.Data;
using ECinema.MovieHouse.Models.MovieHouses;
using MassTransit;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCommonSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMongoDbSettings(builder.Configuration);
builder.Services.AddSingleton<IMovieHouseRepository, MovieHouseRepository>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MovieCreatedConsumer>();
    x.AddConsumer<MovieUpdatedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint(MovieCreatedMessage.MessageName,
            e => { e.ConfigureConsumer<MovieCreatedConsumer>(context); });

        cfg.ReceiveEndpoint(MovieUpdatedMessage.MessageName,
            e => { e.ConfigureConsumer<MovieUpdatedConsumer>(context); });
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionsMiddleware>();

app.UseCommonSwagger();

app.MapPost("/movieHouses", async (CreateMovieHouseModel createMovieHouseModel, ISender sender) =>
    {
        var command = new CreateMovieHouseCommand(createMovieHouseModel.Name, createMovieHouseModel.InterestedGenres,
            createMovieHouseModel.WillBeInformedEmails);
        var commandResult = await sender.Send(command);
        return Results.Created();
    })
    .WithName("CreateMovieHouse")
    .WithOpenApi();

app.Run();