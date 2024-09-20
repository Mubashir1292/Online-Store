using System;
using System.Collections.Generic;

namespace Online_Store
{
    class Product
    {
        public static List<Product> Products = new List<Product>(); // Make the list static to share across all instances.
        public string ProductName;
        public double Price;

        public Product(string productName, double price)
        {
            this.ProductName = productName;
            this.Price = price;
        }

        public virtual string GetDetails()
        {
            return $"Product Name: {ProductName} Price: {Price}";
        }

        public static void AddNewProduct()
        {
            Console.WriteLine("Enter the Product Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the Price:");
            double price = Convert.ToDouble(Console.ReadLine());

            Product product = new Product(name, price);
            Products.Add(product);
            Console.WriteLine("Product is Added.");
        }

        public static void DisplayProducts()
        {
            if (Products.Count == 0)  // Check for an empty list.
            {
                Console.WriteLine("No Products Available");
            }
            else
            {
                foreach (Product product in Products)
                {
                    Console.WriteLine(product.GetDetails());
                }
            }
        }
    }

    class Electronics : Product
    {
        public int Warranty;
        public bool IsDelivery;

        public Electronics(string productName, double price, int warranty, bool isDelivery)
            : base(productName, price)
        {
            this.Warranty = warranty;
            this.IsDelivery = isDelivery;
        }

        public void Replacement()
        {
            Console.WriteLine("Enter Product Name to Replace:");
            string oldProductName = Console.ReadLine();

            Product oldProduct = Products.Find(p => p.ProductName == oldProductName);

            if (oldProduct == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                Console.WriteLine("Enter New Product Name for Replacement:");
                string newProductName = Console.ReadLine();

                Product newProduct = Products.Find(p => p.ProductName == newProductName);

                if (newProduct == null)
                {
                    Console.WriteLine("Replacement Product not found.");
                }
                else
                {
                    if (newProduct.Price > oldProduct.Price)
                    {
                        double difference = newProduct.Price - oldProduct.Price;
                        Console.WriteLine($"You need to pay an additional ${difference} for the replacement.");
                    }
                    else
                    {
                        Console.WriteLine("Product replacement completed.");
                    }

                    Products.Remove(oldProduct);
                    Products.Add(newProduct);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Adding a new electronics product.
            Console.WriteLine("Enter the Product Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the Warranty in Years:");
            int years = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Price:");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Is There any Delivery? (true/false)");
            bool delivery = Convert.ToBoolean(Console.ReadLine());

            Electronics electronics = new Electronics(name, price, years, delivery);
            Product.Products.Add(electronics); // Add initial product to the list.

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nPress 1 to Add a Product\nPress 2 to Replace a Product\nPress 3 to Display Products\nPress 0 to Exit");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Product.AddNewProduct();
                        break;
                    case 2:
                        electronics.Replacement();
                        break;
                    case 3:
                        Product.DisplayProducts();
                        break;
                    case 0:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}
