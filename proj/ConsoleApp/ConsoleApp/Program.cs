using ConsoleApp;

bool printOld = false;

// Users
User u1 = new User("Szredor");
User u2 = new User("Driver");
User u3 = new User("Pek");
User u4 = new User("Commander Shepard");
User u5 = new User("MLG");
User u6 = new User("Rondo");
User u7 = new User("lemon");
User u8 = new User("Bonet");

// Mods
Mod m1 = new Mod("Clouds", "Super clouds", new List<User>{u3});
Mod m2 = new Mod("T-pose", "Cow are now T-posing", new List<User>());
Mod m3 = new Mod("Commander Shepard", "I’m Commander Shepard and this is my favorite mod on Smoke",
    new List<User>{u4});
Mod m4 = new Mod("BTM", "You can now play in BTM’s trains and bytebuses", new List<User>{u7, u8});
Mod m5 = new Mod("Cosmic - black hole edition", "Adds REALISTIC black holes", new List<User>{u2});
m1.Compatibility = new List<Mod> { m2, m3, m4, m5 };
m2.Compatibility = new List<Mod> { m1, m3 };
m3.Compatibility = new List<Mod> { m1, m2, m4 };
m4.Compatibility = new List<Mod> { m1, m3 };
m5.Compatibility = new List<Mod> { m1 };

// Reviews
Review r1 = new Review("I’m Commander Shepard and this is my favorite game on Smoke", 10, u4);
Review r2 = new Review("The Moo remake sets a new standard for the future of the survival horror series⁠, even if it isn't the sequel I've been pining for.",
    12, u2);
Review r3 = new Review("Universe of Technology is a spectacular 4X game, that manages to shine even when the main campaign doesn't.",
    15, u7);
Review r4 = new Review("Moo’s interesting art design can't save it from its glitches, bugs, and myriad terrible game design decisions, but I love its sound design",
    2, u8);
Review r5 = new Review("I've played this game for years nonstop. Over 8k hours logged (not even joking). And I'm gonna tell you: at this point, the game's just not worth playing anymore. I think it hasn't been worth playing for a year or two now, but I'm only just starting to realize it. It breaks my heart to say that, but that's just the truth of the matter.",
    5, u1);

// Games
Game g1 = new Game("Garbage Collector", "simulation", "PW", null, null, new List<Mod>{m1});
Game g2 = new Game("Universe of Technology", "4X", "bitnix", null, new List<Review>{r3}, new List<Mod>{m1, m3});
Game g3 = new Game("Moo", "rogue-like", "bitstation", new List<User>{u2}, new List<Review>{r2, r4}, new List<Mod>{m1, m2, m3});
Game g4 = new Game("Tickets Please", "platformer", "bitbox", new List<User>{u1}, new List<Review>{r1}, new List<Mod>{m1, m3, m4});
Game g5 = new Game("Cosmic", "MOBA", "cross platform", new List<User>{u5}, new List<Review>{r5}, new List<Mod>{m1, m5});

// Add Games for Users
u1.OwnedGames = new List<Game> { g1, g2, g3, g4, g5 };
u2.OwnedGames = new List<Game> { g1, g2, g3, g4, g5 };
u3.OwnedGames = new List<Game> { g1, g2, g3, g4, g5 };
u4.OwnedGames = new List<Game> { g1, g2, g4 };
u5.OwnedGames = new List<Game> { g1, g5 };
u6.OwnedGames = new List<Game> { g1 };
u7.OwnedGames = new List<Game> { g3, g4 };
u8.OwnedGames = new List<Game> { g2 };

if (printOld)
{
    // Print test
    Console.WriteLine(g1);
    Console.WriteLine(u1);
    Console.WriteLine(r1);
    Console.WriteLine(m1);

    Console.WriteLine(m1.Authors[0].OwnedGames.Count); // Should be 5

    Console.WriteLine(r1.ToString());
    Console.WriteLine(r1.Author.OwnedGames[2].Reviews[0].ToString());


    Console.WriteLine("\n==========================\n\nHASH:");
}

