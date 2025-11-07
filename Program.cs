using System;
using System.Collections.Generic;

class Shopping_cart
{
    static void Main()
    {
        Dictionary<string, double> productprice = new Dictionary<string, double>()
        {
            {"Apple", 0.99},
            {"Banana", 0.59},
            {"Orange", 0.79 },
            {"milk",2.0 }
        };

        List<string> cart = new List<string>();
        bool shopping = true;
        while (shopping)
        {
            Console.WriteLine("\nAvailable Product");
            foreach (var item in productprice)
            {
                Console.WriteLine($"{item.Key}: ${item.Value}");
            }
            Console.WriteLine("\nEnter the product name to add to cart or type 'checkout' to finish:");
            string? input = Console.ReadLine();
            if (input != null && input.ToLower() == "checkout")
            {
                shopping = false;
            }
            else if (input != null &&  input.ToLower() == "remove")
            {
                Console.WriteLine("Enter the product name to remove from cart:");
                string? removeItem = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(removeItem) && cart.Contains(removeItem))
                {
                    cart.Remove(removeItem);
                    Console.WriteLine($"{removeItem} removed from cart.");
                }
                else
                {
                    Console.WriteLine("Item not found in cart.");
                }
            }
            else if (!string.IsNullOrEmpty(input) && productprice.ContainsKey(input))
            {
                cart.Add(input);
                Console.WriteLine($"{input} added to cart.");
            }
            else
            {
                Console.WriteLine("Product not found. Please try again.");
            }
            }
        Console.WriteLine("\nYour Shopping Cart:");
        double total = 0;
        foreach (var item in cart)
        {
            double price = productprice[item];
            Console.WriteLine($"{item}: ${price}");
            total += price;

        }
        if (total > 5)
        {
            Console.WriteLine("A 10% discount has been applied to your total!");
            total *= 0.9;
        }

        Console.WriteLine($"\nTotal Amount: ${total:F2}");
        Console.WriteLine("Thank you for shopping with us!");
    }
    } 