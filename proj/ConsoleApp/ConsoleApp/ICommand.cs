using System.Collections;

namespace ConsoleApp;

public class MyConsole
{
    public readonly Dictionary<string, ICommandFactory> CommandDic;
    public static Dictionary<string, ConsoleApp.IMyCollection<Object>> Lists;
    public static bool ContinueRunning = true;
    public static readonly Queue CommandsQueue = new Queue();
    
    public MyConsole(Dictionary<string, ConsoleApp.IMyCollection<Object>> Lists)
    {
        CommandDic = new Dictionary<string, ICommandFactory>
        {
            { "LIST", new ListCommandFactory() },
            { "EXIT", new ExitCommandFactory() },
            { "FIND", new FindCommandFactory() },
            { "ADD", new AddCommandFactory() },
            { "QUEUE", new QueueCommandFactory() }
        };
        MyConsole.Lists = Lists;

        Run();
    }
    public void Run()
    {
        CommandsQueue.Clear();

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

            ICommand command = CommandDic[commandName].Create();
            command.args = args;
            if (command.ExecuteInstantly())
                command.Execute();
            else
                CommandsQueue.Enqueue(command);
        }

        Console.WriteLine("Goodbye!");
    }
}

public interface ICommand
{
    public string Name { get; }
    public string Description { get; }
    public string[] args { get; set; }
    public void Execute();
    public bool ExecuteInstantly() { return false; }
}

public class QueueCommand : ICommand
{
    public readonly Dictionary<string, ICommand> CommandTypesDic = new Dictionary<string, ICommand>()
    {
        {"PRINT", new QueuePrintCommand()},
        //{"EXPORT", new QueueExportCommand()}, // TODO()
        //{"COMMIT", new QueueCommitCommand()} // TODO()
    };

    public string Name { get; } = "QueueCommand";
    public string Description { get; } = "Redirects to proper queue command";
    public string[] args { get; set; }

    public void Execute()
    {
        string commandType = args[1].ToUpper();
        
        if (!CommandTypesDic.ContainsKey(commandType))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: UNKNOWN QUEUE COMMAND {commandType}");
            Console.ResetColor();
            return;
        }
        
        CommandTypesDic[commandType].Execute();
    }

    public void Execute(string[] args)
    {
        throw new NotImplementedException();
    }

    public bool ExecuteInstantly() { return true; }
}

public class QueuePrintCommand : ICommand
{
    public string Name { get; } = "QueuePrint";
    public string Description { get; } = "prints all commands currently stored in the queue";
    public string[] args { get; set; }
    public void Execute()
    {
        foreach (object o in MyConsole.CommandsQueue)
        {
            ICommand command = (ICommand)o;
            
            Console.WriteLine(command.ToString());
        }
    }
}

public class ExitCommand : ICommand
{
    public string Name { get; } = "EXIT";
    public string Description { get; } = "EXIT app";
    public string[] args { get; set; }
    public void Execute()
    {
        MyConsole.ContinueRunning = false;
        Console.WriteLine("Exiting...");
    }

    public bool ExecuteInstantly()
    {
        return true;
    }
}

public class ListCommand : ICommand
{
    public string Name { get; } = "LIST";
    public string Description { get; } = "Print all objects in list";
    public string[] args { get; set; }
    public void Execute() // Taka sama logika dla kadego rodzaju
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
        
        AlgorithmsOnCollections.ForEach(MyConsole.Lists[ListName].GetEnumerator(), MyPrint);
    }

    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < args.Length; i++)
        {
            outString += args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
}

