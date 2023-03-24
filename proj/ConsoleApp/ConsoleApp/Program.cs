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
Mod m2 = new Mod("T-pose", "Cow are now T-posing", new List<User>{});
Mod m3 = new Mod("Commander Shepard", "I’m Commander Shepard and this is my favorite mod on Smoke",
    new List<User>{u4});
Mod m4 = new Mod("BTM", "You can now play in BTM’s trains and bytebuses", new List<User>{u7, u8});
Mod m5 = new Mod("Cosmic - black hole edition", "Adds REALISTIC black holes", new List<User>{u2});
m1.Compatibility = new List<Mod> { m2, m3, m4, m5 };
m2.Compatibility = new List<Mod> { m1, m3 };
m3.Compatibility = new List<Mod> { m1, m2, m4};
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

Console.WriteLine(m1.Authors[0].OwnedGames.Count); // Should be all 5
