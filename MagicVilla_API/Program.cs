
using MagicVilla_API;
using MagicVilla_API.Data;
using MagicVilla_API.Repsository;
using MagicVilla_API.Repsository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<INumberVillaRepository, NumberVillaRepository>();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => {
//        c.RoutePrefix = string.Empty;
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Name of Your API v1");
//    });

//}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
