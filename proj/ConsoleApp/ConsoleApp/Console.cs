using System.Reflection;
using System.Xml;

namespace ConsoleApp;

public class MyConsole
{
    public static readonly Dictionary<string, ICommandFactory> CommandDic = new Dictionary<string, ICommandFactory>
    {
        { "LIST", new ListCommandFactory() },
        { "EXIT", new ExitCommandFactory() },
        { "FIND", new FindCommandFactory() },
        { "ADD", new AddCommandFactory() },
        { "QUEUE", new QueueCommandFactory() },
        { "EDIT", new EditCommandFactory() },
        { "DELETE", new DeleteCommandFactory() },
        { "HISTORY", new HistoryCommandFactory() }
    };
    public static Dictionary<string, ConsoleApp.IMyCollection<Object>> Lists;
    public static bool ContinueRunning = true;
    public static readonly List<AbstractCommand> CommandsList = new ();
    public static List<AbstractCommand> History = new List<AbstractCommand>();

    public MyConsole(Dictionary<string, ConsoleApp.IMyCollection<Object>> Lists)
    {
        MyConsole.Lists = Lists;

        Run();
    }
    public void Run()
    {
        CommandsList.Clear();

        while (ContinueRunning)
        {
            Console.Write("============================================================\n> ");
            string input = Console.ReadLine();

            addInput(input);
        }

        Console.WriteLine("Goodbye!");
    }

    public static void addInput(string input)
    {
        string[] args = input.Split(' ');

        if (args.Length == 0)
        {
            return;
        }
        string commandName = args[0].ToUpper();
        if (!CommandDic.ContainsKey(commandName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: Unknown command {commandName}");
            Console.ResetColor();
            return;
        }

        AbstractCommand command = CommandDic[commandName].Create();
        command.Args = args;
        if (command.ExecuteInstantly())
        {
            command.Execute();
            History.Add(command);
        }
        else
            CommandsList.Add(command);
    }
}

public abstract class AbstractCommand
{
    public virtual string Name { get; }
    public string[] Args { get; set; }
    public abstract void Execute();
    public virtual bool ExecuteInstantly() { return false; }
    public string mySerialize(bool returnName = false)
    {
        string outStr = returnName ? Name + " " : "";
        
        foreach (string arg in Args)
        {
            outStr += arg + " ";
        }
        return outStr;
    }
}

public class QueueCommand : AbstractCommand
{
    public readonly Dictionary<string, AbstractCommand> CommandTypesDic = new Dictionary<string, AbstractCommand>()
    {
        {"PRINT", new QueuePrintCommand()},
        {"EXPORT", new QueueExportCommand()},
        {"COMMIT", new QueueCommitCommand()},
        {"DISMISS", new QueueDismissCommand()},
        {"LOAD", new QueueLoadCommand()}
    };

    public override string Name { get; } = "QueueCommand";
    public string Description { get; } = "Redirects to proper queue command";
    public override void Execute()
    {
        string commandType = Args[1].ToUpper();
        
        if (!CommandTypesDic.ContainsKey(commandType))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: UNKNOWN QUEUE COMMAND {commandType}");
            Console.ResetColor();
            return;
        }

        CommandTypesDic[commandType].Args = Args;
        CommandTypesDic[commandType].Execute();
        MyConsole.History.Add(CommandTypesDic[commandType]);
    }

    public override bool ExecuteInstantly() { return true; }
}

public class QueueExportCommand : AbstractCommand
{
    public override string Name { get; } = "QueueExportCommand";
    public string Description { get; } = "exports all commands currently stored in the queue into a file";
    public override void Execute()
    {
        string filename = Args[2];
        string format = "XML";
        if (Args.Length == 4)
        {
            format = Args[3];
        }

        if (format == "plaintext")
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                int cnt = 0;
                foreach (AbstractCommand command in MyConsole.CommandsList)
                {
                    string commandString = command.mySerialize(false);
                    writer.Write(commandString);
                    if (cnt++ != MyConsole.CommandsList.Count - 1)
                        writer.Write("\n");
                }
            }
        }
        else if (format == "XML")
        {
            using (XmlWriter writer = XmlWriter.Create(filename))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Commands");

                foreach (AbstractCommand command in MyConsole.CommandsList)
                {
                    string commandString = command.mySerialize(true);
                    writer.WriteStartElement("Command");
                    writer.WriteString(commandString);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
    
    public override bool ExecuteInstantly() { return true; }
}

public class QueueLoadCommand : AbstractCommand
{
    public override string Name { get; } = "QueueLoadCommand";
    public string Description { get; } = "load commands from file into the queue";
    public override void Execute()
    {
        if (Args.Length != 3)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: Wrong number of arguments: {Args.Length} != 3");
            Console.ResetColor();
            return;
        }
        
        string filename = Args[2];
        string format = filename.Substring(filename.Length-3, 3).ToUpper();

        if (format == "TXT")
        {

            MyConsole.addInput("dupa"); // TODO()
        }
        else if (format == "XML")
        {
            
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: Wrong format: {format}");
            Console.ResetColor();
        }
    }
    
    public override bool ExecuteInstantly() { return true; }
}

