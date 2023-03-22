using Emerson.DataProcessing.Application.Helper;
using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.WriteIndented = true; 
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IFoo1Device, Foo1Device>();
builder.Services.AddTransient<IFoo2Device, Foo2Device>();
builder.Services.AddTransient<ISummarizeData, SummarizeData>();
builder.Services.AddTransient<IJsonParser, JsonParser>();

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
