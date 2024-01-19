using News.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppConfiguration config = new AppConfiguration(
    builder.Configuration["ApiKey"],
    builder.Configuration["TopHeadlinesUrl"]);

builder.Services.AddSingleton<AppConfiguration>(config);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