// Hash
// Users
UserTuple uh1 = new UserTuple("Szredor");
UserTuple uh2 = new UserTuple("Driver");
UserTuple uh3 = new UserTuple("Pek");
UserTuple uh4 = new UserTuple("Commander Shepard");
UserTuple uh5 = new UserTuple("MLG");
UserTuple uh6 = new UserTuple("Rondo");
UserTuple uh7 = new UserTuple("lemon");
UserTuple uh8 = new UserTuple("Bonet");

// Mods
ModTuple mh1 = new ModTuple("Clouds", "Super clouds", new List<UserTuple> {uh3});
ModTuple mh2 = new ModTuple("T-pose", "Cow are now T-posing", new List<UserTuple>());
ModTuple mh3 = new ModTuple("Commander Shepard", "I’m Commander Shepard and this is my favorite mod on Smoke",
    new List<UserTuple>{uh4});
ModTuple mh4 = new ModTuple("BTM", "You can now play in BTM’s trains and bytebuses", new List<UserTuple>{uh7, uh8});
ModTuple mh5 = new ModTuple("Cosmic - black hole edition", "Adds REALISTIC black holes", new List<UserTuple>{uh2});
mh1.SetCompatibility(new List<ModTuple> { mh2, mh3, mh4, mh5 });
mh2.SetCompatibility(new List<ModTuple> { mh1, mh3 });
mh3.SetCompatibility(new List<ModTuple> { mh1, mh2, mh4 });
mh4.SetCompatibility(new List<ModTuple> { mh1, mh3 });
mh5.SetCompatibility(new List<ModTuple> { mh1 });

// Reviews
ReviewTuple rh1 = new ReviewTuple("I’m Commander Shepard and this is my favorite game on Smoke", 10, uh4);
ReviewTuple rh2 = new ReviewTuple("The Moo remake sets a new standard for the future of the survival horror series⁠, even if it isn't the sequel I've been pining for.",
    12, uh2);
ReviewTuple rh3 = new ReviewTuple("Universe of Technology is a spectacular 4X game, that manages to shine even when the main campaign doesn't.",
    15, uh7);
ReviewTuple rh4 = new ReviewTuple("Moo’s interesting art design can't save it from its glitches, bugs, and myriad terrible game design decisions, but I love its sound design",
    2, uh8);
ReviewTuple rh5 = new ReviewTuple("I've played this game for years nonstop. Over 8k hours logged (not even joking). And I'm gonna tell you: at this point, the game's just not worth playing anymore. I think it hasn't been worth playing for a year or two now, but I'm only just starting to realize it. It breaks my heart to say that, but that's just the truth of the matter.",
    5, uh1);

// Games
GameTuple gh1 = new GameTuple("Garbage Collector", "simulation", "PW", null, null, new List<ModTuple>{mh1});
GameTuple gh2 = new GameTuple("Universe of Technology", "4X", "bitnix", null, new List<ReviewTuple>{rh3}, new List<ModTuple>{mh1, mh3});
GameTuple gh3 = new GameTuple("Moo", "rogue-like", "bitstation", new List<UserTuple>{uh2}, new List<ReviewTuple>{rh2, rh4}, new List<ModTuple>{mh1, mh2, mh3});
GameTuple gh4 = new GameTuple("Tickets Please", "platformer", "bitbox", new List<UserTuple>{uh1}, new List<ReviewTuple>{rh1}, new List<ModTuple>{mh1, mh3, mh4});
GameTuple gh5 = new GameTuple("Cosmic", "MOBA", "cross platform", new List<UserTuple>{uh5}, new List<ReviewTuple>{rh5}, new List<ModTuple>{mh1, mh5});

