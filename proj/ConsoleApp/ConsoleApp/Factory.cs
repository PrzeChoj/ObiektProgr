namespace ConsoleApp;

public interface ICommandFactory
{
    public AbstractCommand Create();
}

public class ListCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new ListCommand();
    }
}

public class ExitCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new ExitCommand();
    }
}

public class FindCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new FindCommand();
    }
}

public class AddCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new AddCommand();
    }
}

public class QueueCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new QueueCommand();
    }
}

public class EditCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new EditCommand();
    }
}

public class DeleteCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new DeleteCommand();
    }
}

public class HistoryCommandFactory : ICommandFactory
{
    public AbstractCommand Create()
    {
        return new HistoryCommand();
    }
}
