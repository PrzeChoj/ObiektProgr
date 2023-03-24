using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;


/// HashMap reprezentacja (czesc 4):
public class GameHash : IGameHash
{
    internal Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _name;
    private int _genre;
    private int _devices;

    public GameHash(string name, string genre, string devices, List<IUserHash>? authors = null, List<IReviewHash>? reviews = null, List<IModHash>? mods = null)
    {
        SetName(name);
        SetGenre(genre);
        SetDevices(devices);
        Authors = authors ?? new List<IUserHash>();
        Reviews = reviews ?? new List<IReviewHash>();
        Mods = mods ?? new List<IModHash>();
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
    
    public List<IUserHash> Authors { get; set; }
    public List<IReviewHash> Reviews { get; set; }
    public List<IModHash> Mods { get; set; }
}

public class ReviewHash : IReviewHash
{
    internal Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _text;
    private int _rating;

    public ReviewHash(string text, int rating, IUserHash author)
    {
        SetText(text);
        SetRating(rating);
        Author = author;
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
    
    public IUserHash Author { get; set; }
}

public class ModHash : IModHash
{
    internal Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _name;
    private int _description;

    public ModHash(string name, string description, List<IUserHash>? authors = null, List<IModHash>? compatibility = null)
    {
        SetName(name);
        SetDescription(description);
        Authors = authors ?? new List<IUserHash>();
        Compatibility = compatibility ?? new List<IModHash>();
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
    
    public List<IUserHash> Authors { get; set; }
    public List<IModHash> Compatibility { get; set; }
}

public class UserHash : IUserHash
{
    internal Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    private int _nickname;

    public UserHash(string nickname, List<IGameHash>? ownedGames = null)
    {
        SetNickname(nickname);
        OwnedGames = ownedGames ?? new List<IGameHash>();
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
    
    public List<IGameHash> OwnedGames { get; set; }
}
