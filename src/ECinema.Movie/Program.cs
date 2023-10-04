using ECinema.Common;
using ECinema.Movie.Application.Movies.Commands.Create;
using ECinema.Movie.Application.Movies.Commands.Update;
using ECinema.Movie.Data;
using ECinema.Movie.Models.Movies;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonSwagger();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMongoDbSettings(builder.Configuration);
builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
var app = builder.Build();
app.UseCommonSwagger();

app.MapPost("/movies", async (CreateMovieModel createMovieRequest, ISender sender) =>
    {
        var command = new CreateMovieCommand(createMovieRequest.Name, createMovieRequest.Base64Poster,
            createMovieRequest.Cast);
        var result = await sender.Send(command);
        return result;
    })
    .WithName("CreateMovie")
    .WithOpenApi();

app.MapPut("/movies/{id}", async (string id, UpdateMovieModel updateMovieRequest, ISender sender) =>
    {
        var command = new UpdateMovieCommand(id, updateMovieRequest.Name, updateMovieRequest.Base64Poster,
            updateMovieRequest.Cast);
        var result = await sender.Send(command);
        return result;
    })
    .WithName("UpdateMovie")
    .WithOpenApi();

app.Run();
