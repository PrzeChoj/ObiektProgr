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
    
    public virtual void SetName(string name)
    {
        throw new NotImplementedException();
    }
    public virtual string GetName()
    {
        throw new NotImplementedException();
    }

    public virtual void SetGenre(string genre)
    {
        throw new NotImplementedException();
    }
    public virtual string GetGenre()
    {
        throw new NotImplementedException();
    }

    public virtual void SetDevices(string devices)
    {
        throw new NotImplementedException();
    }
    public virtual string GetDevices()
    {
        throw new NotImplementedException();
    }

    public virtual void SetAuthors(List<UserTuple>? authors)
    {
        throw new NotImplementedException();
    }
    public virtual List<UserTuple> GetAuthors()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetReviews(List<ReviewTuple>? reviews)
    {
        throw new NotImplementedException();
    }
    public virtual List<ReviewTuple> GetReviews()
    {
        throw new NotImplementedException();
    }
    public virtual void SetMods(List<ModTuple>? mods)
    {
        throw new NotImplementedException();
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

    public virtual void SetText(string text)
    {
        throw new NotImplementedException();
    }
    public virtual string GetText()
    {
        throw new NotImplementedException();
    }

    public virtual void SetRating(int rating)
    {
        throw new NotImplementedException();
    }
    public virtual int GetRating()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetAuthor(UserTuple? Author)
    {
        throw new NotImplementedException();
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

    public virtual void SetName(string name)
    {
        throw new NotImplementedException();
    }
    public virtual string GetName()
    {
        throw new NotImplementedException();
    }

    public virtual void SetDescription(string description)
    {
        throw new NotImplementedException();
    }
    public virtual string GetDescription()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetAuthors(List<UserTuple>? authors)
    {
        throw new NotImplementedException();
    }
    public virtual List<UserTuple> GetAuthors()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetCompatibility(List<ModTuple>? mods)
    {
        throw new NotImplementedException();
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
    
    public virtual void SetNickname(string nickname)
    {
        throw new NotImplementedException();
    }
    public virtual string GetNickname()
    {
        throw new NotImplementedException();
    }
    
    public virtual void SetOwnedGames(List<GameTuple>? ownedGames)
    {
        throw new NotImplementedException();
    }
    public virtual List<GameTuple> GetOwnedGames()
    {
        throw new NotImplementedException();
    }
}
