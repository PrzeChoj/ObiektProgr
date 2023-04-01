namespace ConsoleApp;
using System.Collections.Generic;


// List<Tuple<string, Object>> reprezentacja (czesc 7):
public class GameTuple
{
    private readonly List<Tuple<string, Object>> _myLisOfPairs = new();
    
    public GameTuple(string name, string genre, string devices, List<UserTuple>? authors = null,
        List<ReviewTuple>? reviews = null, List<ModTuple>? mods = null)
    {
        SetName(name);
        SetGenre(genre);
        SetDevices(devices);
        SetAuthors(authors);
        SetReviews(reviews);
        SetMods(mods);
    }
    protected GameTuple() { }
    
    private void _addNewPair(Tuple<string, object> newPair)
    {
        for (int i = 0; i < _myLisOfPairs.Count; i++)
        {
            if (_myLisOfPairs[i].Item1 == newPair.Item1)
            {
                _myLisOfPairs[i] = newPair;
                return;
            }
        }
        _myLisOfPairs.Add(newPair);
    }

    private Object? _getPair(string key)
    {
        return (from pair in _myLisOfPairs where pair.Item1 == key select pair.Item2).FirstOrDefault();
    }
    
    public virtual void SetName(string name)
    {
        _addNewPair(new Tuple<string, object>("name", name));
    }
    public virtual string GetName()
    {
        return (string)_getPair("name")!; // This should not be null, but just in case
    }

    public virtual void SetGenre(string genre)
    {
        _addNewPair(new Tuple<string, object>("genre", genre));
    }
    public virtual string GetGenre()
    {
        return (string)_getPair("genre")!; // This should not be null, but just in case
    }

    public virtual void SetDevices(string devices)
    {
        _addNewPair(new Tuple<string, object>("devices", devices));
    }
    public virtual string GetDevices()
    {
        return (string)_getPair("devices")!; // This should not be null, but just in case
    }

    public virtual void SetAuthors(List<UserTuple>? authors)
    {
        _addNewPair(new Tuple<string, object>("authors", authors ?? new List<UserTuple>()));
    }
    public virtual List<UserTuple> GetAuthors()
    {
        return (List<UserTuple>)_getPair("authors")!; // This should not be null, but just in case
    }
    
    public virtual void SetReviews(List<ReviewTuple>? reviews)
    {
        _addNewPair(new Tuple<string, object>("reviews", reviews ?? new List<ReviewTuple>()));
    }
    public virtual List<ReviewTuple> GetReviews()
    {
        return (List<ReviewTuple>)_getPair("reviews")!; // This should not be null, but just in case
    }
    public virtual void SetMods(List<ModTuple>? mods)
    {
        _addNewPair(new Tuple<string, object>("mods", mods ?? new List<ModTuple>()));
    }
    public virtual List<ModTuple> GetMods()
    {
        return (List<ModTuple>)_getPair("mods")!; // This should not be null, but just in case
    }
}

public class ReviewTuple
{
    private readonly List<Tuple<string, Object>> _myLisOfPairs = new();

    public ReviewTuple(string text, int rating, UserTuple? author = null)
    {
        SetText(text);
        SetRating(rating);
        SetAuthor(author);
    }

    protected ReviewTuple() { }

    private void _addNewPair(Tuple<string, object> newPair)
    {
        for (int i = 0; i < _myLisOfPairs.Count; i++)
        {
            if (_myLisOfPairs[i].Item1 == newPair.Item1)
            {
                _myLisOfPairs[i] = newPair;
                return;
            }
        }
        _myLisOfPairs.Add(newPair);
    }
    
    private Object? _getPair(string key)
    {
        return (from pair in _myLisOfPairs where pair.Item1 == key select pair.Item2).FirstOrDefault();
    }

    public virtual void SetText(string text)
    {
        _addNewPair(new Tuple<string, object>("text", text));
    }
    public virtual string GetText()
    {
        return (string)_getPair("text")!; // This should not be null, but just in case
    }

