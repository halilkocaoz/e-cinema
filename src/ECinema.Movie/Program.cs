using ECinema.Common;
using ECinema.Movie.Application.Movies.Commands.Create;
using ECinema.Movie.Application.Movies.Commands.Update;
using ECinema.Movie.Data;
using ECinema.Movie.Models.Movies;
using MassTransit;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonSwagger();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMongoDbSettings(builder.Configuration);
builder.Services.AddSingleton<IMovieRepository, MovieRepository>();

builder.Services.AddMassTransit(x => { x.UsingRabbitMq((_, cfg) => { cfg.Host("rabbitmq", "/"); }); });

var app = builder.Build();
app.UseMiddleware<ExceptionsMiddleware>();
app.UseCommonSwagger();

app.MapPost("/movies", async (UpsertMovieModel createMovieRequest, ISender sender) =>
    {
        var command = new CreateMovieCommand(createMovieRequest.Name, createMovieRequest.Base64Poster,
            createMovieRequest.Cast, createMovieRequest.Genres);
        var commandResult = await sender.Send(command);
        return Results.Created();
    })
    .WithName("CreateMovie")
    .WithOpenApi();

app.MapPut("/movies/{id}", async (string id, UpsertMovieModel updateMovieRequest, ISender sender) =>
    {
        var command = new UpdateMovieCommand(id, updateMovieRequest.Name, updateMovieRequest.Base64Poster,
            updateMovieRequest.Cast, updateMovieRequest.Genres);
        var commandResult = await sender.Send(command);
        return Results.Ok();
    })
    .WithName("UpdateMovie")
    .WithOpenApi();

app.Run();