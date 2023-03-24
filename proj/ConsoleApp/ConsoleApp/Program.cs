// See https://aka.ms/new-console-template for more information

using ConsoleApp;

Console.WriteLine("Hello, World!");

User u1 = new User("Adam");
Game g1 = new Game("Garbage Collector", "simulation", "PC", new List<User>(){u1});

Console.WriteLine(g1);

Console.WriteLine(u1);