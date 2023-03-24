using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;


/// Bazowa reprezentacja (Czesc 0):
public class Game : IGame
{
    public Game(string name, string genre, string devices, List<User>? authors = null, List<Review>? reviews = null, List<Mod>? mods = null)
    {
        Name = name;
        Genre = genre;
        Devices = devices;
        Authors = authors ?? new List<User>();
        Reviews = reviews ?? new List<Review>();
        Mods = mods ?? new List<Mod>();
    }

    public string Name { get; set; }
    public string Genre { get; set; }
    public string Devices { get; set; }
    public List<User> Authors { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Mod> Mods { get; set; }
}

public class Review : IReview
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

public class Mod : IMod
{
    public Mod(string name, string description, List<User>? authors = null, List<Mod>? compatibility = null)
    {
        Name = name;
        Description = description;
        Authors = authors ?? new List<User>();
        Compatibility = compatibility ?? new List<Mod>();
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Authors { get; set; }
    public List<Mod> Compatibility { get; set; }
}

public class User : IUser
{
    public User(string nickname, List<Game>? ownedGames = null)
    {
        Nickname = nickname;
        OwnedGames = ownedGames ?? new List<Game>();
    }

    public string Nickname { get; set; }
    public List<Game> OwnedGames { get; set; }
}
