namespace Services.Product;

public record UpdateProductRequest(int Id,string Name,decimal Price,int Stock);