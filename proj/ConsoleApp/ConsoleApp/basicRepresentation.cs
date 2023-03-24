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
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Author: {Author.Nickname}");
        sb.AppendLine($"Rating: {Rating}");
        sb.AppendLine($"Text: {Text}");

        return sb.ToString();
    }
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

public class User : IUser
{
    public User(string nickname, List<Game>? ownedGames = null)
    {
        Nickname = nickname;
        OwnedGames = ownedGames ?? new List<Game>();
    }

    public string Nickname { get; set; }
    public List<Game> OwnedGames { get; set; }
    
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
