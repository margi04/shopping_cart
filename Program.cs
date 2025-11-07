using System;
using System.Collections.Generic;

class Shopping_cart
{
    static void main()
    {
        Dictionary<string, double> productprice = new Dictionary<string, double>()
        {
            {"Apple", 0.99},
            {"Banana", 0.59},
            {"Orange", 0.79 },
            {"milk",2.0 }
        };

        List<string> cart=new List<string>();
        bool shopping = true;
        while(shopping)
        {
            Console.WriteLine("\nAvailable Product");
            foreach (var item in productprice)
            {
                Console.WriteLine($"{item.Key}: ${item.Value}");
            }
            Console.WriteLine("\nEnter the product name to add to cart or type 'checkout' to finish:");
            string input = Console.ReadLine();
            if (input.ToLower() == "checkout")
            {
                shopping = false;
            }
            else if (productprice.ContainsKey(input))
            {
                cart.Add(input);
                Console.WriteLine($"{input} added to cart.");
            }
            else
            {
                Console.WriteLine("Product not found. Please try again.");
            }
        }
    }
}
