using System;
using System.Collections.Generic;

public class Product
{
    public string? Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public int ID { get; set; }
    public int QuantitySold { get; set; }

    public void IncreaseQuantity(int amount)
    {
        this.Quantity += amount;
    }

    public void DecreaseQuantity(int amount)
    {
        if (this.Quantity >= amount)
        {
            this.Quantity -= amount;
            this.QuantitySold += amount;
        }
        else
        {
            Console.WriteLine("Not enough in stock to sell this amount.");
        }
    }

    // Calculate total sales
    public double CalculateTotalSales()
    {
        return this.Price * this.QuantitySold;
    }
}

public class Layout
{
    public string? Name { get; set; }
    public string[]? Products { get; set; }
    public string[]? ID { get; set; }
}

class POS
{
    static Dictionary<string, double?> products = new Dictionary<string, double?>()
    {
        {"Arroz Con Pollo", 16.00},
        {"Bulgogi Tacos", 14.00},
        {"Ceviche", 12.00},
        {"Chimichanga", 14.00},
    };

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the POS System!");
        Console.WriteLine("Please enter your name: ");
        string? name = Console.ReadLine();
        Console.WriteLine("Hello " + name + "!");

        Console.WriteLine("Please enter your password: ");
        string? password = Console.ReadLine();
        if (password == "password")
        {
            Console.WriteLine("Welcome to the POS System!");
        }
        else
        {
            Console.WriteLine("Incorrect password. Please try again.");
        }

        Console.WriteLine("Please enter the name of the product you would like to purchase: ");
        string? product = Console.ReadLine();
        Console.WriteLine("Please enter the quantity of the product you would like to purchase: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        if (product != null && products.ContainsKey(product))
        {
            Console.WriteLine("Your total is: " + products[product] * quantity);
        }
        else
        {
            Console.WriteLine("Sorry, we do not have that product.");
        }

        Console.WriteLine("Thank you for shopping with us! Please come again soon!");
    }
}
