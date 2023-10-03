using ECinema.Common;
using ECinema.MovieHouse.WebAPI.Commands.MovieHouses.Create;
using ECinema.MovieHouse.WebAPI.Models.MovieHouses;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCommonSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();
app.UseCommonSwagger();

app.UseHttpsRedirection();

app.MapPost("/movieHouses", async (CreateMovieHouseModel createMovieHouseModel, ISender sender) =>
    {
        var command = new CreateMovieHouseCommand(createMovieHouseModel.Name, createMovieHouseModel.MovieGenres);
        var result = await sender.Send(command);
        return result;
    })
    .WithName("CreateMovieHouse")
    .WithOpenApi();

app.Run();
