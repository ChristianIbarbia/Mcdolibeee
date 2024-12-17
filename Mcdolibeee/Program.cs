using System;
using System.Linq;
using System.Collections.Generic;
using Mcdoliibee.Services;
using Mcdoliibee.Models;
using Mcdoliibee.Data;

namespace MenuManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Sqldbdata menuServices = new Sqldbdata();

            while (true)
            {
                Console.WriteLine("Welcome to Mcdoliibee");
                Console.WriteLine("-------------");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add Menu Item");
                Console.WriteLine("2. Update Menu Item");
                Console.WriteLine("3. Delete Menu Item");
                Console.WriteLine("4. Show All Menu Items");
                Console.WriteLine("5. Exit");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddMenuItem(menuServices);
                        break;
                    case 2:
                        UpdateMenuItem(menuServices);
                        break;
                    case 3:
                        DeleteMenuItem(menuServices);
                        break;
                    case 4:
                        DisplayAllMenuItems(menuServices);
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddMenuItem(Sqldbdata menuServices)
        {
            Console.WriteLine("Enter Menu Item Name:");
            string itemName = Console.ReadLine();

            Console.WriteLine("Enter Menu Category:");
            string category = Console.ReadLine();

            Console.WriteLine("Enter Menu Code:");
            string code = Console.ReadLine();

            menu newMenu = new menu
            {
                ItemName = itemName,
                Category = category,
                Code = code
            };

            bool success = menuServices.CreateMenu(newMenu);

            if (success)
            {
                Console.WriteLine("Menu item added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add menu item.");
            }
        }

        static void UpdateMenuItem(Sqldbdata menuServices)
        {
            Console.WriteLine("Enter Menu Item Name to update:");
            string itemName = Console.ReadLine();

            List<menu> menus = menuServices.GetAllMenus();
            menu existingMenu = menus.FirstOrDefault(m => m.ItemName == itemName);

            if (existingMenu == null)
            {
                Console.WriteLine($"Menu item '{itemName}' not found.");
                return;
            }

            Console.WriteLine("Enter Updated Code:");
            string code = Console.ReadLine();

            existingMenu.Code = code;

            bool success = menuServices.UpdateMenu(existingMenu);

            if (success)
            {
                Console.WriteLine("Menu item updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update menu item.");
            }
        }

        static void DeleteMenuItem(Sqldbdata menuServices)
        {
            Console.WriteLine("Enter Menu Code to delete:");
            string code = Console.ReadLine();

            bool success = menuServices.DeleteMenu(code);

            if (success)
            {
                Console.WriteLine("Menu item deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to delete menu item with code '{code}'.");
            }
        }

        static void DisplayAllMenuItems(Sqldbdata menuServices)
        {
            List<menu> menus = menuServices.GetAllMenus();

            if (menus.Count == 0)
            {
                Console.WriteLine("No menu items found.");
            }
            else
            {
                foreach (var menu in menus)
                {
                    Console.WriteLine($"Item Name: {menu.ItemName}, Category: {menu.Category}, Code: {menu.Code}");
                }
            }
        }
    }
}