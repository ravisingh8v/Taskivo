using Taskivo_Controller.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStartUpConfigurationServices(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Taskivo API v1");
    });
}
app.UseCors();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
