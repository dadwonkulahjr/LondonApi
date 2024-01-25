using LondonAPI.ApiServices;
using LondonAPI.DataSource;
using LondonAPI.Filters;
using LondonAPI.Infrastructure;
using LondonAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<JsonExceptionFilter>();
    options.Filters.Add<RequireHttpsOrCloseAttribute>();
    options.Filters.Add<LinkRewritingFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Convert all api urls to lowercase
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;

});
//Add Service
builder.Services.AddScoped<IApiRoomService, RoomServiceImplementation>();

//Register AutoMappper Service using Microsoft DI container
builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<MappingProfile>();
});
//Add The Api version system.
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new MediaTypeApiVersionReader();
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
});

//Fetch mock data from appsettings.json file
builder.Services.Configure<HotelInfo>(builder.Configuration.GetSection("Info"));

//Register IMemory Database Service
builder.Services.AddDbContext<HotelApiDbContext>(options =>
{
    options.UseInMemoryDatabase("LondonApiDb");
});

var app = builder.Build();

//Register service to work with test InMemory Data!
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.InitilizeAsync(services).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured seeding the database!");

    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
