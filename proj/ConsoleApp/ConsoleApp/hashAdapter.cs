using System.Collections.ObjectModel;

namespace ConsoleApp;
using System.Collections.Generic;

// Adaptery z brzydkiej do ladnej
public class AdapterGameFromHash : Game
{
    private readonly GameHash _g;

    public AdapterGameFromHash(GameHash g)
    {
        _g = g ?? throw new Exception("g is null");
    }

    public override string Name
    {
        get => _g.GetHashMap()[_g.GetName()];
        set => _g.SetName(value);
    }
    public override string Genre
    {
        get => _g.GetHashMap()[_g.GetGenre()];
        set => _g.SetGenre(value);
    }
    public override string Devices
    {
        get => _g.GetHashMap()[_g.GetDevices()];
        set => _g.SetDevices(value);
    }
    
    public override List<User> Authors
    {
        get => _g.Authors.Select(gAuthor => new AdapterUserFromHash(gAuthor)).Cast<User>().ToList();
        set => _g.Authors = value.Select(user => new AdapterUserToHash(user)).Cast<UserHash>().ToList();
    }

    public override List<Review> Reviews
    {
        get => _g.Reviews.Select(gRev => new AdapterReviewFromHash(gRev)).Cast<Review>().ToList();
        set => _g.Reviews = value.Select(review => new AdapterReviewToHash(review)).Cast<ReviewHash>().ToList();
    }

    public override List<Mod> Mods
    {
        get => _g.Mods.Select(gMod => new AdapterModFromHash(gMod)).Cast<Mod>().ToList();
        set => _g.Mods = value.Select(mod => new AdapterModToHash(mod)).Cast<ModHash>().ToList();
    }
}

public class AdapterReviewFromHash : Review
{
    private readonly ReviewHash _r;

    public AdapterReviewFromHash(ReviewHash r)
    {
        _r = r ?? throw new Exception("r is null");
    }
    
    public override string Text
    {
        get => _r.GetHashMap()[_r.GetText()];
        set => _r.SetText(value);
    }
    public override int Rating
    {
        get => int.Parse(_r.GetHashMap()[_r.GetRating()]);
        set => _r.SetRating(value);
    }
    
    public override User Author
    {
        get => new AdapterUserFromHash(_r.Author);
        set => _r.Author = new AdapterUserToHash(value);
    }
}

public class AdapterModFromHash : Mod
{
    private readonly ModHash _m;

    public AdapterModFromHash(ModHash m)
    {
        _m = m ?? throw new Exception("m is null");
    }
    
    public override string Name
    {
        get => _m.GetHashMap()[_m.GetName()];
        set => _m.SetName(value);
    }
    public override string Description
    {
        get => _m.GetHashMap()[_m.GetDescription()];
        set => _m.SetDescription(value);
    }
    
    public override List<User> Authors
    {
        get => _m.Authors.Select(gAuthor => new AdapterUserFromHash(gAuthor)).Cast<User>().ToList();
        set => _m.Authors = value.Select(gAuthor => new AdapterUserToHash(gAuthor)).Cast<UserHash>().ToList();
    }

    public override List<Mod> Compatibility
    {
        get => _m.Compatibility.Select(otherMod => new AdapterModFromHash(otherMod)).Cast<Mod>().ToList();
        set => _m.Compatibility = value.Select(mod => new AdapterModToHash(mod)).Cast<ModHash>().ToList();
    }
}

public class AdapterUserFromHash : User
{
    private readonly UserHash _u;

    public AdapterUserFromHash(UserHash u)
    {
        _u = u ?? throw new Exception("u is null");
    }
    
    public override string Nickname
    {
        get => _u.GetHashMap()[_u.GetNickname()];
        set => _u.SetNickname(value);
    }
    
    public override List<Game> OwnedGames
    {
        get => _u.OwnedGames.Select(game => new AdapterGameFromHash(game)).Cast<Game>().ToList();
        set => _u.OwnedGames = value.Select(game => new AdapterGameToHash(game)).Cast<GameHash>().ToList();
    }
}


// Adaptery z ladnej do brzydkiej
public class AdapterGameToHash : GameHash
{
    private readonly Game _g;
    
    public AdapterGameToHash(Game g)
    {
        _g = g ?? throw new Exception("g is null");
    }
    
    internal override ReadOnlyDictionary<int, string> GetHashMap()
    {
        return new Dictionary<int, string>
            {
                [_g.Name.GetHashCode()] = _g.Name,
                [_g.Genre.GetHashCode()] = _g.Genre,
                [_g.Devices.GetHashCode()] = _g.Devices
            }.AsReadOnly();
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
        get => _g.Authors.Select(author => new AdapterUserToHash(author)).Cast<UserHash>().ToList();
        set => _g.Authors = value.Select(author => new AdapterUserFromHash(author)).Cast<User>().ToList();
    }

    public override List<ReviewHash> Reviews
    {
        get => _g.Reviews.Select(review => new AdapterReviewToHash(review)).Cast<ReviewHash>().ToList();
        set => _g.Reviews = value.Select(review => new AdapterReviewFromHash(review)).Cast<Review>().ToList();
    }

    public override List<ModHash> Mods
    {
        get => _g.Mods.Select(mod => new AdapterModToHash(mod)).Cast<ModHash>().ToList();
        set => _g.Mods = value.Select(mod => new AdapterModFromHash(mod)).Cast<Mod>().ToList();
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
        var stringRating = _r.Rating.ToString();
        
        return new Dictionary<int, string>
            {
                [_r.Text.GetHashCode()] = _r.Text,
                [stringRating.GetHashCode()] = stringRating
            }.AsReadOnly();
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
        return _r.Rating.ToString().GetHashCode(); // Przerobic rating na hasha ze stringa
    }

    public override UserHash Author
    {
        get => new AdapterUserToHash(_r.Author);
        set => _r.Author = new AdapterUserFromHash(value);
    }
}

public class AdapterModToHash : ModHash
{
    private readonly Mod _m;
    
    public AdapterModToHash(Mod m)
    {
        _m = m ?? throw new Exception("m is null");
    }
    
    internal override ReadOnlyDictionary<int, string> GetHashMap()
    {
        return new Dictionary<int, string>
            {
                [_m.Name.GetHashCode()] = _m.Name,
                [_m.Description.GetHashCode()] = _m.Description
            }.AsReadOnly();
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
        get => _m.Authors.Select(author => new AdapterUserToHash(author)).Cast<UserHash>().ToList();
        set => _m.Authors = value.Select(author => new AdapterUserFromHash(author)).Cast<User>().ToList();
    }

    public override List<ModHash> Compatibility
    {
        get => _m.Compatibility.Select(mod => new AdapterModToHash(mod)).Cast<ModHash>().ToList();
        set => _m.Compatibility = value.Select(mod => new AdapterModFromHash(mod)).Cast<Mod>().ToList();
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
        return new Dictionary<int, string>
            {
                [_u.Nickname.GetHashCode()] = _u.Nickname
            }.AsReadOnly();
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
        get => _u.OwnedGames.Select(game => new AdapterGameToHash(game)).Cast<GameHash>().ToList();
        set => _u.OwnedGames = value.Select(game => new AdapterGameFromHash(game)).Cast<Game>().ToList();
    }
}