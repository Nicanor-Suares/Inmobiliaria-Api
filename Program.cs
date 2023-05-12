using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001")//permite escuchar SOLO peticiones locales
builder.WebHost.UseUrls("http://localhost:5200", "http://*:5200"); //permite escuchar peticiones locales y remotas

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(
	options => options.UseMySql(
		configuration["ConnectionStrings:MySql"],
		ServerVersion.AutoDetect(configuration["ConnectionStrings:MySql"])
	)
);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
