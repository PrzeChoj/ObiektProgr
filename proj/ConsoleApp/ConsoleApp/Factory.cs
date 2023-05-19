namespace ConsoleApp;

public interface GameFactory
{
    public Game Create(string name, string genre, string devices, List<User>? authors = null, List<Review>? reviews = null, List<Mod>? mods = null);
}
public class GameBase : GameFactory
{
    public Game Create(string name, string genre, string devices, List<User>? authors = null, List<Review>? reviews = null, List<Mod>? mods = null)
    {
        return new Game(name, genre, devices, authors, reviews, mods);
    }
}
public class GameSecond : GameFactory
{
    public Game Create(string name, string genre, string devices, List<User>? authors = null, List<Review>? reviews = null, List<Mod>? mods = null)
    {
        return new AdapterGameFromHash(new GameHash(name, genre, devices, authors, reviews, mods));
    }
}