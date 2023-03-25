using System.Collections.ObjectModel;
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

    public GameHash(string name, string genre, string devices, List<UserHash>? authors = null,
        List<ReviewHash>? reviews = null, List<ModHash>? mods = null)
    {
        SetName(name);
        SetGenre(genre);
        SetDevices(devices);
        Authors = authors ?? new List<UserHash>();
        Reviews = reviews ?? new List<ReviewHash>();
        Mods = mods ?? new List<ModHash>();
    }

    internal ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }
    
    public void SetName(string name)
    {
        _name = name.GetHashCode();
        _myHashMap[_name] = name;
    }
    public int GetName()
    {
        return _name;
    }

    public void SetGenre(string genre)
    {
        _genre = genre.GetHashCode();
        _myHashMap[_genre] = genre;
    }
    public int GetGenre()
    {
        return _genre;
    }

    public void SetDevices(string devices)
    {
        _devices = devices.GetHashCode();
        _myHashMap[_devices] = devices;
    }
    public int GetDevices()
    {
        return _devices;
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
        SetText(text);
        SetRating(rating);
        Author = author;
    }
    
    internal ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }

    public void SetText(string text)
    {
        _text = text.GetHashCode();
        _myHashMap[_text] = text;
    }
    public int GetText()
    {
        return _text;
    }

    public void SetRating(int rating)
    {
        string stringRating = rating.ToString();
        _rating = stringRating.GetHashCode();
        _myHashMap[_rating] = stringRating;
    }
    public int GetRating()
    {
        return _rating;
    }
    
    public UserHash Author { get; set; }
}

public class ModHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _name;
    private int _description;

    public ModHash(string name, string description, List<UserHash>? authors = null, List<ModHash>? compatibility = null)
    {
        SetName(name);
        SetDescription(description);
        Authors = authors ?? new List<UserHash>();
        Compatibility = compatibility ?? new List<ModHash>();
    }

    internal ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }
    
    public void SetName(string name)
    {
        _name = name.GetHashCode();
        _myHashMap[_name] = name;
    }
    public int GetName()
    {
        return _name;
    }

    public void SetDescription(string description)
    {
        _description = description.GetHashCode();
        _myHashMap[_description] = description;
    }
    public int GetDescription()
    {
        return _description;
    }
    
    public List<UserHash> Authors { get; set; }
    public List<ModHash> Compatibility { get; set; }
}

public class UserHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _nickname;

    public UserHash(string nickname, List<GameHash>? ownedGames = null)
    {
        SetNickname(nickname);
        OwnedGames = ownedGames ?? new List<GameHash>();
    }
    
    internal ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }

    public void SetNickname(string nickname)
    {
        _nickname = nickname.GetHashCode();
        _myHashMap[_nickname] = nickname;
    }
    public int GetNickname()
    {
        return _nickname;
    }
    
    public List<GameHash> OwnedGames { get; set; }
}
