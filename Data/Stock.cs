using StockControl.Models;

namespace StockControl.Data
{
    public class Stock
    {
        private List<Product> Products { get; }

        public Stock()
        {
            Products = new List<Product>();
        }
        
        public void AddProduct(Product product)
        {
            Products.Add(product);
        }
        
        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
        
        public List<Product> GetProducts()
        {
            return Products;
        }
        
        public Product? GetProductById(Guid id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(Product updatedProduct)
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