using System.Collections.ObjectModel;

namespace ConsoleApp;
using System.Collections.Generic;


// List<Tuple<string, Object>> reprezentacja (czesc 7):
public class GameTuple
{
    private List<Tuple<string, Object>> _myLisOfPairs = new List<Tuple<string, Object>>();
    
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
    
    public virtual void SetName(string name)
    {
        _addNewPair(new Tuple<string, object>("name", name));
    }
    public virtual string GetName()
    {
        throw new NotImplementedException();
    }

    public virtual void SetGenre(string genre)
    {
        _addNewPair(new Tuple<string, object>("genre", genre));
    }
    public virtual string GetGenre()
    {
        throw new NotImplementedException();
    }

    public virtual void SetDevices(string devices)
    {
        _addNewPair(new Tuple<string, object>("devices", devices));
    }
    public virtual string GetDevices()
    {
        throw new NotImplementedException();
    }

    public virtual void SetAuthors(List<UserTuple>? authors)
    {
        _addNewPair(new Tuple<string, object>("authors", authors ?? new List<UserTuple>()));
    }
    public virtual List<UserTuple> GetAuthors()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetReviews(List<ReviewTuple>? reviews)
    {
        _addNewPair(new Tuple<string, object>("reviews", reviews ?? new List<ReviewTuple>()));
    }
    public virtual List<ReviewTuple> GetReviews()
    {
        throw new NotImplementedException();
    }
    public virtual void SetMods(List<ModTuple>? mods)
    {
        _addNewPair(new Tuple<string, object>("mods", mods ?? new List<ModTuple>()));
    }
    public virtual List<ModTuple> GetMods()
    {
        throw new NotImplementedException();
    }
}

public class ReviewTuple
{
    private List<Tuple<string, Object>> _myLisOfPairs = new List<Tuple<string, Object>>();

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

    public virtual void SetText(string text)
    {
        _addNewPair(new Tuple<string, object>("text", text));
    }
    public virtual string GetText()
    {
        throw new NotImplementedException();
    }

    public virtual void SetRating(int rating)
    {
        _addNewPair(new Tuple<string, object>("rating", rating));
    }
    public virtual int GetRating()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetAuthor(UserTuple? Author)
    {
        _addNewPair(new Tuple<string, object>("Author", Author ?? new UserTuple("")));
    }
    public virtual UserTuple GetAuthor()
    {
        throw new NotImplementedException();
    }
}

public class ModTuple
{
    private List<Tuple<string, Object>> _myLisOfPairs = new List<Tuple<string, Object>>();

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

    public virtual void SetName(string name)
    {
        _addNewPair(new Tuple<string, object>("name", name));
    }
    public virtual string GetName()
    {
        throw new NotImplementedException();
    }

    public virtual void SetDescription(string description)
    {
        _addNewPair(new Tuple<string, object>("description", description));
    }
    public virtual string GetDescription()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetAuthors(List<UserTuple>? authors)
    {
        _addNewPair(new Tuple<string, object>("authors", authors ?? new List<UserTuple>()));
    }
    public virtual List<UserTuple> GetAuthors()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetCompatibility(List<ModTuple>? mods)
    {
        _addNewPair(new Tuple<string, object>("mods", mods ?? new List<ModTuple>()));
    }
    public virtual List<ModTuple> GetCompatibility()
    {
        throw new NotImplementedException();
    }
}

public class UserTuple
{
    private List<Tuple<string, Object>> _myLisOfPairs = new List<Tuple<string, Object>>();

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
    
    public virtual void SetNickname(string nickname)
    {
        _addNewPair(new Tuple<string, object>("nickname", nickname));
    }
    public virtual string GetNickname()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetOwnedGames(List<GameTuple>? ownedGames)
    {
        _addNewPair(new Tuple<string, object>("ownedGames", ownedGames ?? new List<GameTuple>()));
    }
    public virtual List<GameTuple> GetOwnedGames()
    {
        throw new NotImplementedException();
    }
}