// Add Games for Users
uh1.SetOwnedGames(new List<GameTuple> { gh1, gh2, gh3, gh4, gh5 });
uh2.SetOwnedGames(new List<GameTuple> { gh1, gh2, gh3, gh4, gh5 });
uh3.SetOwnedGames(new List<GameTuple> { gh1, gh2, gh3, gh4, gh5 });
uh4.SetOwnedGames(new List<GameTuple> { gh1, gh2, gh4 });
uh5.SetOwnedGames(new List<GameTuple> { gh1, gh5 });
uh6.SetOwnedGames(new List<GameTuple> { gh1 });
uh7.SetOwnedGames(new List<GameTuple> { gh3, gh4 });
uh8.SetOwnedGames(new List<GameTuple> { gh2 });


if (printOld)
{
    // Print test
    Console.WriteLine(gh1);
    var adaptedGh1 = new AdapterGameFromTuple(gh1);
    Console.WriteLine(adaptedGh1);
    Console.WriteLine(uh1);
    Console.WriteLine(new AdapterUserFromTuple(uh1));
    Console.WriteLine(rh1);
    Console.WriteLine(new AdapterReviewFromTuple(rh1));
    Console.WriteLine(mh1);
    Console.WriteLine(new AdapterModFromTuple(mh1));

    Console.WriteLine(mh1.GetAuthors()[0].GetOwnedGames().Count); // Should be 5
    
    var xd = new AdapterModFromTuple(mh1);
    Console.WriteLine((xd).Name);
}


// Zadanie 2:
bool IsMeanBig(Game g, double threshold = 10.0)
{
    double sum = g.Reviews.Aggregate<Review, double>(0, (current, review) => current + review.Rating);

    return (sum / g.Reviews.Count) > threshold;
}

if (printOld)
{
    void PrintListForBigGames(List<Game> listOfGames)
    {
        foreach (var game in listOfGames.Where(game => IsMeanBig(game)))
        {
            Console.WriteLine(game);
        }
    }

    Console.WriteLine("\n\nPrint all games with big mean:");
    PrintListForBigGames(new List<Game> { g1, g2, g3, g4, g5 });

    Console.WriteLine("\n\nPrint all games Tuple with big mean:");
    PrintListForBigGames(new List<Game>(new List<AdapterGameFromTuple>
    {
        new(gh1),
        new(gh2),
        new(gh3),
        new(gh4),
        new(gh5)
    }));
}

var x = new MyBinaryTree<Game>();

Game gAdapt1 = new AdapterGameFromTuple(gh1);
Game gAdapt2 = new AdapterGameFromTuple(gh2);
Game gAdapt3 = new AdapterGameFromTuple(gh3);
Game gAdapt4 = new AdapterGameFromTuple(gh4);
Game gAdapt5 = new AdapterGameFromTuple(gh5);
x.Add(g1);
x.Add(g2);
x.Add(g3);
x.Add(g4);
x.Add(g5);
/*
x.Add(gAdapt1);
x.Add(gAdapt2);
x.Add(gAdapt3);
x.Add(gAdapt4);
x.Add(gAdapt5);*/

IEnumerator<Game> myEnumerator = x.GetEnumerator();
while (myEnumerator.MoveNext())
{
    Console.WriteLine(myEnumerator.Current);
}

Console.WriteLine("======================REVERSE======================");
myEnumerator = x.GetReverseEnumerator();
while (myEnumerator.MoveNext())
{
    Console.WriteLine(myEnumerator.Current);
}

Console.WriteLine("======================REMOVE======================");



x.Remove(g1);
x.Remove(g2);
x.Remove(g3);
x.Remove(g4);
x.Remove(g5);
/*
x.Remove(gAdapt1);
x.Remove(gAdapt2);
x.Remove(gAdapt3);
x.Remove(gAdapt4);
x.Remove(gAdapt5);*/

myEnumerator = x.GetEnumerator();
while (myEnumerator.MoveNext())
{
    Console.WriteLine(myEnumerator.Current);
}


// Mam funkcje IsMeanBig(Game g, double threshold = 10.0) :D
// Pierwsza gra z duza srednia:
/*
Console.WriteLine(AlgorithmsOnICollection.Find<Game>(x!, game => IsMeanBig(game)));
Console.WriteLine("=======================\nWszystkie:");
AlgorithmsOnICollection.Print<Game>(x!, game => IsMeanBig(game));
*/