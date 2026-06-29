var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "ok", service = "api-gateway" }));
app.MapGet("/", () => Results.Ok(new
{
	message = "Student Services API Gateway",
	routes = new[]
	{
		"/api/students/*",
		"/api/courses/*",
		"/api/enrollments/*"
	}
}));

app.MapReverseProxy();

app.Run();
