using ConsoleApp;

interface IGame
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Devices { get; set; }
    public List<User> Authors { get; set; }
    public List<Review> Reviews { get; set; }
    public List<Mod> Mods { get; set; }
}

interface IReview
{
    public string Text { get; set; }
    public int Rating { get; set; }
    public User Author { get; set; }
}

interface IMod
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Authors { get; set; }
    public List<Mod> Compatibility { get; set; }
}

interface IUser
{
    public string Nickname { get; set; }
    public List<Game> OwnedGames { get; set; }
}