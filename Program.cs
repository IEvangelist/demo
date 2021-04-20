using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;
using static System.Text.Encoding;

ExhaustiveCaseGuardPatternMatching();

static string ExhaustiveExample<T>(IEnumerable<T>? sequence) => sequence switch
{
    T[] { Length: 0 } => "Zero length array",
    T[] { Length: 1 } array => "Single item array",
    T[] { Length: 2 } array => "Array with two things",
    T[] array => "An array with more than two things",

    IEnumerable<T> source when !source.Any() => "Any empty enumerable",
    IEnumerable<T> source when source.Count() < 3 => "A small enumerable",

    IList<T> list => "Some list",

    null => "🤣", // throw new ArgumentNullException(nameof(sequence)),
    _ => "Catch all, must come after null-arm"
};

static void ExhaustiveCaseGuardPatternMatching()
{
    #region Print section details
    OutputEncoding = UTF8;

    WriteLine();
    WriteLine("Switch Expressions: with exhaustive case guard pattern matching 🚀");
    WriteLine();
    #endregion

    WriteLine(ExhaustiveExample(Array.Empty<int>()));           // Zero length array
    WriteLine(ExhaustiveExample(new[] { 1 }));                  // Singe item array
    WriteLine(ExhaustiveExample(new[] { 7, 7 }));               // Array with two things
    WriteLine(ExhaustiveExample(new List<string> { "Hi!" }));   // Small enumerable
    WriteLine(ExhaustiveExample(Enumerable.Range(0, 3)));       // Catch all, due to length

    IReadOnlyList<int> readOnlyList = new List<int> { 4, 5, 6, 7 }.AsReadOnly();
    WriteLine(ExhaustiveExample(readOnlyList));                 // IList<int>
    WriteLine(ExhaustiveExample<int>(null));                    // 🤣
}