namespace ConsoleApp;
using System.Collections.Generic;


/// HashMap reprezentacja (czesc 4):
public class GameHash
{
    private Dictionary<int, string> _myHashMap = new Dictionary<int, string>();
    
    private int _name;
    public int getName()
    {
        return _name;
    }
    public void setName(string name)
    {
        _name = name.GetHashCode();
        _myHashMap[_name] = name;
    }
    
    private int _genre;
    public int getGenre()
    {
        return _genre;
    }
    public void setGenre(string genre)
    {
        _genre = genre.GetHashCode();
        _myHashMap[_genre] = genre;
    }
    
    
    public List<User> Authors { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Mod> Mods { get; set; }
    
    private int _devices;
    public int getDevices()
    {
        return _devices;
    }
    public void setDevices(string devices)
    {
        _devices = devices.GetHashCode();
        _myHashMap[_devices] = devices;
    }
}

public class ReviewHash
{
    private Dictionary<int, string> myHashMap = new Dictionary<int, string>();
    
    private int _text;
    public int getText()
    {
        return _text;
    }
    public void setText(string text)
    {
        _text = text.GetHashCode();
        myHashMap[_text] = text;
    }
    
    private int _rating;
    public int getRating()
    {
        return _rating;
    }
    public void setRating(int rating)
    {
        string stringRating = rating.ToString();
        _text = stringRating.GetHashCode();
        myHashMap[_text] = stringRating;
    }
    
    public User Author { get; set; }
}

public class ModHash
{
    private Dictionary<int, string> myHashMap = new Dictionary<int, string>();

    private int _name;
    public int getName()
    {
        return _name;
    }
    public void setName(string name)
    {
        _name = name.GetHashCode();
        myHashMap[_name] = name;
    }

    private int _description;
    public int getDescription()
    {
        return _description;
    }
    public void setDescription(string description)
    {
        _description = description.GetHashCode();
        myHashMap[_description] = description;
    }
    
    public List<User> Authors { get; set; }
    public List<Mod> Compatibility { get; set; }
}

public class UserHash
{
    private Dictionary<int, string> myHashMap = new Dictionary<int, string>();

    private int _nickname;
    public int getNickname()
    {
        return _nickname;
    }
    public void setNickname(string nickname)
    {
        _nickname = nickname.GetHashCode();
        myHashMap[_nickname] = nickname;
    }
    
    public List<Game> OwnedGames { get; set; }
}
