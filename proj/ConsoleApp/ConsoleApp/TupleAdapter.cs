namespace ConsoleApp;
using System.Collections.Generic;

// Adaptery z brzydkiej do ladnej
public class AdapterGameFromTuple : Game
{
    private readonly GameTuple _g;

    public AdapterGameFromTuple(GameTuple g)
    {
        if (g == null)
        {
            throw new Exception("g is null");
        }
        this._g = g;
    }

    public override string Name
    {
        get => _g.GetName();
        set => _g.SetName(value);
    }
    public override string Genre
    {
        get => _g.GetGenre();
        set => _g.SetGenre(value);
    }
    public override string Devices
    {
        get => _g.GetDevices();
        set => _g.SetDevices(value);
    }
    
    public override List<User> Authors
    {
        get
        {
            var outAuthors = new List<User>();
            foreach (UserTuple gAuthor in _g.GetAuthors())
            {
                outAuthors.Add(new AdapterUserFromTuple(gAuthor));
            }

            return outAuthors;
        }
        set
        {
            var newAuthorsList = new List<UserTuple>();
            foreach (User user in value)
            {
                newAuthorsList.Add(new AdapterUserToTuple(user));
            }
            _g.SetAuthors(newAuthorsList);
        }
    }

    public override List<Review> Reviews
    {
        get
        {
            var outRev = new List<Review>();
            foreach (ReviewTuple gRev in _g.GetReviews())
            {
                outRev.Add(new AdapterReviewFromTuple(gRev));
            }

            return outRev;
        }
        set
        {
            var newReviewsList = new List<ReviewTuple>();
            foreach (Review review in value)
            {
                newReviewsList.Add(new AdapterReviewToTuple(review));
            }
            _g.SetReviews(newReviewsList);
        }
    }

    public override List<Mod> Mods
    {
        get
        {
            var outRev = new List<Mod>();
            foreach (ModTuple gMod in _g.GetMods())
            {
                outRev.Add(new AdapterModFromTuple(gMod));
            }

            return outRev;
        }
        set
        {
            var newModsList = new List<ModTuple>();
            foreach (Mod mod in value)
            {
                newModsList.Add(new AdapterModToTuple(mod));
            }
            _g.SetMods(newModsList);
        }
    }
}

public class AdapterReviewFromTuple : Review
{
    private readonly ReviewTuple _r;

    public AdapterReviewFromTuple(ReviewTuple r)
    {
        if (r == null)
        {
            throw new Exception("r is null");
        }
        this._r = r;
    }
    
    public override string Text
    {
        get => _r.GetText();
        set => _r.SetText(value);
    }
    public override int Rating
    {
        get => _r.GetRating();
        set => _r.SetRating(value);
    }
    
    public override User Author
    {
        get
        {
            return new AdapterUserFromTuple(_r.GetAuthor());
        }
        set => _r.SetAuthor(new AdapterUserToTuple(value));
    }
}

public class AdapterModFromTuple : Mod
{
    private readonly ModTuple _m;

    public AdapterModFromTuple(ModTuple m)
    {
        if (m == null)
        {
            throw new Exception("m is null");
        }
        this._m = m;
    }
    
    public override string Name
    {
        get => _m.GetName();
        set => _m.SetName(value);
    }
    public override string Description
    {
        get => _m.GetDescription();
        set => _m.SetDescription(value);
    }
    
    public override List<User> Authors
    {
        get
        {
            var outAuthors = new List<User>();
            foreach (UserTuple gAuthor in _m.GetAuthors())
            {
                outAuthors.Add(new AdapterUserFromTuple(gAuthor));
            }

            return outAuthors;
        }
        set
        {
            var newAuthors = new List<UserTuple>();
            foreach (User gAuthor in value)
            {
                newAuthors.Add(new AdapterUserToTuple(gAuthor));
            }

            _m.SetAuthors(newAuthors);
        }
    }

    public override List<Mod> Compatibility
    {
        get
        {
            var outMods = new List<Mod>();
            foreach (ModTuple otherMod in _m.GetCompatibility())
            {
                outMods.Add(new AdapterModFromTuple(otherMod));
            }

            return outMods;
        }
        set
        {
            var newMods = new List<ModTuple>();
            foreach (Mod mod in value)
            {
                newMods.Add(new AdapterModToTuple(mod));
            }

            _m.SetCompatibility(newMods);
        }
    }
}

public class AdapterUserFromTuple : User
{
    private readonly UserTuple _u;

    public AdapterUserFromTuple(UserTuple u)
    {
        if (u == null)
        {
            throw new Exception("u is null");
        }
        this._u = u;
    }
    
    public override string Nickname
    {
        get => _u.GetNickname();
        set => _u.SetNickname(value);
    }
    
    public override List<Game> OwnedGames
    {
        get
        {
            var outGames = new List<Game>();
            foreach (GameTuple game in _u.GetOwnedGames())
            {
                outGames.Add(new AdapterGameFromTuple(game));
            }

            return outGames;
        }
        set
        {
            var newGames = new List<GameTuple>();
            foreach (Game game in value)
            {
                newGames.Add(new AdapterGameToTuple(game));
            }

            _u.SetOwnedGames(newGames);
        }
    }
}


// Adaptery z ladnej do brzydkiej
public class AdapterGameToTuple : GameTuple
{
    private readonly Game _g;
    
    public AdapterGameToTuple(Game g)
    {
        if (g == null)
        {
            throw new Exception("g is null");
        }
        this._g = g;
    }
    
    public override void SetName(string name)
    {
        _g.Name = name;
    }
    public override string GetName()
    {
        return _g.Name;
    }

