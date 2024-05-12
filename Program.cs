//simple tast list application

using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace demo
{
    internal class Program
    {
       

        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            void show()
            {
                Console.WriteLine("list as Shown:");
                foreach (String i in list )
                {
                    Console.WriteLine(i);
                }
            }


            while (true)
            {
                Console.WriteLine("\n\n 1.create and add item in list  \n 2.update list at specific index \n 3.delete iteam from list \n 4.show list  \n 5.exit\n");
                Console.WriteLine("enter option number : ");
                int a = Convert.ToInt32(Console.ReadLine());
                switch (a)
                {
                    case 1:
                        Console.WriteLine("\nenter the size how many iteam you want to enter: ");
                        int size = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nadd iteams");
                        for (int i = 1; i <= size; i++)
                        {
                            string iteam = Console.ReadLine();
                            list.Add(iteam);

                        }
                        Console.WriteLine("list created successfully\n");
                        show();
                        Console.WriteLine("\n");
                        break;

                    case 2:
                        Console.WriteLine("\nenter index to update");
                        int index = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nenter new iteam :");
                        string newiteam = Console.ReadLine();
                        if (list[index]!=newiteam)
                        {
                            list[index] = newiteam;
                        }
                        else 
                        {
                            Console.WriteLine("you enter iteam is already exist in list Try again!!");
                        }
                        Console.WriteLine("\nlist updated successfully\n");
                        show();
                        break;

                    case 3:
                        Console.WriteLine("\nenter index to delete");
                        int ind = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nConfirm you want to delete iteam form list y(yes) or n(no)");
                        string inp = Console.ReadLine();
                        if (inp == "y")
                        {
                            list.RemoveAt(ind);
                            Console.WriteLine("\niteam successfully deleted form list");
                            show();
                        }
                        break;
                    case 4:
                        Console.WriteLine("\n");
                        show();
                        break;

                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
            
         
     

        }
    }
     
}
