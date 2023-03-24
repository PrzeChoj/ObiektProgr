using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;


/// Abstrakcyjan klasa do nadpisywania:
public abstract class GameAbstract
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public List<UserAbstract> Authors { get; set; }
    public List<ReviewAbstract> Reviews { get; set; }
    public List<ModAbstract> Mods { get; set; }
    public string Devices { get; set; }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Genre: {Genre}");
        sb.AppendLine($"Devices: {Devices}");
        ;
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

public abstract class ReviewAbstract
{
    public string Text { get; set; }
    public int Rating { get; set; }
    public UserAbstract Author { get; set; }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append($"Author: {Author.Nickname}\n");
        sb.Append($"Rating: {Rating}\n");
        sb.Append($"Text: {Text}");

        return sb.ToString();
    }
}

public abstract class ModAbstract
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<UserAbstract> Authors { get; set; }
    public List<ModAbstract> Compatibility { get; set; }
    
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

public abstract class UserAbstract
{
    public string Nickname { get; set; }
    public List<GameAbstract> OwnedGames { get; set; }

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