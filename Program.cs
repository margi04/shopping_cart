using System;
using System.Collections.Generic;
using System.Linq;
class Shopping_cart
{
    static void Main()
    {
        Dictionary<string, double> productprice = new Dictionary<string, double>()
        {
            {"Apple", 0.99},
            {"Banana", 0.59},
            {"Orange", 0.79 },
            {"Milk",2.0 },
            {"Chocolate",3.0 },
            {"Curd",1.5 },
            {"Butter-Milk",2.0}
        };

        List<string> cart = new List<string>();
        bool shopping = true;
        while (shopping)
        {
            Console.WriteLine("\nAvailable Product");
            var shortedproducts = productprice.OrderBy(p => p.Key);

            foreach (var item in shortedproducts)
            {
                Console.WriteLine($"{item.Key}: ${item.Value}");
            }

            Console.WriteLine("\nType Product Name for Buy");
            Console.WriteLine("\nCommands: 'checkout' to finish, 'remove' to discard, 'filter' to filter by price, 'viewcart' to view cart");
            string? input = Console.ReadLine()?.Trim();
            string? product = productprice.Keys
                .FirstOrDefault(k => k.Equals(input, StringComparison.OrdinalIgnoreCase));

            if (string.Equals(input, "checkout", StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Are you sure you want to checkout? (yes/no): ");
                if (Console.ReadLine()?.Trim().ToLower() == "yes")
                    shopping = false;
            }
            else if (string.Equals(input, "remove", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter the product name to remove from cart:");
                string? removeItem = Console.ReadLine()?.Trim();
                string? toRemove = cart.FirstOrDefault(i => i.Equals(removeItem, StringComparison.OrdinalIgnoreCase));
                if (toRemove != null)
                {
                    Console.Write("Enter quantity to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int qty))
                    {
                        for (int i = 0; i < qty && cart.Contains(toRemove); i++)
                            cart.Remove(toRemove);
                        Console.WriteLine($"{qty} x {toRemove} removed from cart.");
                    }
                    else
                    {
                        cart.Remove(toRemove);
                        Console.WriteLine($"{toRemove} removed from cart.");
                    }
                }
                else
                {
                    Console.WriteLine("Item not found in cart.");
                }
            }
            else if (string.Equals(input, "filter", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter the min-price to filter");
                if (double.TryParse(Console.ReadLine(), out double minPrice))
                {
                    var filterd = productprice.Where(p => p.Value >= minPrice);
                    Console.WriteLine($"\nProducts Costing>=${minPrice:F2}:");
                    foreach (var f in filterd)
                    {
                        Console.WriteLine($"{f.Key,-15}:${f.Value:F2}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Price Entered");
                }
            }
            else if (string.Equals(input, "viewcart", StringComparison.OrdinalIgnoreCase))
            {
                DisplayCart(cart, productprice);
            }
            else if (!string.IsNullOrEmpty(product) && productprice.ContainsKey(product))
            {
                Console.Write($"Enter quantity of {product}: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    for (int i = 0; i < quantity; i++)
                        cart.Add(product);
                    Console.WriteLine($"{quantity} x {product} added to cart.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity.");
                }
            }
            else
            {
                Console.WriteLine("Product not found. Please try again.");
            }
        }

        Console.WriteLine("\n========Your Shopping Cart:========");
        if (cart.Count() == 0)
        {
            Console.WriteLine("Your Cart is Empty!!!");
            return;
        }
        DisplayCart(cart, productprice);
        Console.WriteLine("\n Thank you for shopping with Us!!");
    }
        static void DisplayCart(List<string>cart,Dictionary<string,double>productprice)
        {
            var cartDetails = from item in cart
                              group item by item into g
                              let quantity = g.Count()
                              let price = productprice[g.Key]
                              let total = price * quantity
                              select new { Product = g.Key, Quantity = quantity, Price = price, Total = total };

            Console.WriteLine("\n=======CART DETAILS=======");
            Console.WriteLine("{0,-15} {1,10} {2,10} {3,15}", "Product", "Quantity", "Price($)", "Total($)");
            Console.WriteLine(new string('-', 55));

        
        foreach (var item in cartDetails.OrderByDescending(x => x.Total))
            {
                Console.WriteLine("{0,-15} {1,10} {2,10:F2} {3,15:F2}",
                    item.Product, item.Quantity, item.Price, item.Total);
            }
       
        Console.WriteLine(new string('-', 55));
        double grandTotal = cartDetails.Sum(x => x.Total);
        Console.WriteLine("{0,-15} {1,10} {2,10:F2} {3,15:F2}", "GrandTotal", "", "", grandTotal);

        if (grandTotal > 5)
            {
                grandTotal *= 0.9;
            Console.WriteLine("\n10% discount applied!");
            Console.WriteLine("{0,-15} {1,10} {2,10} {3,15:F2}", "Final Total", "", "", grandTotal);
        }
        


        var categories = new Dictionary<string, string>
        {
            {"Apple","Fruit"},
            {"Banana","Fruit" },
            {"Orange","Fruit" },
            {"Milk","Dairy" },
            {"Chocolate","Snacks" },
            {"Curd","Dairy" },
            {"Butter-Milk","Dairy"}
        };
       
            var categoryTotal = from c in cartDetails
                                join cat in categories on c.Product equals cat.Key
                                group c by cat.Value into g
                                select new
                                {
                                    Category = g.Key,
                                    TotalValue = g.Sum(X => X.Total)
                                };
            foreach (var g in categoryTotal)
            {
                Console.WriteLine($"{g.Category}:${g.TotalValue:F2}");
            }

    }
    }
