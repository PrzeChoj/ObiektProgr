using System.Text;

namespace ConsoleApp;
using System.Collections.Generic;

/// Adaptery z brzydkiej do ladnej
/// TODO Dodać ToString()
public class GameHashAdapter : IGame
{
    private readonly GameHash g;

    public GameHashAdapter(GameHash g)
    {
        this.g = g;
    }


    public string Name
    {
        get => g._myHashMap[g.GetName()];
        set
        {
            g.SetName(value);
        }
    }
    public string Genre
    {
        get => g._myHashMap[g.GetGenre()];
        set
        {
            g.SetGenre(value);
        }
    }
    public string Devices
    {
        get => g._myHashMap[g.GetDevices()];
        set
        {
            g.SetDevices(value);
        }
    }
    
    //TODO(:<<<)
    public List<IUser> Authors
    {
        get
        {
            var outAuthors = new List<IUser>();
            foreach (UserHash gAuthor in g.Authors)
            {
                outAuthors.Add(new UserHashAdapter(gAuthor));
            }

            return outAuthors;
        }
        set
        {
            var newGAuthors = new List<IUserHash>();
            foreach (IUser user in value)
            {
                var ownedGames = new List<IGameHash>();
                foreach (IGame ownedGame in user.OwnedGames)
                {
                    ;//ownedGames.Add(); // TODO()
                }
                newGAuthors.Add(new UserHash(user.Nickname, ownedGames));
            }
            
            g.Authors = newGAuthors;
        }
    }

    public List<IReview> Reviews { get; set; }
    public List<IMod> Mods { get; set; }
}

public class ReviewHashAdapter : IReview
{
    private readonly ReviewHash r;

    public ReviewHashAdapter(ReviewHash r)
    {
        this.r = r;
    }
    
    public string Text
    {
        get => r._myHashMap[r.GetText()];
        set
        {
            r.SetText(value);
        }
    }
    public int Rating
    {
        get => int.Parse(r._myHashMap[r.GetRating()]);
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
    private readonly ModHash m;

    public ModHashAdapter(ModHash m)
    {
        this.m = m;
    }
    
    public string Name
    {
        get => m._myHashMap[m.GetName()];
        set
        {
            m.SetName(value);
        }
    }
    public string Description
    {
        get => m._myHashMap[m.GetDescription()];
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
    private readonly UserHash u;

    public UserHashAdapter(UserHash u)
    {
        this.u = u;
    }
    
    public string Nickname
    {
        get => u._myHashMap[u.GetNickname()];
        set
        {
            u.SetNickname(value);
        }
    }
    // TODO()
    public List<IGame> OwnedGames { get; set; }
}


