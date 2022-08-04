
using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;


var AllowAll = "_myAllowAllOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

    

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapperInitilizer));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers().AddNewtonsoftJson(options => 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddCors(options=> {
    options.AddPolicy(name: AllowAll, policy => {
        policy.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader();
    
    });
});

    var app = builder.Build();    

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }



    app.UseHttpsRedirection();
    app.UseCors(AllowAll);
    app.UseAuthorization();
    app.MapControllers();
    app.Run();




