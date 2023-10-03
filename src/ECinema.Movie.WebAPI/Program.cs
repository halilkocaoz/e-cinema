using ECinema.Common;
using ECinema.WebAPI.Application.Movies.Commands.Create;
using ECinema.WebAPI.Application.Movies.Commands.Update;
using ECinema.WebAPI.Models.Movies;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommonSwagger();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMongoDbSettings(builder.Configuration);

var app = builder.Build();
app.UseCommonSwagger();
app.UseHttpsRedirection();

app.MapPost("/movies", async (CreateMovieModel createMovieRequest, ISender sender) =>
    {
        var command = new CreateMovieCommand(createMovieRequest.Name, createMovieRequest.Base64Poster,
            createMovieRequest.Cast);
        var result = await sender.Send(command);
        return result;
    })
    .WithName("CreateMovie")
    .WithOpenApi();

app.MapPut("/movies/{id}", async (Guid id, UpdateMovieModel updateMovieRequest, ISender sender) =>
    {
        var command = new UpdateMovieCommand(id, updateMovieRequest.Name, updateMovieRequest.Base64Poster,
            updateMovieRequest.Cast);
        var result = await sender.Send(command);
        return result;
    })
    .WithName("UpdateMovie")
    .WithOpenApi();

app.Run();
