using Microsoft.EntityFrameworkCore;
using Cinesimbiose.API.Data;
var builder = WebApplication.CreateBuilder(args);
//  Injeção de Dependência. 
// "registrando" nosso objeto de contexto (DbContext) para que 
// o .NET possa injetá-lo automaticamente em outras classes (como os Controllers).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CinesimbioseContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add services to the container.
builder.Services.AddControllersWithViews();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          // Vamos trocar "XXXX" na Fase 7
                          policy.WithOrigins("https://localhost:XXXX")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
