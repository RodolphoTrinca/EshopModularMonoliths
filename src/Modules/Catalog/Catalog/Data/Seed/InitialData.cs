namespace Catalog.Data.Seed;

public static class InitialData
{
    public static IEnumerable<Product> Products => new List<Product>()
    {
        Product.Create(
            Guid.NewGuid(),
            "Wireless Mouse",
            new List<string> { "Electronics", "Accessories" },
            "A high-precision wireless mouse with ergonomic design.",
            "images/wireless-mouse.jpg",
            29.99m
        ),
        Product.Create(
            Guid.NewGuid(),
            "Gaming Keyboard",
            new List<string> { "Electronics", "Gaming" },
            "Mechanical gaming keyboard with RGB lighting and programmable keys.",
            "images/gaming-keyboard.jpg",
            79.99m
        ),
        Product.Create(
            Guid.NewGuid(),
            "Noise-Cancelling Headphones",
            new List<string> { "Electronics", "Audio" },
            "Over-ear headphones with active noise cancellation and superior sound quality.",
            "images/noise-cancelling-headphones.jpg",
            199.99m
        ),
        Product.Create(
            Guid.NewGuid(),
            "Smartphone Stand",
            new List<string> { "Accessories", "Mobile" },
            "Adjustable smartphone stand for hands-free use and video calls.",
            "images/smartphone-stand.jpg",
            14.99m
        )
    };
}
