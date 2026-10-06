using StockControl.Models;

namespace StockControl.Data
{
    public static class Stock
    {
        private static List<Product> Products { get; }

        static Stock()
        {
            Products = new List<Product>();
        }
        
        public static void AddProduct(Product product)
        {
            Products.Add(product);
        }
        
        public static void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
        
        public static List<Product> GetProducts()
        {
            return Products;
        }
        
        public static Product? GetProductById(Guid id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public static void UpdateProduct(Product updatedProduct)
        {
            var existingProduct = GetProductById(updatedProduct.Id);
            if (existingProduct is null)
            {
                return;
            }
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Id == updatedProduct.Id)
                {
                    Products[i] = updatedProduct;
                    break;
                }
            }
        }
    }
}