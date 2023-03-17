namespace ConsoleApp;
using System.Collections.Generic;

public class Game
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public List<User> Authors { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Mod> Mods { get; set; }
    public string Devices { get; set; }
}

public class Review
{
    public string Text { get; set; }
    public int Rating { get; set; }
    public User Author { get; set; }
}

public class Mod
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Authors { get; set; }
    public List<Mod> Compatibility { get; set; }
}

public class User
{
    public string Nickname { get; set; }
    public List<Game> OwnedGames { get; set; }
}


