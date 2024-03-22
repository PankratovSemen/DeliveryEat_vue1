using DeliveryEat_vue1.Server.DataBase.Basket;
using DeliveryEat_vue1.Server.DataBase.Contexts;

namespace DeliveryEat_vue1.Server.Interfaces.DataCode
{
    public class ProductManager
    {
        ApplicationContext context;
        ProductManager(ApplicationContext applicationContext)
        {
            context = applicationContext;
        }

        public IEnumerable<Product> GetProductsAll()
        {
            
                return context.Product.ToArray();
            
        }

        public void AddProduct(Product product)
        {
             
                context.Product.Add(product);
                context.SaveChanges();
            
        }

        public void DeleteProduct(Product product)
        {
            
                context.Product.Remove(product);
                context.SaveChanges();
            
        }
    }
}