    public virtual void SetRating(int rating)
    {
        _addNewPair(new Tuple<string, object>("rating", rating));
    }
    public virtual int GetRating()
    {
        return (int)_getPair("rating")!; // This should not be null, but just in case
    }
    
    public virtual void SetAuthor(UserTuple? author)
    {
        _addNewPair(new Tuple<string, object>("author", author ?? new UserTuple("")));
    }
    public virtual UserTuple GetAuthor()
    {
        return (UserTuple)_getPair("author")!; // This should not be null, but just in case
    }
}

public class ModTuple
{
    private readonly List<Tuple<string, Object>> _myLisOfPairs = new();

    public ModTuple(string name, string description, List<UserTuple>? authors = null, List<ModTuple>? compatibility = null)
    {
        SetName(name);
        SetDescription(description);
        SetAuthors(authors);
        SetCompatibility(compatibility);
    }

    protected ModTuple() { }
    
    private void _addNewPair(Tuple<string, object> newPair)
    {
        for (int i = 0; i < _myLisOfPairs.Count; i++)
        {
            if (_myLisOfPairs[i].Item1 == newPair.Item1)
            {
                _myLisOfPairs[i] = newPair;
                return;
            }
        }
        _myLisOfPairs.Add(newPair);
    }
    
    private Object? _getPair(string key)
    {
        foreach (var pair in _myLisOfPairs)
        {
            if (pair.Item1 == key)
                return pair.Item2;
        }

        return null;
    }

    public virtual void SetName(string name)
    {
        _addNewPair(new Tuple<string, object>("name", name));
    }
    public virtual string GetName()
    {
        return (string)_getPair("name")!; // This should not be null, but just in case
    }

    public virtual void SetDescription(string description)
    {
        _addNewPair(new Tuple<string, object>("description", description));
    }
    public virtual string GetDescription()
    {
        return (string)_getPair("description")!; // This should not be null, but just in case
    }
    
    public virtual void SetAuthors(List<UserTuple>? authors)
    {
        _addNewPair(new Tuple<string, object>("authors", authors ?? new List<UserTuple>()));
    }
    public virtual List<UserTuple> GetAuthors()
    {
        return (List<UserTuple>)_getPair("authors")!; // This should not be null, but just in case
    }
    
    public virtual void SetCompatibility(List<ModTuple>? mods)
    {
        _addNewPair(new Tuple<string, object>("mods", mods ?? new List<ModTuple>()));
    }
    public virtual List<ModTuple> GetCompatibility()
    {
        return (List<ModTuple>)_getPair("mods")!; // This should not be null, but just in case
    }
}

public class UserTuple
{
    private readonly List<Tuple<string, Object>> _myLisOfPairs = new();

    public UserTuple(string nickname, List<GameTuple>? ownedGames = null)
    {
        SetNickname(nickname);
        SetOwnedGames(ownedGames);
    }
    protected UserTuple(){}
    
    private void _addNewPair(Tuple<string, object> newPair)
    {
        for (int i = 0; i < _myLisOfPairs.Count; i++)
        {
            if (_myLisOfPairs[i].Item1 == newPair.Item1)
            {
                _myLisOfPairs[i] = newPair;
                return;
            }
        }
        _myLisOfPairs.Add(newPair);
    }
    
    private Object? _getPair(string key)
    {
        foreach (var pair in _myLisOfPairs)
        {
            if (pair.Item1 == key)
                return pair.Item2;
        }

        return null;
    }
    
    public virtual void SetNickname(string nickname)
    {
        _addNewPair(new Tuple<string, object>("nickname", nickname));
    }
    public virtual string GetNickname()
    {
        return (string)_getPair("nickname")!; // This should not be null, but just in case
    }
    
    public virtual void SetOwnedGames(List<GameTuple>? ownedGames)
    {
        _addNewPair(new Tuple<string, object>("ownedGames", ownedGames ?? new List<GameTuple>()));
    }
    public virtual List<GameTuple> GetOwnedGames()
    {
        return (List<GameTuple>)_getPair("ownedGames")!; // This should not be null, but just in case
    }
}
