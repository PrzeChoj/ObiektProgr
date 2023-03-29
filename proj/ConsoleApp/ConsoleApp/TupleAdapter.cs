namespace ConsoleApp;
using System.Collections.Generic;

// Adaptery z brzydkiej do ladnej
public class AdapterGameFromTuple : Game
{
    private readonly GameTuple _g;

    public AdapterGameFromTuple(GameTuple g)
    {
        _g = g ?? throw new Exception("g is null");
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
        get => _g.GetAuthors().Select(gAuthor => new AdapterUserFromTuple(gAuthor)).Cast<User>().ToList();
        set => _g.SetAuthors(value.Select(user => new AdapterUserToTuple(user)).Cast<UserTuple>().ToList());
    }

    public override List<Review> Reviews
    {
        get => _g.GetReviews().Select(gRev => new AdapterReviewFromTuple(gRev)).Cast<Review>().ToList();
        set => _g.SetReviews(value.Select(review => new AdapterReviewToTuple(review)).Cast<ReviewTuple>().ToList());
    }

    public override List<Mod> Mods
    {
        get => _g.GetMods().Select(gMod => new AdapterModFromTuple(gMod)).Cast<Mod>().ToList();
        set => _g.SetMods(value.Select(mod => new AdapterModToTuple(mod)).Cast<ModTuple>().ToList());
    }
}

public class AdapterReviewFromTuple : Review
{
    private readonly ReviewTuple _r;

    public AdapterReviewFromTuple(ReviewTuple r)
    {
        _r = r ?? throw new Exception("r is null");
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
        get => new AdapterUserFromTuple(_r.GetAuthor());
        set => _r.SetAuthor(new AdapterUserToTuple(value));
    }
}

public class AdapterModFromTuple : Mod
{
    private readonly ModTuple _m;

    public AdapterModFromTuple(ModTuple m)
    {
        _m = m ?? throw new Exception("m is null");
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
        get => _m.GetAuthors().Select(gAuthor => new AdapterUserFromTuple(gAuthor)).Cast<User>().ToList();
        set => _m.SetAuthors(value.Select(gAuthor => new AdapterUserToTuple(gAuthor)).Cast<UserTuple>().ToList());
    }

    public override List<Mod> Compatibility
    {
        get => _m.GetCompatibility().Select(otherMod => new AdapterModFromTuple(otherMod)).Cast<Mod>().ToList();
        set => _m.SetCompatibility(value.Select(mod => new AdapterModToTuple(mod)).Cast<ModTuple>().ToList());
    }
}

public class AdapterUserFromTuple : User
{
    private readonly UserTuple _u;

    public AdapterUserFromTuple(UserTuple u)
    {
        _u = u ?? throw new Exception("u is null");
    }
    
    public override string Nickname
    {
        get => _u.GetNickname();
        set => _u.SetNickname(value);
    }
    
    public override List<Game> OwnedGames
    {
        get => _u.GetOwnedGames().Select(game => new AdapterGameFromTuple(game)).Cast<Game>().ToList();
        set => _u.SetOwnedGames(value.Select(game => new AdapterGameToTuple(game)).Cast<GameTuple>().ToList());
    }
}


// Adaptery z ladnej do brzydkiej
public class AdapterGameToTuple : GameTuple
{
    private readonly Game _g;
    
    public AdapterGameToTuple(Game g)
    {
        _g = g ?? throw new Exception("g is null");
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

        newAuthors.AddRange(authors.Select(author => new AdapterUserFromTuple(author)));
        _g.Authors = newAuthors;
    }

    public override List<UserTuple> GetAuthors()
    {
        return _g.Authors.Select(author => new AdapterUserToTuple(author)).Cast<UserTuple>().ToList();
    }

    public override void SetReviews(List<ReviewTuple>? reviews)
    {
        var newReviews = new List<Review>();
        
        if (reviews == null)
        {
            _g.Reviews = newReviews;
            return;
        }

        newReviews.AddRange(reviews.Select(review => new AdapterReviewFromTuple(review)).Cast<Review>());
        _g.Reviews = newReviews;
    }

    public override List<ReviewTuple> GetReviews()
    {
        return _g.Reviews.Select(review => new AdapterReviewToTuple(review)).Cast<ReviewTuple>().ToList();
    }

    public override void SetMods(List<ModTuple>? mods)
    {
        var newMods = new List<Mod>();
        
        if (mods == null)
        {
            _g.Mods = newMods;
            return;
        }

        newMods.AddRange(mods.Select(mod => new AdapterModFromTuple(mod)));
        _g.Mods = newMods;
    }

    public override List<ModTuple> GetMods()
    {
        return _g.Mods.Select(mod => new AdapterModToTuple(mod)).Cast<ModTuple>().ToList();
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
        _m = m ?? throw new Exception("m is null");
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

        newAuthors.AddRange(authors.Select(author => new AdapterUserFromTuple(author)).Cast<User>());
        _m.Authors = newAuthors;
    }

    public override List<UserTuple> GetAuthors()
    {
        return _m.Authors.Select(author => new AdapterUserToTuple(author)).Cast<UserTuple>().ToList();
    }

    public override void SetCompatibility(List<ModTuple>? mods)
    {
        var newMods = new List<Mod>();
        
        if (mods == null)
        {
            _m.Compatibility = newMods;
            return;
        }

        newMods.AddRange(mods.Select(mod => new AdapterModFromTuple(mod)));
        _m.Compatibility = newMods;
    }

    public override List<ModTuple> GetCompatibility()
    {
        return _m.Compatibility.Select(mod => new AdapterModToTuple(mod)).Cast<ModTuple>().ToList();
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

        newGames.AddRange(ownedGames.Select(game => new AdapterGameFromTuple(game)));
        _u.OwnedGames = newGames;
    }

    public override List<GameTuple> GetOwnedGames()
    {
        return _u.OwnedGames.Select(game => new AdapterGameToTuple(game)).Cast<GameTuple>().ToList();
    }
}