using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;


/// Bazowa reprezentacja (Czesc 0):
public class Game : IGame
{
    public Game(string name, string genre, string devices, List<IUser>? authors = null, List<IReview>? reviews = null, List<IMod>? mods = null)
    {
        Name = name;
        Genre = genre;
        Devices = devices;
        Authors = authors ?? new List<IUser>();
        Reviews = reviews ?? new List<IReview>();
        Mods = mods ?? new List<IMod>();
    }

    public string Name { get; set; }
    public string Genre { get; set; }
    public string Devices { get; set; }
    public List<IUser> Authors { get; set; }
    public List<IReview> Reviews { get; set; }
    public List<IMod> Mods { get; set; }
}

public class Review : IReview
{
    public Review(string text, int rating, IUser author)
    {
        Text = text;
        Rating = rating;
        Author = author;
    }

    public string Text { get; set; }
    public int Rating { get; set; }
    public IUser Author { get; set; }
}

public class Mod : IMod
{
    public Mod(string name, string description, List<IUser>? authors = null, List<IMod>? compatibility = null)
    {
        Name = name;
        Description = description;
        Authors = authors ?? new List<IUser>();
        Compatibility = compatibility ?? new List<IMod>();
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public List<IUser> Authors { get; set; }
    public List<IMod> Compatibility { get; set; }
}

public class User : IUser
{
    public User(string nickname, List<IGame>? ownedGames = null)
    {
        Nickname = nickname;
        OwnedGames = ownedGames ?? new List<IGame>();
    }

    public string Nickname { get; set; }
    public List<IGame> OwnedGames { get; set; }
}
