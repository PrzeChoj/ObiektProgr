namespace ConsoleApp;

public class MyConsole
{
    public readonly Dictionary<string, ICommand> CommandDic;
    public static Dictionary<string, ConsoleApp.IMyCollection<Object>> Lists;
    public static bool ContinueRunning = true;
    
    public MyConsole(Dictionary<string, ConsoleApp.IMyCollection<Object>> Lists)
    {
        CommandDic = new Dictionary<string, ICommand>();
        CommandDic.Add("LIST", new ListCommand());
        CommandDic.Add("EXIT", new ExitCommand());
        //CommandDic.Add("FIND", new FindCommand());
        //CommandDic.Add("ADD", new AddCommand());
        MyConsole.Lists = Lists;

        Run();
    }
    public void Run()
    {

        while (ContinueRunning)
        {
            Console.Write("============================================================\n> ");
            string input = Console.ReadLine();

            string[] args = input.Split(' ');

            if (args.Length == 0)
            {
                continue;
            }
            string commandName = args[0].ToUpper();
            if (!CommandDic.ContainsKey(commandName))
            {
                Console.WriteLine($"Error: Unknown command {commandName}");
                continue;
            }

            ICommand command = CommandDic[commandName];
            command.Execute(args);
        }

        Console.WriteLine("Goodbye!");
    }
}

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }
    public void Execute(string[] args);
}

public class ExitCommand : ICommand
{
    public string Name { get; } = "EXIT";
    public string Description { get; } = "EXIT app";
    public void Execute(string[] args)
    {
        MyConsole.ContinueRunning = false;
        Console.WriteLine("Exiting...");
    }
}

public class ListCommand : ICommand
{
    public string Name { get; } = "LIST";
    public string Description { get; } = "Print all objects in list";
    public ListCommand()
    {
        
    }
    public void Execute(string[] args)
    {
        void MyPrint<T>(T t)
        {
            Console.WriteLine(t);
        }
        
        string ListName = args[1].ToUpper();
        if (!MyConsole.Lists.ContainsKey(ListName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: UNKNOWN COLLECTION {ListName}");
            Console.ResetColor();
            return;
        }
        
        AlgorithmsOnCollections.ForEach<Object>((IEnumerator<object>)MyConsole.Lists[ListName].GetEnumerator(), MyPrint<Object>);;
    }
}
