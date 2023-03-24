namespace ConsoleApp;
using System.Collections.Generic;


/// Bazowa reprezentacja (Czesc 0):
public class Game : GameAbstract
{
    public Game(string name, string genre, List<User> authors, List<Review> reviews, List<Mod> mods, string devices)
    {
        Name = name;
        Genre = genre;
        Authors = authors;
        Reviews = reviews;
        Mods = mods;
        Devices = devices;
    }

    public string Name { get; set; }
    public string Genre { get; set; }
    public List<User> Authors { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Mod> Mods { get; set; }
    public string Devices { get; set; }
}

public class Review : ReviewAbstract
{
    public Review(string text, int rating, User author)
    {
        Text = text;
        Rating = rating;
        Author = author;
    }

    public string Text { get; set; }
    public int Rating { get; set; }
    public User Author { get; set; }
}

public class Mod : ModAbstract
{
    public Mod(string name, string description, List<User> authors, List<Mod> compatibility)
    {
        Name = name;
        Description = description;
        Authors = authors;
        Compatibility = compatibility;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Authors { get; set; }
    public List<Mod> Compatibility { get; set; }
}

public class User : UserAbstract
{
    public User(string nickname, List<Game> ownedGames)
    {
        Nickname = nickname;
        OwnedGames = ownedGames;
    }

    public string Nickname { get; set; }
    public List<Game> OwnedGames { get; set; }
}