public class FindCommand : ICommand
{
    private readonly Dictionary<string, Func<string[], Func<object, bool>>> _filter;
    public string Name { get; } = "FIND";
    public string Description { get; } = "Prints objects matching certain conditions";
    public string[] args { get; set; }
    public FindCommand()
    {
        var preds = new Dictionary<string, Func<IComparable, IComparable, bool>>();
        preds.Add("=", (a, b) => a.Equals(b));
        preds.Add(">", (a, b) => a.CompareTo(b)>0);
        preds.Add("<", (a, b) => a.CompareTo(b)<0);
        
        _filter = new Dictionary<string, Func<string[], Func<object, bool>>>();
        _filter.Add("GAMES", args =>
        {
            return o =>
            {
                Game myGame = (Game)o;
                var fields = new Dictionary<string, string>();
                fields.Add("NAME", myGame.Name);
                fields.Add("GENRE", myGame.Genre);
                fields.Add("DEVICES", myGame.Devices);

                return findIfAllOfRequremandsAreSatisfied(args, preds, fields);
            };
        });
        _filter.Add("USERS", args =>
        {
            return o =>
            {
                User myUser = (User)o;
                var fields = new Dictionary<string, string>();
                fields.Add("NICKNAME", myUser.Nickname);
                fields.Add("NAME", myUser.Nickname); // Yes, this is copy

                return findIfAllOfRequremandsAreSatisfied(args, preds, fields);
            };
        });
        _filter.Add("REVIEWS", args =>
        {
            return o =>
            {
                Review myReview = (Review)o;
                var fields = new Dictionary<string, string>();
                fields.Add("TEXT", myReview.Text);
                fields.Add("RATING", myReview.Rating.ToString());

                return findIfAllOfRequremandsAreSatisfied(args, preds, fields);
            };
        });
        _filter.Add("MODS", args =>
        {
            return o =>
            {
                Mod myReview = (Mod)o;
                var fields = new Dictionary<string, string>();
                fields.Add("NAME", myReview.Name);
                fields.Add("DESCRIPTION", myReview.Description);

                return findIfAllOfRequremandsAreSatisfied(args, preds, fields);
            };
        });
    }

    private bool findIfAllOfRequremandsAreSatisfied(string[] args, Dictionary<string,Func<IComparable,IComparable,bool>> preds, Dictionary<string,string> fields)
    {
        for (int i = 2; i < args.Length; i++)
        {
            string[] splittedArgs = splitStringForFind(args[i]);
                    
            string nameReal = splittedArgs[0].ToUpper();
            string equalSmallerOrBigger = splittedArgs[1];
            string nameReference = splittedArgs[2];

            try
            {
                if (!preds[equalSmallerOrBigger](fields[nameReal], nameReference))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ERROR: UNKNOWN FIELD NAME {nameReal}");
                Console.ResetColor();
                return false;
            }
        }

        return true;
    }

    public string[] splitStringForFind(string s)
    {
        string[] parts;
        string whatContains;
        if (s.Contains("="))
        {
            parts = s.Split('=');
            whatContains = "=";
        }
        else if (s.Contains("<"))
        {
            parts = s.Split('<');
            whatContains = "<";
        }
        else
        {
            parts = s.Split('>');
            whatContains = ">";
        }
        
        string[] partsOut = {parts[0], whatContains, parts[1].Substring(1, parts[1].Length - 2)};
        
        return partsOut;
    }
    
    public void Execute()
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
        
        AlgorithmsOnCollections.DoIf(MyConsole.Lists[ListName].GetEnumerator(), _filter[ListName](args), MyPrint);
    }
    
    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < args.Length; i++)
        {
            outString += args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
}

public class AddCommand : ICommand
{
    public string[] args { get; set; }
    private static Dictionary<string, string[]> legalFields = new Dictionary<string, string[]>()
    {
        { "GAMES", new[] { "NAME", "GENRE", "DEVICES" } },
        { "USERS", new[] { "NICKNAME", "NAME" } },
        { "REVIEWS", new[] { "TEXT", "RATING" } },
        { "MODS", new[] { "NAME", "DESCRIPTION" } }
    };
    private static Dictionary<string, string[]> defaultFieldsValues = new Dictionary<string, string[]>()
    {
        { "GAMES", new[] { "defaultName", "defaultGenre", "defaultDevices" } },
        { "USERS", new[] { "defaultName", "defaultName" } },
        { "REVIEWS", new[] { "defaultText", "0" } },
        { "MODS", new[] { "defaultName", "defaultDescription" } }
    };
    
    Dictionary<string, ICommand> AddDict;
    public string Name { get; } = "ADD";
    public string Description { get; } = "Adds object";
    public AddCommand()
    {
        AddDict = new Dictionary<string, ICommand>();
        AddDict.Add("GAMES", new AddGame());
        //AddDict.Add("NEXT", new AddNext());
    }
    
    public void Execute()
    {

        try { AddDict[args[1].ToUpper()].Execute(); }
        catch {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("INVALID ADD ARGUMENT");
            Console.ResetColor();
        };
    }
    
    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < args.Length; i++)
        {
            outString += args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
}

public class AddGame : ICommand
{
    public string Name { get; } = "ADD Game";
    public string Description { get; } = "Adds Game";
    public string[] args { get; set; }
    private Dictionary<string, IMyCollection<object>>? _lists;
    public void Execute()
    {
        throw new NotImplementedException();
    }
    
    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < args.Length; i++)
        {
            outString += args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
}
