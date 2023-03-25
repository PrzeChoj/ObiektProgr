using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace ConsoleApp;
using System.Collections.Generic;

// Adaptery z brzydkiej do ladnej
public class AdapterGameFromHash : Game
{
    private readonly GameHash _g;

    public AdapterGameFromHash(GameHash g)
    {
        if (g == null)
        {
            throw new Exception("g is null");
        }
        this._g = g;
    }

    public override string Name
    {
        get => _g.GetHashMap()[_g.GetName()];
        set
        {
            _g.SetName(value);
        }
    }
    public override string Genre
    {
        get => _g.GetHashMap()[_g.GetGenre()];
        set
        {
            _g.SetGenre(value);
        }
    }
    public override string Devices
    {
        get => _g.GetHashMap()[_g.GetDevices()];
        set
        {
            _g.SetDevices(value);
        }
    }
    
    public override List<User> Authors
    {
        get
        {
            var outAuthors = new List<User>();
            foreach (UserHash gAuthor in _g.Authors)
            {
                outAuthors.Add(new AdapterUserFromHash(gAuthor));
            }

            return outAuthors;
        }
        set
        {
            var newAuthorsList = new List<UserHash>();
            foreach (User user in value)
            {
                newAuthorsList.Add(new AdapterUserToHash(user));
            }
            _g.Authors = newAuthorsList;
        }
    }

    public override List<Review> Reviews
    {
        get
        {
            var outRev = new List<Review>();
            foreach (ReviewHash gRev in _g.Reviews)
            {
                outRev.Add(new AdapterReviewFromHash(gRev));
            }

            return outRev;
        }
        set
        {
            var newReviewsList = new List<ReviewHash>();
            foreach (Review review in value)
            {
                newReviewsList.Add(new AdapterReviewToHash(review));
            }
            _g.Reviews = newReviewsList;
        }
    }

    public override List<Mod> Mods
    {
        get
        {
            var outRev = new List<Mod>();
            foreach (ModHash gMod in _g.Mods)
            {
                outRev.Add(new AdapterModFromHash(gMod));
            }

            return outRev;
        }
        set
        {
            var newModsList = new List<ModHash>();
            foreach (Mod mod in value)
            {
                newModsList.Add(new AdapterModToHash(mod));
            }
            _g.Mods = newModsList;
        }
    }
}

public class AdapterReviewFromHash : Review
{
    private readonly ReviewHash _r;

    public AdapterReviewFromHash(ReviewHash r)
    {
        if (r == null)
        {
            throw new Exception("r is null");
        }
        this._r = r;
    }
    
    public override string Text
    {
        get => _r.GetHashMap()[_r.GetText()];
        set
        {
            _r.SetText(value);
        }
    }
    public override int Rating
    {
        get => int.Parse(_r.GetHashMap()[_r.GetRating()]);
        set
        {
            _r.SetRating(value);
        }
    }
    
    public override User Author
    {
        get
        {
            return new AdapterUserFromHash(_r.Author);
        }
        set
        {
            _r.Author = new AdapterUserToHash(value);
        }
    }
}

public class AdapterModFromHash : Mod
{
    private readonly ModHash _m;

    public AdapterModFromHash(ModHash m)
    {
        if (m == null)
        {
            throw new Exception("m is null");
        }
        this._m = m;
    }
    
    public override string Name
    {
        get => _m.GetHashMap()[_m.GetName()];
        set
        {
            _m.SetName(value);
        }
    }
    public override string Description
    {
        get => _m.GetHashMap()[_m.GetDescription()];
        set
        {
            _m.SetDescription(value);
        }
    }
    
    public override List<User> Authors
    {
        get
        {
            var outAuthors = new List<User>();
            foreach (UserHash gAuthor in _m.Authors)
            {
                outAuthors.Add(new AdapterUserFromHash(gAuthor));
            }

            return outAuthors;
        }
        set
        {
            var newAuthors = new List<UserHash>();
            foreach (User gAuthor in value)
            {
                newAuthors.Add(new AdapterUserToHash(gAuthor));
            }

            _m.Authors = newAuthors;
        }
    }

