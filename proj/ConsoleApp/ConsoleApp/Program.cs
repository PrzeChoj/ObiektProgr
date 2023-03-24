// See https://aka.ms/new-console-template for more information

using ConsoleApp;

Console.WriteLine("Hello, World!");

User u1 = new User("Adam", new List<Game>());
List<User> aut1 = new List<User>();
aut1.Add(u1);
Game g1 = new Game("Garbage Collector", "simulation", "PC", aut1, new List<Review>(), new List<Mod>());

Console.Write(g1);