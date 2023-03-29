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
        Authors = authors!;
        Reviews = reviews!;
        Mods = mods!;
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

    private List<UserHash> _authors;
    public virtual List<UserHash> Authors
    {
        get => _authors;
        set => _authors = value ?? new List<UserHash>();
    }

    private List<ReviewHash> _reviews;
    public virtual List<ReviewHash> Reviews
    {
        get => _reviews;
        set => _reviews = value ?? new List<ReviewHash>();
    }

    private List<ModHash> _mods;
    public virtual List<ModHash> Mods
    {
        get => _mods;
        set => _mods = value ?? new List<ModHash>();
    }
}

public class ReviewHash
{
    private readonly Dictionary<int, string> _myHashMap = new();
    private int _text;
    private int _rating;

    public ReviewHash(string text, int rating, UserHash? author = null)
    {
        SetText(text);
        SetRating(rating);
        Author = author!;
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

    private UserHash _author;
    public virtual UserHash Author
    {
        get => _author;
        set => _author = value ?? new UserHash("");
    }
}

public class ModHash
{
    private readonly Dictionary<int, string> _myHashMap = new();
    private int _name;
    private int _description;

    public ModHash(string name, string description, List<UserHash>? authors = null, List<ModHash>? compatibility = null)
    {
        SetName(name);
        SetDescription(description);
        Authors = authors!;
        Compatibility = compatibility!;
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

    private List<UserHash> _authors;
    public virtual List<UserHash> Authors
    {
        get => _authors;
        set => _authors = value ?? new List<UserHash>();
    }

    private List<ModHash> _compatibility;
    public virtual List<ModHash> Compatibility
    {
        get => _compatibility;
        set => _compatibility = value ?? new List<ModHash>();
    }
}

public class UserHash
{
    private readonly Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _nickname;

    public UserHash(string nickname, List<GameHash>? ownedGames = null)
    {
        SetNickname(nickname);
        OwnedGames = ownedGames!;
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

    private List<GameHash> _ownedGames;
    public virtual List<GameHash> OwnedGames
    {
        get => _ownedGames;
        set => _ownedGames = value ?? new List<GameHash>();
    }
}
