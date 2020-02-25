using System;
using System.Linq;
using WiredBrainCoffee.DataAccess;

namespace WiredBrainCoffee.ShopInfoTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wired Brain Coffee - shop Info Tool");
            Console.WriteLine("Write 'help' to list available commands, "+ "write 'quit' to exit application");

            var coffeeShopDataProvider = new CoffeeShopDataProvider();

            while (true)
            {
                var line = Console.ReadLine();
                var coffeeShops = coffeeShopDataProvider.LoadCoffeeShops();
                if (string.Equals("quit", line, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                if (string.Equals("help", line, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("> Available coffee shop commands: ");
                    foreach (var coffeeShop in coffeeShops)
                    {
                        Console.WriteLine("> " + coffeeShop.Location);
                    }
                }
                
                else
                {
                    var foundCoffeeShops = coffeeShops
                        .Where(x => x.Location.StartsWith(line, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    if(foundCoffeeShops.Count ==0)
                    {
                        Console.WriteLine($">Command '{line}' not found");
                    }
                    else if (foundCoffeeShops.Count == 1)
                    {
                        var coffeeShop = foundCoffeeShops.Single();
                        Console.WriteLine($"> Location: {coffeeShop.Location}");
                        Console.WriteLine($"> Beans: {coffeeShop.BeansInStockInKg} kg");
                    }
                    else
                    {
                        Console.WriteLine($"> Multiple matching coffee shops command found: ");
                        foreach(var coffeeType in foundCoffeeShops)
                        {
                            Console.WriteLine($"> {coffeeType.Location}");
                        }
                    }
                }
            }

        }
    }
}

