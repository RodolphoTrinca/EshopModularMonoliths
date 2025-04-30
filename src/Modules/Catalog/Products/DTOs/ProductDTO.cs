namespace Catalog.Products.DTOs;

public record ProductDTO(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
);
