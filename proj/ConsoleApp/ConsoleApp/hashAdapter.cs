using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;

/// Adaptery z brzydkiej do ladnej
/// TODO Dodać ToString()
public class GameHashAdapter : IGame
{
    private readonly IGameHash g;

    public GameHashAdapter(IGameHash g)
    {
        this.g = g;
    }


    public string Name
    {
        get => g.GetHashMap()[g.GetName()];
        set
        {
            g.SetName(value);
        }
    }
    public string Genre
    {
        get => g.GetHashMap()[g.GetGenre()];
        set
        {
            g.SetGenre(value);
        }
    }
    public string Devices
    {
        get => g.GetHashMap()[g.GetDevices()];
        set
        {
            g.SetDevices(value);
        }
    }
    
    public List<IUser> Authors
    {
        get
        {
            var outAuthors = new List<IUser>();
            foreach (IUserHash gAuthor in g.Authors)
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
}

public class ReviewHashAdapter : IReview
{
    private readonly IReviewHash r;

    public ReviewHashAdapter(IReviewHash r)
    {
        this.r = r;
    }
    
    public string Text
    {
        get => r.GetHashMap()[r.GetText()];
        set
        {
            r.SetText(value);
        }
    }
    public int Rating
    {
        get => int.Parse(r.GetHashMap()[r.GetRating()]);
        set
        {
            r.SetRating(value);
        }
    }
    
    // TODO
    public IUser Author { get; set; }
}

public class ModHashAdapter : IMod
{
    private readonly IModHash m;

    public ModHashAdapter(IModHash m)
    {
        this.m = m;
    }
    
    public string Name
    {
        get => m.GetHashMap()[m.GetName()];
        set
        {
            m.SetName(value);
        }
    }
    public string Description
    {
        get => m.GetHashMap()[m.GetDescription()];
        set
        {
            m.SetDescription(value);
        }
    }
    
    // TODO()
    public List<IUser> Authors { get; set; }
    public List<IMod> Compatibility { get; set; }
}

public class UserHashAdapter : IUser
{
    private readonly IUserHash u;

    public UserHashAdapter(IUserHash u)
    {
        this.u = u;
    }
    
    public string Nickname
    {
        get => u.GetHashMap()[u.GetNickname()];
        set
        {
            u.SetNickname(value);
        }
    }
    // TODO()
    public List<IGame> OwnedGames { get; set; }
}


