﻿using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Backend
{
    class Application
    {
        public static void Main(string[] args)
        {
            string testString;
            Console.Write("Enter a string - ");
            testString = Console.ReadLine();
            if (testString == "a")
            {
                Console.WriteLine("You selected option {0}", testString);
            }
            else
            {
                Console.WriteLine("You entered '{0}'", testString);
            }
            
        }
    }

    class Cart
    {
        public void ListContents()
        {

        }
    }

    class Checkout
    {
        public void Pay()
        {

        }
    }

    class Coffee
    {
        public void AddCoffee() 
        {
        
        }
    }

    class Buns
    {
        public void AddBun()
        {

        }
    }

    class Tests
    {
        public void TestCart()
        {
         
        }
    }
}   