    public override List<Mod> Compatibility
    {
        get
        {
            var outMods = new List<Mod>();
            foreach (ModHash otherMod in _m.Compatibility)
            {
                outMods.Add(new AdapterModFromHash(otherMod));
            }

            return outMods;
        }
        set
        {
            var newMods = new List<ModHash>();
            foreach (Mod mod in value)
            {
                newMods.Add(new AdapterModToHash(mod));
            }

            _m.Compatibility = newMods;
        }
    }
}

public class AdapterUserFromHash : User
{
    private readonly UserHash _u;

    public AdapterUserFromHash(UserHash u)
    {
        if (u == null)
        {
            throw new Exception("u is null");
        }
        this._u = u;
    }
    
    public override string Nickname
    {
        get => _u.GetHashMap()[_u.GetNickname()];
        set
        {
            _u.SetNickname(value);
        }
    }
    
    public override List<Game> OwnedGames
    {
        get
        {
            var outGames = new List<Game>();
            foreach (GameHash game in _u.OwnedGames)
            {
                outGames.Add(new AdapterGameFromHash(game));
            }

            return outGames;
        }
        set
        {
            var newGames = new List<GameHash>();
            foreach (Game game in value)
            {
                newGames.Add(new AdapterGameToHash(game));
            }

            _u.OwnedGames = newGames;
        }
    }
}


// Adaptery z ladnej do brzydkiej
public class AdapterGameToHash : GameHash
{
    private readonly Game _g;
    
    public AdapterGameToHash(Game g)
    {
        if (g == null)
        {
            throw new Exception("g is null");
        }
        this._g = g;
    }
    
    internal override ReadOnlyDictionary<int, string> GetHashMap()
    {
        var outHashMap = new Dictionary<int, string>();
        outHashMap[_g.Name.GetHashCode()] = _g.Name;
        outHashMap[_g.Genre.GetHashCode()] = _g.Genre;
        outHashMap[_g.Devices.GetHashCode()] = _g.Devices;

        return outHashMap.AsReadOnly();
    }
    
    public override void SetName(string name)
    {
        _g.Name = name;
    }
    public override int GetName()
    {
        return _g.Name.GetHashCode();
    }

    public override void SetGenre(string genre)
    {
        _g.Genre = genre;
    }
    public override int GetGenre()
    {
        return _g.Genre.GetHashCode();
    }

    public override void SetDevices(string devices)
    {
        _g.Devices = devices;
    }
    public override int GetDevices()
    {
        return _g.Devices.GetHashCode();
    }

    public override List<UserHash> Authors
    {
        get
        {
            var outAuthors = new List<UserHash>();
            foreach (User author in _g.Authors)
            {
                outAuthors.Add(new AdapterUserToHash(author));
            }

            return outAuthors;
        }
        set
        {
            var newAuthors = new List<User>();
            foreach (UserHash author in value)
            {
                newAuthors.Add(new AdapterUserFromHash(author));
            }

            _g.Authors = newAuthors;
        }
    }

    public override List<ReviewHash> Reviews
    {
        get
        {
            var outReviews = new List<ReviewHash>();
            foreach (Review review in _g.Reviews)
            {
                outReviews.Add(new AdapterReviewToHash(review));
            }

            return outReviews;
        }
        set
        {
            var newReviews = new List<Review>();
            foreach (ReviewHash review in value)
            {
                newReviews.Add(new AdapterReviewFromHash(review));
            }

            _g.Reviews = newReviews;
        }
    }

    public override List<ModHash> Mods
    {
        get
        {
            var outMods = new List<ModHash>();
            foreach (Mod mod in _g.Mods)
            {
                outMods.Add(new AdapterModToHash(mod));
            }

            return outMods;
        }
        set
        {
            var newMods = new List<Mod>();
            foreach (ModHash mod in value)
            {
                newMods.Add(new AdapterModFromHash(mod));
            }

            _g.Mods = newMods;
        }
    }
}

