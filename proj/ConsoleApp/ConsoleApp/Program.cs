using ConsoleApp;

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


// Print test
Console.WriteLine(g1);
Console.WriteLine(u1);
Console.WriteLine(r1);
Console.WriteLine(m1);

Console.WriteLine(m1.Authors[0].OwnedGames.Count); // Should be 5

Console.WriteLine(r1.ToString());
Console.WriteLine(r1.Author.OwnedGames[2].Reviews[0].ToString());


Console.WriteLine("\n==========================\n\nHASH:");

// Hash
// Users
UserHash uh1 = new UserHash("Szredor");
UserHash uh2 = new UserHash("Driver");
UserHash uh3 = new UserHash("Pek");
UserHash uh4 = new UserHash("Commander Shepard");
UserHash uh5 = new UserHash("MLG");
UserHash uh6 = new UserHash("Rondo");
UserHash uh7 = new UserHash("lemon");
UserHash uh8 = new UserHash("Bonet");

// Mods
ModHash mh1 = new ModHash("Clouds", "Super clouds", new List<UserHash> {uh3});
ModHash mh2 = new ModHash("T-pose", "Cow are now T-posing", new List<UserHash>());
ModHash mh3 = new ModHash("Commander Shepard", "I’m Commander Shepard and this is my favorite mod on Smoke",
    new List<UserHash>{uh4});
ModHash mh4 = new ModHash("BTM", "You can now play in BTM’s trains and bytebuses", new List<UserHash>{uh7, uh8});
ModHash mh5 = new ModHash("Cosmic - black hole edition", "Adds REALISTIC black holes", new List<UserHash>{uh2});
mh1.Compatibility = new List<ModHash> { mh2, mh3, mh4, mh5 };
mh2.Compatibility = new List<ModHash> { mh1, mh3 };
mh3.Compatibility = new List<ModHash> { mh1, mh2, mh4 };
mh4.Compatibility = new List<ModHash> { mh1, mh3 };
mh5.Compatibility = new List<ModHash> { mh1 };

// Reviews
ReviewHash rh1 = new ReviewHash("I’m Commander Shepard and this is my favorite game on Smoke", 10, uh4);
ReviewHash rh2 = new ReviewHash("The Moo remake sets a new standard for the future of the survival horror series⁠, even if it isn't the sequel I've been pining for.",
    12, uh2);
ReviewHash rh3 = new ReviewHash("Universe of Technology is a spectacular 4X game, that manages to shine even when the main campaign doesn't.",
    15, uh7);
ReviewHash rh4 = new ReviewHash("Moo’s interesting art design can't save it from its glitches, bugs, and myriad terrible game design decisions, but I love its sound design",
    2, uh8);
ReviewHash rh5 = new ReviewHash("I've played this game for years nonstop. Over 8k hours logged (not even joking). And I'm gonna tell you: at this point, the game's just not worth playing anymore. I think it hasn't been worth playing for a year or two now, but I'm only just starting to realize it. It breaks my heart to say that, but that's just the truth of the matter.",
    5, uh1);

// Games
GameHash gh1 = new GameHash("Garbage Collector", "simulation", "PW", null, null, new List<ModHash>{mh1});
GameHash gh2 = new GameHash("Universe of Technology", "4X", "bitnix", null, new List<ReviewHash>{rh3}, new List<ModHash>{mh1, mh3});
GameHash gh3 = new GameHash("Moo", "rogue-like", "bitstation", new List<UserHash>{uh2}, new List<ReviewHash>{rh2, rh4}, new List<ModHash>{mh1, mh2, mh3});
GameHash gh4 = new GameHash("Tickets Please", "platformer", "bitbox", new List<UserHash>{uh1}, new List<ReviewHash>{rh1}, new List<ModHash>{mh1, mh3, mh4});
GameHash gh5 = new GameHash("Cosmic", "MOBA", "cross platform", new List<UserHash>{uh5}, new List<ReviewHash>{rh5}, new List<ModHash>{mh1, mh5});

// Add Games for Users
uh1.OwnedGames = new List<GameHash> { gh1, gh2, gh3, gh4, gh5 };
uh2.OwnedGames = new List<GameHash> { gh1, gh2, gh3, gh4, gh5 };
uh3.OwnedGames = new List<GameHash> { gh1, gh2, gh3, gh4, gh5 };
uh4.OwnedGames = new List<GameHash> { gh1, gh2, gh4 };
uh5.OwnedGames = new List<GameHash> { gh1, gh5 };
uh6.OwnedGames = new List<GameHash> { gh1 };
uh7.OwnedGames = new List<GameHash> { gh3, gh4 };
uh8.OwnedGames = new List<GameHash> { gh2 };


// Print test
Console.WriteLine(gh1);
var adaptedGh1 = new AdapterGameFromHash(gh1);
Console.WriteLine(adaptedGh1);
Console.WriteLine(uh1);
Console.WriteLine(new AdapterUserFromHash(uh1));
Console.WriteLine(rh1);
Console.WriteLine(new AdapterReviewFromHash(rh1));
Console.WriteLine(mh1);
Console.WriteLine(new AdapterModFromHash(mh1));

Console.WriteLine(mh1.Authors[0].OwnedGames.Count); // Should be 5

var xd = new AdapterModFromHash(mh1);
Console.WriteLine((xd).Name);




// Zadanie 2:
bool IsMeanBig(Game g, double treshold = 10.0)
{
    double sum = 0;
    foreach (Review review in g.Reviews)
    {
        sum += review.Rating;
    }
    
    return (sum / g.Reviews.Count) > treshold;
}