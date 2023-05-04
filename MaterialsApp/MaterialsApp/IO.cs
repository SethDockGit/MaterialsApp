using System;
using System.Collections.Generic;
using System.Text;
using MaterialsApp.Data;
using MaterialsApp.Logic;
using MaterialsApp.Models;

namespace MaterialsApp
{
    class IO
    {
        private bool Exit { get; set; } = false;
        private Manager Manager { get; set; }
        public IO(IDataSource dataSource)
        {
            Manager = new ManagerFactory().GetManager();
        }
        public void Run()
        {
            while (!Exit)
            {
                Menu();
            }
        }
        public void Menu()
        {
            Console.Clear();

            Console.WriteLine("                    ***** Materials App *****\n\n");
            Console.WriteLine("--------------------");
            Console.WriteLine("-  Menu Selection  -");
            Console.WriteLine("--------------------");
            Console.WriteLine("1. Check resources");
            Console.WriteLine("2. Deposit a resource");
            Console.WriteLine("3. Withdraw a resource");
            Console.WriteLine("--------------------\n");
            Console.WriteLine("Press a number to select a menu item, or ESC to quit: ");

            var cki = Console.ReadKey(true);

            switch (cki.Key)
            {
                case ConsoleKey.D1:
                    CheckResourcesWorkFlow();
                    break;

                case ConsoleKey.D2:
                    DepositResourceWorkFlow();
                    break;

                case ConsoleKey.D3:
                    WithdrawResourceWorkFlow();
                    break;

                case ConsoleKey.Escape:
                    Exit = true;
                    break;

                default:
                    break;
            }
        }
        internal void CheckResourcesWorkFlow()
        {
            string username = GetUsername();
            WorkflowResponse response = Manager.CheckResources(username);

            if(!response.Success)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
            else
            {
                PrintUserResources(response.User);
            }
        }
        internal void DepositResourceWorkFlow()
        {
            string username = GetUsername();
            User user = Manager.Authenticate(username);
            if (user == null)
            {
                Console.WriteLine($"Error: user not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
            else
            {
                ResourceType resource = GetResourceType();

                if (resource == ResourceType.Invalid)
                {
                    Console.WriteLine("Error: resource type selection was not valid. Press any key to return to the main menu... ");
                    Console.ReadKey();
                }
                else
                {
                    int depositAmount = GetIntFromUser($"How much {resource} would you like to deposit?\n");

                    WorkflowResponse response = Manager.DepositResource(user, resource, depositAmount);

                    if (!response.Success)
                    {
                        Console.WriteLine(response.Message);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine(response.Message);
                        Console.ReadKey();
                    }
                }
            }
        }
        internal void WithdrawResourceWorkFlow()
        {
            string username = GetUsername();
            User user = Manager.Authenticate(username);
            if (user == null)
            {
                Console.WriteLine($"Error: user not found. Press any key to return to the main menu... ");
                Console.ReadKey();
            }
            else
            {
                ResourceType resource = GetResourceType();

                if (resource == ResourceType.Invalid)
                {
                    Console.WriteLine("Error: resource type selection was not valid. Press any key to return to the main menu... ");
                    Console.ReadKey();
                }
                else
                {
                    int withdrawAmount = GetIntFromUser($"How much {resource} would you like to withdraw?\n");

                    WorkflowResponse response = Manager.WithdrawResource(user, resource, withdrawAmount);

                    if (!response.Success)
                    {
                        Console.WriteLine(response.Message);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine(response.Message);
                        Console.ReadKey();
                    }
                }
            }
        }
        private string GetUsername()
        {
            Console.Clear();

            Console.Write("Please enter your username: ");
            string input = Console.ReadLine();

            return input;
        }
        private ResourceType GetResourceType()
        {
            Console.Clear();
            Console.WriteLine("***Select a resource, then press enter***\n");
            Console.WriteLine("1. Wood\n2. Stone\n3. Iron\n4. Gold\n");
            string userInput = Console.ReadLine();

            ResourceType resource;

            switch (userInput)
            {
                case "1":
                    resource = ResourceType.Wood;
                    break;

                case "2":
                    resource = ResourceType.Stone;
                    break;

                case "3":
                    resource = ResourceType.Iron;
                    break;

                case "4":
                    resource = ResourceType.Gold;
                    break;

                default:
                    resource = ResourceType.Invalid;
                    break;
            }
            return resource;
        }
        private int GetIntFromUser(string prompt)
        {
            Console.Clear();
            Console.WriteLine($"{prompt}");
            string toParse = Console.ReadLine();

            int amount = -1;
            int.TryParse(toParse, out amount);
            return amount;

        }
        private void PrintUserResources(User user)
        {
            Console.Clear();

            Console.WriteLine($"{user.Username}'s Materials:\n\n");
            Console.WriteLine($"Wood: {user.WoodCount}");
            Console.WriteLine($"Stone: {user.StoneCount}");
            Console.WriteLine($"Iron: {user.IronCount}");
            Console.WriteLine($"Gold: {user.GoldCount}");
            Console.WriteLine("\n\nPress any key to return to the main menu...");

            Console.ReadKey();
        }
    }
}