    public override void SetGenre(string genre)
    {
        _g.Genre = genre;
    }
    public override string GetGenre()
    {
        return _g.Genre;
    }

    public override void SetDevices(string devices)
    {
        _g.Devices = devices;
    }
    public override string GetDevices()
    {
        return _g.Devices;
    }

    public override void SetAuthors(List<UserTuple>? authors)
    {
        var newAuthors = new List<User>();
        
        if (authors == null)
        {
            _g.Authors = newAuthors;
            return;
        }
        
        foreach (UserTuple author in authors)
        {
            newAuthors.Add(new AdapterUserFromTuple(author));
        }
        _g.Authors = newAuthors;
    }

    public override List<UserTuple> GetAuthors()
    {
        var outAuthors = new List<UserTuple>();
        foreach (User author in _g.Authors)
        {
            outAuthors.Add(new AdapterUserToTuple(author));
        }
        return outAuthors;
    }

    public override void SetReviews(List<ReviewTuple>? reviews)
    {
        var newReviews = new List<Review>();
        
        if (reviews == null)
        {
            _g.Reviews = newReviews;
            return;
        }
        
        foreach (ReviewTuple review in reviews)
        {
            newReviews.Add(new AdapterReviewFromTuple(review));
        }
        _g.Reviews = newReviews;
    }

    public override List<ReviewTuple> GetReviews()
    {
        var outReviews = new List<ReviewTuple>();
        foreach (Review review in _g.Reviews)
        {
            outReviews.Add(new AdapterReviewToTuple(review));
        }
        return outReviews;
    }

    public override void SetMods(List<ModTuple>? mods)
    {
        var newMods = new List<Mod>();
        
        if (mods == null)
        {
            _g.Mods = newMods;
            return;
        }
        
        foreach (ModTuple mod in mods)
        {
            newMods.Add(new AdapterModFromTuple(mod));
        }
        _g.Mods = newMods;
    }

    public override List<ModTuple> GetMods()
    {
        var outMods = new List<ModTuple>();
        foreach (Mod mod in _g.Mods)
        {
            outMods.Add(new AdapterModToTuple(mod));
        }
        return outMods;
    }
}

public class AdapterReviewToTuple : ReviewTuple
{
    private readonly Review _r;
    
    public AdapterReviewToTuple(Review review)
    {
        _r = review;
    }
    
    public override void SetText(string text)
    {
        _r.Text = text;
    }
    public override string GetText()
    {
        return _r.Text;
    }

    public override void SetRating(int rating)
    {
        _r.Rating = rating;
    }
    public override int GetRating()
    {
        return _r.Rating;
    }

    public override void SetAuthor(UserTuple? author)
    {
        _r.Author = new AdapterUserFromTuple(author ?? new UserTuple(""));
    }
    public override UserTuple GetAuthor()
    {
        return new AdapterUserToTuple(_r.Author);
    }
}

public class AdapterModToTuple : ModTuple
{
    private readonly Mod _m;
    
    public AdapterModToTuple(Mod m)
    {
        if (m == null)
        {
            throw new Exception("m is null");
        }
        this._m = m;
    }
    
    public override void SetName(string name)
    {
        _m.Name = name;
    }
    public override string GetName()
    {
        return _m.Name;
    }

    public override void SetDescription(string description)
    {
        _m.Description = description;
    }
    public override string GetDescription()
    {
        return _m.Description;
    }

    public override void SetAuthors(List<UserTuple>? authors)
    {
        var newAuthors = new List<User>();
        
        if (authors == null)
        {
            _m.Authors = newAuthors;
            return;
        }
        
        foreach (UserTuple author in authors)
        {
            newAuthors.Add(new AdapterUserFromTuple(author));
        }
        _m.Authors = newAuthors;
    }

    public override List<UserTuple> GetAuthors()
    {
        var outAuthors = new List<UserTuple>();
        foreach (User author in _m.Authors)
        {
            outAuthors.Add(new AdapterUserToTuple(author));
        }
        return outAuthors;
    }

    public override void SetCompatibility(List<ModTuple>? mods)
    {
        var newMods = new List<Mod>();
        
        if (mods == null)
        {
            _m.Compatibility = newMods;
            return;
        }
        
        foreach (ModTuple mod in mods)
        {
            newMods.Add(new AdapterModFromTuple(mod));
        }
        _m.Compatibility = newMods;
    }

    public virtual List<ModTuple> GetCompatibility()
    {
        var outMods = new List<ModTuple>();
        foreach (Mod mod in _m.Compatibility)
        {
            outMods.Add(new AdapterModToTuple(mod));
        }
        return outMods;
    }
}

public class AdapterUserToTuple : UserTuple
{
    private readonly User _u;
    public AdapterUserToTuple(User user)
    {
        _u = user;
    }
    
    public override void SetNickname(string nickname)
    {
        _u.Nickname = nickname;
    }
    public override string GetNickname()
    {
        return _u.Nickname;
    }

    public override void SetOwnedGames(List<GameTuple>? ownedGames)
    {
        var newGames = new List<Game>();
        
        if (ownedGames == null)
        {
            _u.OwnedGames = newGames;
            return;
        }
        
        foreach (GameTuple game in ownedGames)
        {
            newGames.Add(new AdapterGameFromTuple(game));
        }
        _u.OwnedGames = newGames;
    }

    public override List<GameTuple> GetOwnedGames()
    {
        var outGames = new List<GameTuple>();
        foreach (Game game in _u.OwnedGames)
        {
            outGames.Add(new AdapterGameToTuple(game));
        }
        return outGames;
    }
}