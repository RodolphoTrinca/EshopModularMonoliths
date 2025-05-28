var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;

builder.Services.AddCarterWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddMediatRWithAssemblies(catalogAssembly, basketAssembly);

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandlers();

var app = builder.Build();

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => {});

app.UseCatalogModule();
app.UseBasketModule();
app.UseOrderingModule();

app.Run();
