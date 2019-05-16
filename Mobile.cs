using McMaster.Extensions.CommandLineUtils;
using System.Linq;
using System;

[Command("mobile", Description = "generate mobile numbers.")]
public class Mobile
{
    private static readonly Random rand = new Random();
    public void OnExecute(IConsole console)
    {
        console.WriteLine(GenMobile());
    }

    string GenMobile()
    {
        var second = rand.Next(3, 9);
        var tail = Enumerable.Range(1, 9).Select(r => rand.Next(0, 9));
        return $"1{second}{string.Join("", tail)}";
    }
}