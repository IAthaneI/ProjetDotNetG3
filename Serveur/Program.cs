using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serveur.Datas;
using Serveur.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajouter HttpClient et ExchangeRateService
builder.Services.AddHttpClient<ExchangeRateService>();


// Ajouter la politique CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.ConfigureWarnings(warnings =>
    warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    options.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;Database=dbCB;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Appliquer la politique CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
