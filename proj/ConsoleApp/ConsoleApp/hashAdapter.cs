using System.Text;

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
            /*
            var newGAuthors = new List<IUserHash>();
            foreach (IUser user in value)
            {
                List<IGameHash> ownedGames = new List<IGameHash>();
                foreach (IGame ownedGame in user.OwnedGames)
                {
                    ownedGames.Add();
                }
                newGAuthors.Add(new UserHash(user.Nickname, ownedGames));
            }
            
            g.Authors = newGAuthors;
            */

            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
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
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
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
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
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
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
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
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
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
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
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
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
        }
    }
}


// Adaptery z ladnej do brzydkiej
