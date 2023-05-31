namespace ConsoleApp;

public interface ICommandFactory
{
    public ICommand Create();
}

public class ListCommandFactory : ICommandFactory
{
    public ICommand Create()
    {
        return new ListCommand();
    }
}

public class ExitCommandFactory : ICommandFactory
{
    public ICommand Create()
    {
        return new ExitCommand();
    }
}

public class FindCommandFactory : ICommandFactory
{
    public ICommand Create()
    {
        return new FindCommand();
    }
}

public class AddCommandFactory : ICommandFactory
{
    public ICommand Create()
    {
        return new AddCommand();
    }
}

public class QueueCommandFactory : ICommandFactory
{
    public ICommand Create()
    {
        return new QueueCommand();
    }
}

