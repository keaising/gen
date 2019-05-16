using McMaster.Extensions.CommandLineUtils;
using System;
using System.Linq;

[Command("id", Description = "generate id card codes.")]
public class Id
{
    [Argument(0, Description = "The start of ID card number.", Name = "Start")]
    public string StartWith { get; }

    [Option(ShortName = "l", LongName = "eighteen", ShowInHelpText = true, Description = "18位身份证号")]
	public bool Long { get; set; }

	[Option("-s|--fifteen", Description = "15位身份证号")]
	public bool Short { get; set; }

	public void OnExecute(IConsole console)
    {
        console.WriteLine(StartWith);
		if (Long)
		{
			console.WriteLine(genId());
		}
		else if (Short)
		{
			console.WriteLine("Short");
		}
    }

	private string genId()
	{
		var r = new Random();
		var myValues = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }; 
		var head = Enumerable.Range(0, 6 - StartWith.Length).Select(e => myValues[r.Next(myValues.Length)]);
		var year = r.Next(1970, 2018);
		var month = $"{r.Next(1, 12)}".PadLeft(2, '0');
		var day = $"{r.Next(1, 28)}".PadLeft(2, '0');
		var tail = Enumerable.Range(0, 4).Select(e => myValues[r.Next(myValues.Length)]);
		return StartWith + string.Join("", head) + year + month + day + string.Join("", tail);
	}
}