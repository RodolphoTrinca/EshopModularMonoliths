using Basket;
using Catalog;
using Ordering;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogModule(builder.Configuration);
builder.Services.AddBasketModule(builder.Configuration);
builder.Services.AddOrderingModule(builder.Configuration);

var app = builder.Build();

app.Run();
