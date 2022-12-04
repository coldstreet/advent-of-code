using System.Linq;

namespace AdventOfCode2022;

public static class Day03_RucksackReorganization
{
    public static long FindSumOfSharedItems(string[] input)
    {
        // parse input
        var rucksacks = new List<(List<char>, List<char>)>();
        foreach (var rucksackItems in input)
        {
            var items1 = rucksackItems.Substring(0, rucksackItems.Length / 2);
            var items2 = rucksackItems.Substring(rucksackItems.Length / 2);
            var rucksack = (new List<char>(), new List<char>());

            for (var i = 0; i < items1.Length; i++)
            {
                rucksack.Item1.Add(items1[i]);  
                rucksack.Item2.Add(items2[i]); 
            }

            rucksacks.Add(rucksack);
        }

        var sum = 0;
        foreach (var ruckSack in rucksacks)
        {
            var sharedItem = ruckSack.Item1.Intersect(ruckSack.Item2).ToArray();
            var intValue = Convert.ToInt32(sharedItem[0]);
            sum += intValue >= 97 ? intValue - 96 : intValue - 38;
        }

        return sum;
    }

    public static long FindSumOfBadges(string[] input)
    {
        // parse input
        var rucksacks = new List<List<char>>();
        foreach (var rucksackItems in input)
        {
            rucksacks.Add(rucksackItems.ToCharArray().ToList());
        }

        var sum = 0;
        for (int i = 0; i < rucksacks.Count; i+=3)
        {
            var range = new Range(i, i + 3);
            var setOfThreeRucksacks = rucksacks.Take(range);
            
            var intersection = setOfThreeRucksacks
                .Skip(1)
                .Aggregate(new List<char>(setOfThreeRucksacks.First()), (a, b) => a.Intersect(b).ToList());

            var intValue = Convert.ToInt32(intersection[0]);
            sum += intValue >= 97 ? intValue - 96 : intValue - 38;
        }

        return sum;
    }
}