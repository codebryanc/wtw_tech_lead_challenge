using BLL;
using DAL;

var builder = WebApplication.CreateBuilder(args);

// Inject interfaces
builder.Services.AddScoped<DAL.IRequest, DAL.Request>();
builder.Services.AddScoped<BLL.IRequest, BLL.Request>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
