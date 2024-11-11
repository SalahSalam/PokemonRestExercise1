using PokemonLib;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  //policy.WithAnyOrigin(WWW.).AllowAnyMethod().AllowAnyHeader();

                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddSingleton<PokemonsRepository>(new PokemonsRepository());



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();