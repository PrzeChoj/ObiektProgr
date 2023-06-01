using System.Collections;
using System.Reflection;

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
            { "QUEUE", new QueueCommandFactory() },
            { "EDIT", new EditCommandFactory()}
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Unknown command {commandName}");
                Console.ResetColor();
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
        {"COMMIT", new QueueCommitCommand()}
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

public class QueueCommitCommand : ICommand
{
    public string Name { get; } = "QueueCommit";
    public string Description { get; } = "Commits and executes all commands currently stored in the queue";
    public string[] args { get; set; }
    public void Execute()
    {
        foreach (object o in MyConsole.CommandsQueue)
        {
            ICommand command = (ICommand)o;
            
            command.Execute();
        }

        MyConsole.CommandsQueue.Clear();
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
    protected readonly Dictionary<string, Func<string[], Func<object, bool>>> _filter;
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
        _filter.Add("ADAPTERGAMEFROMTUPLE", args =>
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
        _filter.Add("ADAPTERUSERSFROMTUPLE", args =>
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
        _filter.Add("ADAPTERREVIEWSFROMTUPLE", args =>
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
        _filter.Add("ADAPTERMODSFROMTUPLE", args =>
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
            if (splittedArgs == null)
            {
                return false;
            }
                    
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

        if (!s.Contains("\""))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: Finding component has to possess a \" sign: {s}");
            Console.ResetColor();

            return null;
        }
        
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
        
        if (parts.Length != 2 || parts[1].Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: Wrong finding component: {s}");
            Console.ResetColor();

            return null;
        }
        
        string[] partsOut = {parts[0], whatContains, parts[1].Substring(1, parts[1].Length - 2)};
        
        return partsOut;
    }
    
    public virtual void Execute()
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
    
    public virtual bool ExecuteInstantly() { return false; }
}

public class EditCommand : FindCommand
{
    public override void Execute()
    {
        var fieldDic = new Dictionary<string, string[]>
        {
            { "GAME", new []{"NAME", "GENRE", "DEVICES"} },
            { "ADAPTERGAMEFROMTUPLE", new []{"NAME", "GENRE", "DEVICES"} },
            { "USERS", new []{"NICKNAME"} },
            { "ADAPTERUSERSFROMTUPLE", new []{"NICKNAME"} },
            { "REVIEWS", new []{"TEXT", "RATING"} },
            { "ADAPTERREVIEWSFROMTUPLE", new []{"TEXT", "RATING"} },
            { "MODS", new []{"NAME", "DESCRIPTION"} },
            { "ADAPTERMODSFROMTUPLE", new []{"NAME", "DESCRIPTION"} }
        };
        
        void MyEdit<T>(T t)
        {
            while (true)
            {
                Console.Write("Write <name_of_field>=<value> or DONE:\n> ");
                string input = Console.ReadLine();

                if (input.ToUpper() is "DONE" or "EXIT")
                {
                    break;
                }

                string[] newAttribute = input.Split('=');

                if (newAttribute.Length != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Wrong input. Try again");
                    Console.ResetColor();
                    Console.Write("> ");
                    continue;
                }
                string fieldName = newAttribute[0].ToUpper();
                if (!fieldDic[t.GetType().Name.ToUpper()].Contains(fieldName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Unknown attribute {fieldName}");
                    Console.ResetColor();
                    continue;
                }
                
                Type type = t.GetType();
                if (!SetAttributeValue(t, type, newAttribute[0], newAttribute[1]))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Provided attribute has wrong uppercases: {newAttribute[0]}");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Attribute successfully edited!");
                    Console.ResetColor();
                }
            }
        }
        
        string ListName = args[1].ToUpper();
        if (!MyConsole.Lists.ContainsKey(ListName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: UNKNOWN COLLECTION {ListName}");
            Console.ResetColor();
            return;
        }
        
        AlgorithmsOnCollections.DoIf(MyConsole.Lists[ListName].GetEnumerator(), _filter[ListName](args), MyEdit);
    }
    
    public override bool ExecuteInstantly() { return true; }
    
    public static bool SetAttributeValue(object obj, Type type, string attribute, string value)
    {
        PropertyInfo property = type.GetProperty(attribute);

        if (property == null)
        {
            return false;
        }
        
        if (property.PropertyType == typeof(int))
        {
            int intValue = Convert.ToInt32(value);
            property.SetValue(obj, intValue);
        }
        else
        {
            property.SetValue(obj, value);
        }

        return true;
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