public class QueuePrintCommand : AbstractCommand
{
    public override string Name { get; } = "QueuePrintCommand";
    public string Description { get; } = "prints all commands currently stored in the queue";
    public override void Execute()
    {
        if (MyConsole.CommandsList.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Queue is empty :>");
            Console.ResetColor();
        }
        
        foreach (AbstractCommand command in MyConsole.CommandsList)
        {
            Console.WriteLine(command.ToString());
        }
    }
    
    public override bool ExecuteInstantly() { return true; }
}

public class QueueCommitCommand : AbstractCommand
{
    public override string Name { get; } = "QueueCommitCommand";
    public string Description { get; } = "Commits and executes all commands currently stored in the queue";
    public override void Execute()
    {
        foreach (object o in MyConsole.CommandsList)
        {
            AbstractCommand command = (AbstractCommand)o;
            
            command.Execute();
            MyConsole.History.Add(command);
        }

        MyConsole.CommandsList.Clear();
    }
}

public class QueueDismissCommand : AbstractCommand
{
    public override string Name { get; } = "QueueDismissCommand";
    public string Description { get; } = "Deletes all commands currently stored in the queue";
    public override void Execute()
    {
        MyConsole.CommandsList.Clear();
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Queue will now be empty :>");
        Console.ResetColor();
    }
}

public class ExitCommand : AbstractCommand
{
    public override string Name { get; } = "ExitCommand";
    public string Description { get; } = "EXIT app";
    public override void Execute()
    {
        MyConsole.ContinueRunning = false;
        Console.WriteLine("Exiting...");
    }

    public override bool ExecuteInstantly()
    {
        return true;
    }
}

