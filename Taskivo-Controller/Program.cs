using Microsoft.OpenApi;
using Taskivo_AppServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddOpenApi(options=>
{
   options.AddDocumentTransformer((document, context, calcellationToken) =>
   {
       document.Components ??=new OpenApiComponents();
       document.Components.SecuritySchemes?.Add("Bearer", new OpenApiSecurityScheme
       {
           Type = SecuritySchemeType.Http,
           Scheme = "bearer"

       });
       return Task.CompletedTask;
   });
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
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Taskivo API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
