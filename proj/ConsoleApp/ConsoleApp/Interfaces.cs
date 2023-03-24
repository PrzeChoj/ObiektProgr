using System.Net.Mime;
using System.Text;
using ConsoleApp;

public interface IGame
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Devices { get; set; }
    public List<IUser> Authors { get; set; }
    public List<IReview> Reviews { get; set; }
    public List<IMod> Mods { get; set; }

    public string ToString()
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

public interface IReview
{
    public string Text { get; set; }
    public int Rating { get; set; }
    public IUser Author { get; set; }
    
    public string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Author: {Author.Nickname}");
        sb.AppendLine($"Rating: {Rating}");
        sb.AppendLine($"Text: {Text}");

        return sb.ToString();
    }
}

public interface IMod
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IUser> Authors { get; set; }
    public List<IMod> Compatibility { get; set; }
    
    public string ToString()
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

public interface IUser
{
    public string Nickname { get; set; }
    public List<IGame> OwnedGames { get; set; }
    
    public string ToString()
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