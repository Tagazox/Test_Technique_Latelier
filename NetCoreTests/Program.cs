using NetCoreTests.Data.Acess.DAL;

var builder = WebApplication.CreateBuilder(args);

string _JsonfilePath = builder.Environment.IsDevelopment() ? $"{Path.GetDirectoryName(Environment.ProcessPath)}\\data.jsonn" : "./data.json";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDataAcessLayer>(DataAcessLayerProvider => new DataAcessLayer(_JsonfilePath));

var portExists = int.TryParse(Environment.GetEnvironmentVariable("PORT"), out var port);
if (portExists)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(port);
    });
}

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
