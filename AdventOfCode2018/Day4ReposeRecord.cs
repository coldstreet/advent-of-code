namespace AdventOfCode2018
{
	public class GuardLogEntry
	{
		public GuardLogEntry(DateTime logEntry, int guardNumber, string guardAction)
		{
			LogEntry = logEntry;
			GuardNumber = guardNumber;
			GuardAction = guardAction;
		}

		public DateTime LogEntry { get; }

		public int GuardNumber { get; }

		public string GuardAction { get; }
	}

	public class Day4ReposeRecord
	{
	}
}