public class ListCommand : AbstractCommand
{
    public override string Name { get; } = "ListCommand";
    public string Description { get; } = "Print all objects in list";
    public override void Execute() // Taka sama logika dla kadego rodzaju
    {
        void MyPrint<T>(T t)
        {
            Console.WriteLine(t);
        }
        
        string ListName = Args[1].ToUpper();
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
        for (int i = 0; i < Args.Length; i++)
        {
            outString += Args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
}

public abstract class AbstractFilteringCommand : AbstractCommand
{
    protected readonly Dictionary<string, Func<string[], Func<object, bool>>> _filter;
    public override string Name { get; } = "FilteringCommand";
    public string Description { get; } = "Prints objects matching certain conditions";
    public AbstractFilteringCommand()
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
}

public class FindCommand : AbstractFilteringCommand
{
    public override string Name { get; } = "FindCommand";
    public string Description { get; } = "Prints objects matching certain conditions";
    
    public override void Execute()
    {
        void MyPrint<T>(T t)
        {
            Console.WriteLine(t);
        }
        
        string ListName = Args[1].ToUpper();
        if (!MyConsole.Lists.ContainsKey(ListName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: UNKNOWN COLLECTION {ListName}");
            Console.ResetColor();
            return;
        }
        
        int howManyRecordsSattisfy =
            AlgorithmsOnCollections.CountIf(MyConsole.Lists[ListName].GetEnumerator(), _filter[ListName](Args));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Found {howManyRecordsSattisfy} object sattisfying conditions:");
        Console.ResetColor();
        AlgorithmsOnCollections.DoIf(MyConsole.Lists[ListName].GetEnumerator(), _filter[ListName](Args), MyPrint);
    }
    
    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < Args.Length; i++)
        {
            outString += Args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
    
    public override bool ExecuteInstantly() { return false; }
}

public class DeleteCommand : AbstractFilteringCommand
{
    public override string Name { get; } = "DeleteCommand";
    public string Description { get; } = "Deletes objects matching certain conditions, but only when the single object can be identified";
    
    public override void Execute()
    {
        string ListName = Args[1].ToUpper();
        if (!MyConsole.Lists.ContainsKey(ListName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: UNKNOWN COLLECTION {ListName}");
            Console.ResetColor();
            return;
        }

        int howManyRecordsSattisfy =
            AlgorithmsOnCollections.CountIf(MyConsole.Lists[ListName].GetEnumerator(), _filter[ListName](Args));
        if (howManyRecordsSattisfy == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: Conditions are not Sattisfied by any record");
            Console.ResetColor();
            return;
        }

        if (howManyRecordsSattisfy != 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: Conditions are Sattisfied by {howManyRecordsSattisfy} records");
            Console.WriteLine("Deletion is only possible when a single record can to identified!");
            Console.ResetColor();
            return;
        }

        var x = AlgorithmsOnCollections.FindObject(MyConsole.Lists[ListName].GetEnumerator(), _filter[ListName](Args));
        var wasDeleted = MyConsole.Lists[ListName].Remove(x);

        if (wasDeleted)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully deleted an object!");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("An object was not deleted!");
            Console.ResetColor();
        }
        
    }
    
    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < Args.Length; i++)
        {
            outString += Args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
    
    public override bool ExecuteInstantly() { return false; }
}

public class EditCommand : AbstractFilteringCommand
{
    public override string Name { get; } = "EditCommand";
    public override void Execute()
    {
        var fieldDic = new Dictionary<string, string[]>
        {
            { "GAME", new []{"NAME", "GENRE", "DEVICES"} },
            { "ADAPTERGAMEFROMTUPLE", new []{"NAME", "GENRE", "DEVICES"} },
            { "USER", new []{"NICKNAME"} },
            { "ADAPTERUSERFROMTUPLE", new []{"NICKNAME"} },
            { "REVIEW", new []{"TEXT", "RATING"} },
            { "ADAPTERREVIEWFROMTUPLE", new []{"TEXT", "RATING"} },
            { "MOD", new []{"NAME", "DESCRIPTION"} },
            { "ADAPTERMODFROMTUPLE", new []{"NAME", "DESCRIPTION"} }
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
        
        string ListName = Args[1].ToUpper();
        if (!MyConsole.Lists.ContainsKey(ListName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERROR: UNKNOWN COLLECTION {ListName}");
            Console.ResetColor();
            return;
        }
        
        AlgorithmsOnCollections.DoIf(MyConsole.Lists[ListName].GetEnumerator(), _filter[ListName](Args), MyEdit);
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

public class AddCommand : AbstractCommand
{
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
    
    Dictionary<string, AbstractCommand> AddDict;
    public override string Name { get; } = "AddCommand";
    public string Description { get; } = "Adds object";
    public AddCommand()
    {
        AddDict = new Dictionary<string, AbstractCommand>();
        AddDict.Add("GAMES", new AddGame());
        //AddDict.Add("NEXT", new AddNext());
    }
    
    public override void Execute()
    {

        try
        {
            AddDict[Args[1].ToUpper()].Execute();
            MyConsole.History.Add(AddDict[Args[1].ToUpper()]);
        }
        catch {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("INVALID ADD ARGUMENT");
            Console.ResetColor();
        };
    }
    
    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < Args.Length; i++)
        {
            outString += Args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
}

public class AddGame : AbstractCommand
{
    public override string Name { get; } = "AddGame";
    public string Description { get; } = "Adds Game";
    private Dictionary<string, IMyCollection<object>>? _lists;
    public override void Execute()
    {
        throw new NotImplementedException();
    }
    
    public override string ToString()
    {
        String outString = "";
        for (int i = 0; i < Args.Length; i++)
        {
            outString += Args[i];
            outString += " ";
        }
        return String.Format(outString);
    }
}

public class HistoryCommand : AbstractCommand
{
    public override string Name { get; } = "HistoryCommand";
    public string Description { get; } = "prints all executed commands";
    public override void Execute()
    {
        if (MyConsole.History.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("No commands were executed :<");
            Console.ResetColor();
        }
        
        foreach (AbstractCommand command in MyConsole.History)
        {
            Console.WriteLine(command.ToString());
        }
    }
    
    public override bool ExecuteInstantly() { return true; }
}
