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
        Authors = authors ?? new List<User>();
        Reviews = reviews ?? new List<Review>();
        Mods = mods ?? new List<Mod>();
    }

    protected Game() { }

    public virtual string Name { get; set; }
    public virtual string Genre { get; set; }
    public virtual string Devices { get; set; }
    public virtual List<User> Authors { get; set; }
    public virtual List<Review> Reviews { get; set; }
    public virtual List<Mod> Mods { get; set; }
    
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
        Author = author ?? new User("");
    }
    protected Review() { }

    public virtual string Text { get; set; }
    public virtual int Rating { get; set; }
    public virtual User Author { get; set; }
    
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
        Authors = authors ?? new List<User>();
        Compatibility = compatibility ?? new List<Mod>();
    }
    protected Mod() { }

    public virtual string Name { get; set; }
    public virtual string Description { get; set; }
    public virtual List<User> Authors { get; set; }
    public virtual List<Mod> Compatibility { get; set; }
    
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
        OwnedGames = ownedGames ?? new List<Game>();
    }
    protected User() { }
    
    public virtual string Nickname { get; set; }
    public virtual List<Game> OwnedGames { get; set; }
    
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
