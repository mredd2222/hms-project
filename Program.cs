using Microsoft.EntityFrameworkCore;
using MTCBackend.Data;
using MTCBackend.Interfaces;
using MTCBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<MTCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MTCDatabase")));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add your custom services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IBillingService, BillingService>();
builder.Services.AddScoped<IFileService, FileService>();  // New FileService for handling file uploads
builder.Services.AddScoped<IFormService, FormService>();  // New FormService for handling forms
builder.Services.AddScoped<IDigitalSignatureService, DigitalSignatureService>();
builder.Services.AddScoped<IPatientDashboardService, PatientDashboardService>();

// CORS policy configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()   // Allow requests from any origin
               .AllowAnyMethod()   // Allow any HTTP method (GET, POST, etc.)
               .AllowAnyHeader();  // Allow any headers (e.g., Authorization, Content-Type)
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
    if (app.Environment.IsDevelopment())
        options.RoutePrefix = "swagger";
    else
        options.RoutePrefix = string.Empty;
}
);
app.UseSwagger();

// Apply the CORS policy globally
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
