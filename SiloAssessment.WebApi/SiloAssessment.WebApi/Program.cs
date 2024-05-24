using Microsoft.EntityFrameworkCore;
using SiloAssessment.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using Cqrs.Hosts;
using SiloAssessment.Domain.Interfaces;
using SiloAssessment.Infrastructure.Repositories;
using SiloAssessment.Application.Queries.BoardRoom;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetBoardRoomsQueryHandler).Assembly);
builder.Services.AddScoped<IBoardRoomRepository, BoardRoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
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
