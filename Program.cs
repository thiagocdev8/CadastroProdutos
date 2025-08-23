var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

var produtos = new List<Produto>
{
    new Produto { Id = 1, Nome = "Produto A", Preco = 10.0m, Estoque = 100 },
    new Produto { Id = 2, Nome = "Produto B", Preco = 20.0m, Estoque = 200 }   
};


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


//listar todos os produtos
app.MapGet("/produtos", () =>
{
    return produtos;
});

//buscar produto por id
app.MapGet("/produtos/{id}", (int id) =>
{
    var produto = produtos.FirstOrDefault(p => p.Id == id);
    return produto == null ? Results.NotFound() : Results.Ok(produto);

});

//criar novo produto
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

app.MapPut("/produtos/{id}", (int id, Produto produtoAtualizado) =>
{
    var produtoExistente = produtos.FirstOrDefault(p => p.Id == id);
    if (produtoExistente == null)
    {
        return Results.NotFound($"Produto com ID {id} não encontrado.");
    }

    produtoExistente.Nome = produtoAtualizado.Nome;
    produtoExistente.Preco = produtoAtualizado.Preco;
    produtoExistente.Estoque = produtoAtualizado.Estoque;

    return Results.Ok();
}); 



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
