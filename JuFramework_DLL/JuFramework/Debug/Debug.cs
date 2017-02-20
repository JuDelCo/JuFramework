using System;
using System.Text;

namespace JuFramework
{
	public abstract class Debug
	{
		public enum LogLevel
		{
			Debug,
			Info,
			Notice,
			Warning,
			Error
		}

		private LogLevel minLogLevel;
		private StringBuilder stringBuilder;

		public Debug(LogLevel minLogLevel = LogLevel.Info)
		{
			this.minLogLevel = minLogLevel;

			stringBuilder = new StringBuilder();
		}

		public LogLevel GetMinLogLevel()
		{
			return minLogLevel;
		}

		public void SetMinLogLevel(LogLevel minLogLevel)
		{
			this.minLogLevel = minLogLevel;
		}

		public abstract void Log(object obj);
		public abstract void Log(string message, params object[] args);
		public abstract void Info(string message, params object[] args);
		public abstract void Notice(string message, params object[] args);
		public abstract void Warning(string message, params object[] args);
		public abstract void Error(string message, params object[] args);

		public string GetCurrentTime()
		{
			var now = DateTime.Now;

			stringBuilder.Length = 0;
			stringBuilder.Capacity = 0;

			stringBuilder.Append(now.Hour.ToString().PadLeft(2, '0'));
			stringBuilder.Append(":");
			stringBuilder.Append(now.Minute.ToString().PadLeft(2, '0'));
			stringBuilder.Append(":");
			stringBuilder.Append(now.Second.ToString().PadLeft(2, '0'));

			return stringBuilder.ToString();
		}
	}
}
