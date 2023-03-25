using System.Collections.ObjectModel;

namespace ConsoleApp;
using System.Collections.Generic;


// HashMap reprezentacja (czesc 4):
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
    protected GameHash() { }

    internal virtual ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }
    
    public virtual void SetName(string name)
    {
        _name = name.GetHashCode();
        _myHashMap[_name] = name;
    }
    public virtual int GetName()
    {
        return _name;
    }

    public virtual void SetGenre(string genre)
    {
        _genre = genre.GetHashCode();
        _myHashMap[_genre] = genre;
    }
    public virtual int GetGenre()
    {
        return _genre;
    }

    public virtual void SetDevices(string devices)
    {
        _devices = devices.GetHashCode();
        _myHashMap[_devices] = devices;
    }
    public virtual int GetDevices()
    {
        return _devices;
    }
    
    public virtual List<UserHash> Authors { get; set; }
    public virtual List<ReviewHash> Reviews { get; set; }
    public virtual List<ModHash> Mods { get; set; }
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

    protected ReviewHash() { }

    internal virtual ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }

    public virtual void SetText(string text)
    {
        _text = text.GetHashCode();
        _myHashMap[_text] = text;
    }
    public virtual int GetText()
    {
        return _text;
    }

    public virtual void SetRating(int rating)
    {
        string stringRating = rating.ToString();
        _rating = stringRating.GetHashCode();
        _myHashMap[_rating] = stringRating;
    }
    public virtual int GetRating()
    {
        return _rating;
    }
    
    public virtual UserHash Author { get; set; }
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

    protected ModHash() { }

    internal virtual ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }
    
    public virtual void SetName(string name)
    {
        _name = name.GetHashCode();
        _myHashMap[_name] = name;
    }
    public virtual int GetName()
    {
        return _name;
    }

    public virtual void SetDescription(string description)
    {
        _description = description.GetHashCode();
        _myHashMap[_description] = description;
    }
    public virtual int GetDescription()
    {
        return _description;
    }
    
    public virtual List<UserHash> Authors { get; set; }
    public virtual List<ModHash> Compatibility { get; set; }
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
    protected UserHash(){}
    
    internal virtual ReadOnlyDictionary<int, string> GetHashMap()
    {
        return _myHashMap.AsReadOnly();
    }

    public virtual void SetNickname(string nickname)
    {
        _nickname = nickname.GetHashCode();
        _myHashMap[_nickname] = nickname;
    }
    public virtual int GetNickname()
    {
        return _nickname;
    }
    
    public virtual List<GameHash> OwnedGames { get; set; }
}
