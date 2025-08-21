var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var produtos = new List<Produto>
{
    new Produto { Id = 1, Nome = "Produto A", Preco = 10.0m, Estoque = 100 },
    new Produto { Id = 2, Nome = "Produto B", Preco = 20.0m, Estoque = 200 }   
};

#region endpoints
app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/test", () => "Esse é um endpoint de teste");



app.MapGet("/produtos", () =>
{
    return produtos;
});

app.MapGet("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);
    return produto == null ? Results.NotFound() : Results.Ok(produto);

});

app.MapPost("/produtos", (Produto produto) =>
{
    var NovoProduto = new Produto
    {
        Id = produtos.Max(p => p.Id) + 1,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Estoque = produto.Estoque
    };

    produtos.Add(NovoProduto);
    return Results.Created($"/produtos/{NovoProduto.Id}", NovoProduto);
}); 


#endregion

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}
