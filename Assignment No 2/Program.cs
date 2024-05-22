using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo2
{
    internal class Program
    {
        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            public Item(int id, string name, decimal price, int quantity)
            {
                ID = id;
                Name = name;
                Price = price;
                Quantity = quantity;
            }
        }

        public class Inventory
        {
           public static List<Item> items = new List<Item>();

            public void AddItem(Item item)
            {
                items.Add(item);
                Console.WriteLine("Item successfully added to the list.");
            }
            public void DisplayItem(Item item) 
            {
                if (items.Count == 0)
                {
                    Console.WriteLine("There are no items in the list.");
                }
                else
                {
                    foreach (var i in items)
                    {
                        Console.WriteLine($"ID: {i.ID}, Name: {i.Name}, Price: {i.Price}, Quantity: {i.Quantity}");
                    }
                }

            }

            public void FindingItemInList(int id)
            {
                Item n = items.Find(s => s.ID == id);
                if (n != null)
                {
                    Console.WriteLine($"ID: {n.ID}, Name: {n.Name}, Price: {n.Price}, Quantity: {n.Quantity}");
                }
                else
                {
                    Console.WriteLine("Item not found.");
                }
            }

            public void UpdateListItem(int id,string name, decimal price , int quantity)
            {
                Item n = items.Find( s => s.ID == id);
                if(n != null)
                {
                    n.ID = id;
                    n.Name = name;
                    n.Price = price;
                    n.Quantity = quantity;
                    Console.WriteLine("Item updated Sucessfully");
                }
                else
                {
                    Console.WriteLine("Item not found");
                }
            }

            public void RemoveListItem(int id)
            {
                Item n = items.Find(s=>s.ID == id);
                if( n != null )
                {
                    items.Remove(n);
                    Console.WriteLine("item Deleted sucessfully");
                }
                else 
                {
                    Console.WriteLine("Item not found"); 
                }
            }


        }



        
            static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            while (true)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Display All Items");
                Console.WriteLine("3. Find Item by ID");
                Console.WriteLine("4. Update Item");
                Console.WriteLine("5. Delete Item");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddingListItem();
                        break;
                    case 2:
                        show();
                        break;
                    case 3:
                        search();
                        break;
                    case 4:
                        update();
                        break;
                    case 5:
                        Delete();
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            void AddingListItem()
            {
                Console.WriteLine("Enter ID:");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Price:");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("Enter Quantity:");
                int quantity = Convert.ToInt32(Console.ReadLine());

                Item item = new Item(id, name, price, quantity);
                inventory.AddItem(item);
            }

            void show()
            {
                inventory.DisplayItem(null);
            }

            void search()
            { 
                Console.WriteLine("enter id to search:");
                int id = Convert.ToInt32(Console.ReadLine());
                inventory.FindingItemInList(id);
            }

            void update()
            {
                Console.WriteLine("Enter id to Update:");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("enter Name:");
                string name = Console.ReadLine();

                Console.WriteLine("enter price:");
                decimal price = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("enter Quantity:");
                int quantity = Convert.ToInt32(Console.ReadLine());

                inventory.UpdateListItem(id, name, price, quantity);
            }

            void Delete()
            {
                Console.WriteLine("enter Id to Delete:");
                int id = Convert.ToInt32(Console.ReadLine());

                inventory.RemoveListItem(id);
            }
            Console.ReadLine();
        }
    }
}
