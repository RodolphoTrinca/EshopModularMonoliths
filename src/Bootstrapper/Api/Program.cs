var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);

var app = builder.Build();

app.UseCatalogModule();
app.UseBasketModule();
app.UseOrderingModule();


app.Run();
