using Microsoft.OpenApi;
using Taskivo_Controller.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStartUpConfigurationServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Taskivo API v1",
        Version = "v1",
        Description = "Taskivo Task Management API"
    });

    // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    // {
    //     Name = "Authorization",
    //     Type = SecuritySchemeType.Http,
    //     Scheme = "bearer",
    //     BearerFormat = "JWT",
    //     In = ParameterLocation.Header,
    //     Description = "Enter your JWT token."
    // });

    // options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    // {
    //     [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    // });
});


var app = builder.Build();
// ============================================================================
// API DOCUMENTATION CONFIGURATION (.NET 10+)
// ============================================================================
// Why this setup?
// 1. Framework Standard: We use first-party '.AddOpenApi()' to leverage the 
//    high-performance native OpenAPI document generator built into .NET 10.
// 2. Company Standard: We use '.UseSwaggerUI()' to map the interactive UI 
//    to the native JSON spec, preserving the familiar '/swagger' route and 
//    look-and-feel required by our team guidelines.
// ============================================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // options.SwaggerEndpoint("/swagger/v1/swagger.json", "Taskivo API v1");
    });
}
app.UseCors();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
