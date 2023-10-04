using ECinema.Common;
using ECinema.MovieHouse.WebAPI.Application.Commands.MovieHouses.Create;
using ECinema.MovieHouse.WebAPI.Models.MovieHouses;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCommonSwagger();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddMongoDbSettings(builder.Configuration);

var app = builder.Build();
app.UseCommonSwagger();


app.MapPost("/movieHouses", async (CreateMovieHouseModel createMovieHouseModel, ISender sender) =>
    {
        var command = new CreateMovieHouseCommand(createMovieHouseModel.Name, createMovieHouseModel.MovieGenres);
        var result = await sender.Send(command);
        return result;
    })
    .WithName("CreateMovieHouse")
    .WithOpenApi();

app.Run();
