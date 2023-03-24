using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;

/// Adaptery z brzydkiej do ladnej
/// TODO Dodać ToString()
public class GameHashAdapter : IGame
{
    private readonly IGameHash _g;

    public GameHashAdapter(IGameHash g)
    {
        this._g = g;
    }


    public string Name
    {
        get => _g.GetHashMap()[_g.GetName()];
        set
        {
            _g.SetName(value);
        }
    }
    public string Genre
    {
        get => _g.GetHashMap()[_g.GetGenre()];
        set
        {
            _g.SetGenre(value);
        }
    }
    public string Devices
    {
        get => _g.GetHashMap()[_g.GetDevices()];
        set
        {
            _g.SetDevices(value);
        }
    }
    
    public List<IUser> Authors
    {
        get
        {
            var outAuthors = new List<IUser>();
            foreach (IUserHash gAuthor in _g.Authors)
            {
                outAuthors.Add(new UserHashAdapter(gAuthor));
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

    public List<IReview> Reviews { get; set; }
    public List<IMod> Mods { get; set; }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Genre: {Genre}");
        sb.AppendLine($"Devices: {Devices}");
        sb.AppendLine("Authors:");
        foreach (var author in Authors)
        {
            sb.AppendLine($"- {author.Nickname}");
        }
        sb.AppendLine("Reviews:");
        foreach (var review in Reviews)
        {
            sb.AppendLine($"- {review.Author}: {review.Rating}");
        }
        sb.AppendLine("Mods:");
        foreach (var mod in Mods)
        {
            sb.AppendLine($"- {mod.Name}");
        }
    
        return sb.ToString();
    }
}

public class ReviewHashAdapter : IReview
{
    private readonly IReviewHash _r;

    public ReviewHashAdapter(IReviewHash r)
    {
        this._r = r;
    }
    
    public string Text
    {
        get => _r.GetHashMap()[_r.GetText()];
        set
        {
            _r.SetText(value);
        }
    }
    public int Rating
    {
        get => int.Parse(_r.GetHashMap()[_r.GetRating()]);
        set
        {
            _r.SetRating(value);
        }
    }
    
    public IUser Author
    {
        get
        {
            return new UserHashAdapter(_r.Author);
        }
        set
        {
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
        }
    }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Author: {Author.Nickname}");
        sb.AppendLine($"Rating: {Rating}");
        sb.AppendLine($"Text: {Text}");

        return sb.ToString();
    }
}

public class ModHashAdapter : IMod
{
    private readonly IModHash _m;

    public ModHashAdapter(IModHash m)
    {
        this._m = m;
    }
    
    public string Name
    {
        get => _m.GetHashMap()[_m.GetName()];
        set
        {
            _m.SetName(value);
        }
    }
    public string Description
    {
        get => _m.GetHashMap()[_m.GetDescription()];
        set
        {
            _m.SetDescription(value);
        }
    }
    
    public List<IUser> Authors
    {
        get
        {
            var outAuthors = new List<IUser>();
            foreach (IUserHash gAuthor in _m.Authors)
            {
                outAuthors.Add(new UserHashAdapter(gAuthor));
            }

            return outAuthors;
        }
        set
        {
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
        }
    }

    public List<IMod> Compatibility
    {
        get
        {
            var outMods = new List<IMod>();
            foreach (IModHash otherMod in _m.Compatibility)
            {
                outMods.Add(new ModHashAdapter(otherMod));
            }

            return outMods;
        }
        set
        {
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
        }
    }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"Description: {Description}");
        sb.AppendLine("Authors:");
        foreach (var author in Authors)
        {
            sb.AppendLine($"- {author.Nickname}");
        }
        sb.AppendLine("Compatibility:");
        foreach (var mod in Compatibility)
        {
            sb.AppendLine($"- {mod.Name}");
        }
        return sb.ToString();
    }
}

public class UserHashAdapter : IUser
{
    private readonly IUserHash _u;

    public UserHashAdapter(IUserHash u)
    {
        this._u = u;
    }
    
    public string Nickname
    {
        get => _u.GetHashMap()[_u.GetNickname()];
        set
        {
            _u.SetNickname(value);
        }
    }
    
    public List<IGame> OwnedGames
    {
        get
        {
            var outGames = new List<IGame>();
            foreach (IGameHash game in _u.OwnedGames)
            {
                outGames.Add(new GameHashAdapter(game));
            }

            return outGames;
        }
        set
        {
            throw new NotImplementedException(); // TODO(Czy ja potrzebuję adaptera w droga strone?)
        }
    }
    
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Nickname: {Nickname}");
        sb.AppendLine("Owned Games:");
        foreach (var game in OwnedGames)
        {
            sb.AppendLine($"- {game.Name}");
        }
        return sb.ToString();
    }
}


