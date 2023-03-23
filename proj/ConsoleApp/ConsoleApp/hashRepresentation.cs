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

    public GameHash(string name, string genre, string devices, List<UserHash> authors, List<ReviewHash> reviews, List<ModHash> mods)
    {
        Name = name;
        Genre = genre;
        Devices = devices;
        Authors = authors;
        Reviews = reviews;
        Mods = mods;
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
            _text = stringRating.GetHashCode();
            _myHashMap[_text] = stringRating;
        }
    }
    
    public UserHash Author { get; set; }
}

public class ModHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _name;
    private int _description;

    public ModHash(string name, string description, List<UserHash> authors, List<ModHash> compatibility)
    {
        Name = name;
        Description = description;
        Authors = authors;
        Compatibility = compatibility;
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
}

public class UserHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _nickname;

    public UserHash(string nickname, List<GameHash> ownedGames)
    {
        Nickname = nickname;
        OwnedGames = ownedGames;
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
}