using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;


/// HashMap reprezentacja (czesc 4):
public class GameHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _name;
    private int _genre;
    private int _devices;

    public GameHash(string name, string genre, string devices, List<UserHash>? authors = null, List<ReviewHash>? reviews = null, List<ModHash>? mods = null)
    {
        Name = name;
        Genre = genre;
        Devices = devices;
        Authors = authors ?? new List<UserHash>();
        Reviews = reviews ?? new List<ReviewHash>();
        Mods = mods ?? new List<ModHash>();
    }
    
    public string Name
    {
        get => _myHashMap[_name];
        set
        {
            _name = value.GetHashCode();
            _myHashMap[_name] = value;
        }
    }

    public string Genre
    {
        get => _myHashMap[_genre];
        set
        {
            _genre = value.GetHashCode();
            _myHashMap[_genre] = value;
        }
    }

    public string Devices
    {
        get => _myHashMap[_devices];
        set
        {
            _devices = value.GetHashCode();
            _myHashMap[_devices] = value;
        }
    }
    
    public List<UserHash> Authors { get; set; }
    public List<ReviewHash> Reviews { get; set; }
    public List<ModHash> Mods { get; set; }
    
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

public class ReviewHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _text;
    private int _rating;

    public ReviewHash(string text, int rating, UserHash author)
    {
        Text = text;
        Rating = rating;
        Author = author;
    }

    public string Text {
        get => _myHashMap[_rating];
        set
        {
            _text = value.GetHashCode();
            _myHashMap[_text] = value;
        }
    }

    public int Rating
    {
        get => int.Parse(_myHashMap[_rating]);
        set
        {
            string stringRating = value.ToString();
            _rating = stringRating.GetHashCode();
            _myHashMap[_text] = stringRating;
        }
    }
    
    public UserHash Author { get; set; }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Author: {Author.Nickname}");
        sb.AppendLine($"Rating: {Rating}");
        sb.AppendLine($"Text: {Text}");

        return sb.ToString();
    }
}

public class ModHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _name;
    private int _description;

    public ModHash(string name, string description, List<UserHash>? authors = null, List<ModHash>? compatibility = null)
    {
        Name = name;
        Description = description;
        Authors = authors ?? new List<UserHash>();
        Compatibility = compatibility ?? new List<ModHash>();
    }

    public string Name
    {
        get => _myHashMap[_name];
        set
        {
            _name = value.GetHashCode();
            _myHashMap[_name] = value;
        }
    }
    
    public string Description { get => _myHashMap[_description];
        set
        {
            _description = value.GetHashCode();
            _myHashMap[_description] = value;
        }
    }

    public List<UserHash> Authors { get; set; }
    public List<ModHash> Compatibility { get; set; }
    
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

public class UserHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _nickname;

    public UserHash(string nickname, List<GameHash>? ownedGames = null)
    {
        Nickname = nickname;
        OwnedGames = ownedGames ?? new List<GameHash>();
    }
    
    public string Nickname {
        get => _myHashMap[_nickname];
        set
        {
            _nickname = value.GetHashCode();
            _myHashMap[_nickname] = value;
        }
    }
    
    public List<GameHash> OwnedGames { get; set; }
    
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
