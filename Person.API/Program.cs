using Microsoft.EntityFrameworkCore;
using Person.Domain.Helpers;
using Person.Domain.PersonAggregate;
using Person.Infrastructure;
using Person.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter())
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    setup =>
    {
        setup.OrderActionsBy(ac => ac.HttpMethod);
    }
);

builder.Services.AddDbContext<PersonContext>(
    option =>
    {
        option.UseNpgsql(
            "Server=127.0.0.1;Port=5432;Database=PersonDBGenerator;User Id=postgres;Password=8523;"
        );
    },
    ServiceLifetime.Scoped
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPersonRepository, PersonRepository>();

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
