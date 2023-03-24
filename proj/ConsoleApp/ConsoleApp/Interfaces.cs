namespace ConsoleApp;

// Interfac'y dla basic
public interface IGame
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Devices { get; set; }
    public List<IUser> Authors { get; set; }
    public List<IReview> Reviews { get; set; }
    public List<IMod> Mods { get; set; }
}

public interface IReview
{
    public string Text { get; set; }
    public int Rating { get; set; }
    public IUser Author { get; set; }
}

public interface IMod
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IUser> Authors { get; set; }
    public List<IMod> Compatibility { get; set; }
}

public interface IUser
{
    public string Nickname { get; set; }
    public List<IGame> OwnedGames { get; set; }
}

// Interfac'y dla hash
public interface IGameHash
{
    public void SetName(string name);
    public int GetName();
    public void SetGenre(string genre);
    public int GetGenre();
    public void SetDevices(string devices);
    public int GetDevices();
    public List<IUserHash> Authors { get; set; }
    public List<IReviewHash> Reviews { get; set; }
    public List<IModHash> Mods { get; set; }
}

public interface IReviewHash
{
    public void SetText(string text);
    public int GetText();
    public void SetRating(int rating);
    public int GetRating();
    public IUserHash Author { get; set; }
}

public interface IModHash
{
    public void SetName(string name);
    public int GetName();
    public void SetDescription(string description);
    public int GetDescription();
    public List<IUserHash> Authors { get; set; }
    public List<IModHash> Compatibility { get; set; }
}

public interface IUserHash
{
    public void SetNickname(string nickname);
    public int GetNickname();
    public List<IGameHash> OwnedGames { get; set; }
}