using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;


// Bazowa reprezentacja (Czesc 0):
public class Game
{
    public Game(string name, string genre, string devices, List<User>? authors = null, List<Review>? reviews = null, List<Mod>? mods = null)
    {
        Name = name;
        Genre = genre;
        Devices = devices;
        Authors = authors!;
        Reviews = reviews!;
        Mods = mods!;
    }

    protected Game() { }

    public virtual string Name { get; set; }
    public virtual string Genre { get; set; }
    public virtual string Devices { get; set; }

    private List<User> _authors;
    public virtual List<User> Authors
    {
        get => _authors;
        set => _authors = value ?? new List<User>();
    }

    private List<Review> _reviews;
    public virtual List<Review> Reviews
    {
        get => _reviews;
        set => _reviews = value ?? new List<Review>();
    }
    
    private List<Mod> _mods;
    public virtual List<Mod> Mods
    {
        get => _mods;
        set => _mods = value ?? new List<Mod>();
    }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Genre: {Genre}");
        sb.AppendLine($"Devices: {Devices}");
        sb.AppendLine("Authors:");
        foreach (var author in Authors)
        {
            sb.AppendLine($"- {author.Nickname}");
        }
        sb.AppendLine("Reviews:");
        foreach (var review in Reviews)
        {
            sb.AppendLine($"- {review.Author}: {review.Rating}");
        }
        sb.AppendLine("Mods:");
        foreach (var mod in Mods)
        {
            sb.AppendLine($"- {mod.Name}");
        }
    
        return sb.ToString();
    }
}

public class Review
{
    public Review(string text, int rating, User? author = null)
    {
        Text = text;
        Rating = rating;
        Author = author!;
    }
    protected Review() { }

    public virtual string Text { get; set; }
    public virtual int Rating { get; set; }

    private User _author;
    public virtual User Author
    {
        get => _author;
        set => _author = value ?? new User("");
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Author: {Author.Nickname}");
        sb.AppendLine($"Rating: {Rating}");
        sb.AppendLine($"Text: {Text}");

        return sb.ToString();
    }
}

public class Mod
{
    public Mod(string name, string description, List<User>? authors = null, List<Mod>? compatibility = null)
    {
        Name = name;
        Description = description;
        Authors = authors!;
        Compatibility = compatibility!;
    }
    protected Mod() { }

    public virtual string Name { get; set; }
    public virtual string Description { get; set; }

    private List<User> _authors;
    public virtual List<User> Authors
    {
        get => _authors;
        set => _authors = value ?? new List<User>();
    }

    private List<Mod> _compatibility;
    public virtual List<Mod> Compatibility
    {
        get => _compatibility;
        set => _compatibility = value ?? new List<Mod>();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Description: {Description}");
        sb.AppendLine("Authors:");
        foreach (var author in Authors)
        {
            sb.AppendLine($"- {author.Nickname}");
        }
        sb.AppendLine("Compatibility:");
        foreach (var mod in Compatibility)
        {
            sb.AppendLine($"- {mod.Name}");
        }
        return sb.ToString();
    }
}

public class User
{
    public User(string nickname, List<Game>? ownedGames = null)
    {
        Nickname = nickname;
        OwnedGames = ownedGames!;
    }
    protected User() { }
    
    public virtual string Nickname { get; set; }

    private List<Game> _ownedGames;
    public virtual List<Game> OwnedGames
    {
        get => _ownedGames;
        set => _ownedGames = value ?? new List<Game>();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Nickname: {Nickname}");
        sb.AppendLine("Owned Games:");
        foreach (var game in OwnedGames)
        {
            sb.AppendLine($"- {game.Name}");
        }
        return sb.ToString();
    }
}
