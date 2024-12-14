using Domain.Model;
using Infrastructure.DataContext;
using Infrastructure.Service.CustomerService;
using Infrastructure.Service.LocationService;
using Infrastructure.Service.TicketService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<DapperContext>();

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


