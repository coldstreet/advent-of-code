namespace AdventOfCode2017
{
	public class LogEntry
	{
		public LogEntry(DateTime dateEntry, int id, string log)
		{
			DateEntry = dateEntry;
			Id = id;
			Log = log;
		}

		public DateTime DateEntry { get; }
		public int Id { get; }
		public string Log { get; }
	}

	public static class ReposeRecord
	{
		public static int FindIdMinuteCombo(string[] inputs)
		{
			List<LogEntry> logEntries = new List<LogEntry>();
			foreach (var input in inputs)
			{
				var date = input.Substring(1, 16);
				var id = "0";
				string log;
				if (input.Contains("Guard #"))
				{
					id = input.Substring(26, 4);
					log = input.Substring(31);
				}
				else
				{
					log = input.Substring(19);
				}

				LogEntry logEntry = new LogEntry(DateTime.Parse(date), int.Parse(id), log);
				logEntries.Add(logEntry);
			}

			LogEntry[] sortedLogEntries = logEntries.OrderBy(x => x.DateEntry).ToArray();


			return 0;
		}
	}
}
