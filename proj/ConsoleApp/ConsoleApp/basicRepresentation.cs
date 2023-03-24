using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;


/// Bazowa reprezentacja (Czesc 0):
public class Game
{
    public Game(string name, string genre, string devices, List<User> authors, List<Review> reviews, List<Mod> mods)
    {
        Name = name;
        Genre = genre;
        Devices = devices;
        Authors = authors;
        Reviews = reviews;
        Mods = mods;
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
        if (Authors == null)
        {
            sb.AppendLine("Authors: NONE");
        }
        else
        {
            sb.AppendLine("Authors:");
            foreach (var author in Authors)
            {
                sb.AppendLine($"- {author.Nickname}");
            }
        }

        if (Reviews == null)
        {
            sb.AppendLine("Reviews: NONE");
        }
        else
        {
            sb.AppendLine("Reviews:");
            foreach (var review in Reviews)
            {
                sb.AppendLine($"- {review.Author}: {review.Rating}");
            }
        }

        if (Mods == null)
        {
            sb.AppendLine("Mods: NONE");
        }
        else
        {
            sb.AppendLine("Mods:");
            foreach (var mod in Mods)
            {
                sb.AppendLine($"- {mod.Name}");
            }
        }

        return sb.ToString();
    }
}

public class Review
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

        sb.Append($"Author: {Author.Nickname}\n");
        sb.Append($"Rating: {Rating}\n");
        sb.Append($"Text: {Text}");

        return sb.ToString();
    }
}

public class Mod
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
    public User(string nickname, List<Game> ownedGames)
    {
        Nickname = nickname;
        OwnedGames = ownedGames;
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
            sb.AppendLine(game.Name);
        }
        return sb.ToString();
    }
}
