using DotNetDemo.Service;

string dotNetDemoCORSOrigins = "_dotNetDemoCORSOrigins";

var builder = WebApplication.CreateBuilder(args);

var corsPolicyUrl = builder.Configuration["CorsOrigins"];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
                          policy =>
                          {
                              policy.WithOrigins(corsPolicyUrl)
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
