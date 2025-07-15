using Microsoft.EntityFrameworkCore;
using BLL;
using DAL;
using DAL.Base.Data;
using DAL.Base.UnitOfWork;

// [Application Builder Configuration]
var builder = WebApplication.CreateBuilder(args);

// [Entity Framework Configuration]
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// [Dependency Injection Configuration]
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DAL.IRequest, DAL.Request>();
builder.Services.AddScoped<BLL.IRequest, BLL.Request>();

// [CORS Configuration]
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// [Services Configuration]
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// [Application Build]
var app = builder.Build();

// [CORS Call]
app.UseCors();

// [Development Environment Configuration]
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// [Middleware Configuration]
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// [Application Run]
app.Run();