public class AdapterReviewToHash : ReviewHash
{
    private readonly Review _r;
    
    public AdapterReviewToHash(Review review)
    {
        _r = review;
    }
    
    internal override ReadOnlyDictionary<int, string> GetHashMap()
    {
        var outHashMap = new Dictionary<int, string>();
        outHashMap[_r.Text.GetHashCode()] = _r.Text;
        
        string stringRating = _r.Rating.ToString();
        outHashMap[stringRating.GetHashCode()] = stringRating;
        
        return outHashMap.AsReadOnly();
    }

    public override void SetText(string text)
    {
        _r.Text = text;
    }
    public override int GetText()
    {
        return _r.Text.GetHashCode();
    }

    public override void SetRating(int rating)
    {
        _r.Rating = rating;
    }
    public override int GetRating()
    {
        // Przerobic rating na hasha ze stringa
        return _r.Rating.ToString().GetHashCode();
    }

    public override UserHash Author
    {
        get
        {
            return new AdapterUserToHash(_r.Author);
        }
        set
        {
            _r.Author = new AdapterUserFromHash(value);
        }
    }
}

public class AdapterModToHash : ModHash
{
    private readonly Mod _m;
    
    public AdapterModToHash(Mod m)
    {
        if (m == null)
        {
            throw new Exception("m is null");
        }
        this._m = m;
    }
    
    internal override ReadOnlyDictionary<int, string> GetHashMap()
    {
        var outHashMap = new Dictionary<int, string>();
        outHashMap[_m.Name.GetHashCode()] = _m.Name;
        outHashMap[_m.Description.GetHashCode()] = _m.Description;

        return outHashMap.AsReadOnly();
    }
    
    public override void SetName(string name)
    {
        _m.Name = name;
    }
    public override int GetName()
    {
        return _m.Name.GetHashCode();
    }

    public override void SetDescription(string description)
    {
        _m.Description = description;
    }
    public override int GetDescription()
    {
        return _m.Description.GetHashCode();
    }

    public override List<UserHash> Authors
    {
        get
        {
            var outAuthors = new List<UserHash>();
            foreach (User author in _m.Authors)
            {
                outAuthors.Add(new AdapterUserToHash(author));
            }

            return outAuthors;
        }
        set
        {
            var newAuthors = new List<User>();
            foreach (UserHash author in value)
            {
                newAuthors.Add(new AdapterUserFromHash(author));
            }

            _m.Authors = newAuthors;
        }
    }

    public override List<ModHash> Compatibility
    {
        get
        {
            var outMods = new List<ModHash>();
            foreach (Mod mod in _m.Compatibility)
            {
                outMods.Add(new AdapterModToHash(mod));
            }

            return outMods;
        }
        set
        {
            var newMods = new List<Mod>();
            foreach (ModHash mod in value)
            {
                newMods.Add(new AdapterModFromHash(mod));
            }

            _m.Compatibility = newMods;
        }
    }
}

public class AdapterUserToHash : UserHash
{
    private readonly User _u;
    public AdapterUserToHash(User user)
    {
        _u = user;
    }
    
    internal override ReadOnlyDictionary<int, string> GetHashMap()
    {
        var outHashMap = new Dictionary<int, string>();
        outHashMap[_u.Nickname.GetHashCode()] = _u.Nickname;

        return outHashMap.AsReadOnly();
    }

    public override void SetNickname(string nickname)
    {
        _u.Nickname = nickname;
    }
    public override int GetNickname()
    {
        return _u.Nickname.GetHashCode();
    }

    public override List<GameHash> OwnedGames
    {
        get
        {
            var outGames = new List<GameHash>();
            foreach (Game game in _u.OwnedGames)
            {
                outGames.Add(new AdapterGameToHash(game));
            }

            return outGames;
        }
        set
        {
            var newGames = new List<Game>();
            foreach (GameHash game in value)
            {
                newGames.Add(new AdapterGameFromHash(game));
            }

            _u.OwnedGames = newGames;
        }
    }
}