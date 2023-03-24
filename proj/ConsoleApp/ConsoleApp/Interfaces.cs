namespace ConsoleApp;

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