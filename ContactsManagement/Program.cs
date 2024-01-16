using ContactsManagement.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IContactsService,ContactsService>();
builder.Services.AddSwaggerGen();

var specificOrgins = "AppOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: specificOrgins,
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().WithMethods("GET", "POST", "PUT","DELETE");
    });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
   
    app.UseCors(specificOrgins);

}


app.UseAuthorization();

app.MapControllers();

app.Run();
