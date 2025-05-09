var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly);

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);

builder.Services.AddExceptionHandlers();

var app = builder.Build();

app.MapCarter();

app.UseCatalogModule();
app.UseBasketModule();
app.UseOrderingModule();

app.UseExceptionHandler(options => {});

app.Run();
