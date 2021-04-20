using System;
using System.Collections.Generic;
using static System.Console;
using static System.Text.Encoding;

KeyValuePairs();
CustomObjects();
ExtensionMethods();
Records();

static void KeyValuePairs()
{
    #region Print section details
    OutputEncoding = UTF8;

    WriteLine();
    WriteLine("📦 💜 Deconstructing non-tuple types. 💜 📦");
    WriteLine();

    #endregion

    Dictionary<string, int> favoriteLanguages =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["C#"] = 1,
            ["TypeScript"] = 2,
            ["F#"] = 3,
            ["Rust"] = 4,
            ["Python"] = 5
        };

    foreach ((string lang, int rank) in favoriteLanguages)
    {
        WriteLine($"My {ToOrdinalString(rank)} favorite programming language is {lang}");
    }

    WriteLine();
}

static void CustomObjects()
{
    // Instantiate an album object 🤘🏽
    Album album = new(
        id: 7,
        name: "Adrenaline, by Deftones",
        askingPrice: 9.99m,
        releaseDate: new DateTime(1995, 10, 3));

    // And even extension methods.
    var (_, name, askingPrice, (hasDate, date)) = album;
    WriteLine($"The first CD I bought was \"{name}\". 🎸 🎤 🥁");
    WriteLine($"It released on {date:MMMM} {ToOrdinalString(date.Day)}, {date.Year}.");
    WriteLine($"Priced @ {askingPrice:c} 😱.");

    WriteLine();
}

static void ExtensionMethods()
{
    (bool hasValue, int value) = new int?();
    WriteLine($"Has value = {hasValue}, value = {value}");

    (hasValue, value) = new int?(77);
    WriteLine($"Has value = {hasValue}, value = {value}");
    WriteLine();
}

static void Records()
{
    var (name, date) = new CompactDisc("Deftones", new(2003, 5, 20));
    WriteLine(
        $"The self-titled album, \"{name}\" " + 
        $"was released on {date:MMMM} {ToOrdinalString(date.Day)}, {date.Year}!");
}

#region Helper function(s) 🙈 nothing to see here
static string ToOrdinalString(int number) =>
    (number % 10) switch
    {
        1 => number + "st",
        2 => number + "nd",
        3 => number + "rd",
        _ when number % 100 > 10 && number % 100 < 20 => number + "th",
        _ => number + "th"
    };
#endregion

public class Album
{
    public Album(
        int id,
        string? name,
        decimal askingPrice,
        DateTime? releaseDate)
    {
        Id = id;
        Name = name;
        AskingPrice = askingPrice;
        ReleaseDate = releaseDate;
    }

    public void Deconstruct(
        out int id,
        out string? name,
        out decimal askingPrice,
        out DateTime? releaseDate)
    {
        id = Id;
        name = Name;
        askingPrice = AskingPrice;
        releaseDate = ReleaseDate;
    }

    public int Id { get; set; }
    public string? Name { get; set; } = null!;
    public decimal AskingPrice { get; set; }
    public DateTime? ReleaseDate { get; set; }
}

static class NullableExtensions
{
    internal static void Deconstruct<T>(
        this T? nullable, out bool hasValue, out T value) where T : struct
        {
            hasValue = nullable.HasValue;
            value = nullable.GetValueOrDefault();
        }
}

record CompactDisc(string Name, DateTime ReleaseDate);