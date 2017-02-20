
namespace JuFramework
{
	public class UnityDebug : Debug
	{
		public UnityDebug(LogLevel minLogLevel = LogLevel.Info) : base(minLogLevel)
		{
		}

		public override void Log(object obj)
		{
			if(GetMinLogLevel() <= LogLevel.Debug)
			{
				UnityEngine.Debug.Log(obj);
			}
		}

		public override void Log(string message, params object[] args)
		{
			if(GetMinLogLevel() <= LogLevel.Debug)
			{
				UnityEngine.Debug.Log(string.Format(GetCurrentTime() + " " + message, args));
			}
		}

		public override void Info(string message, params object[] args)
		{
			if(GetMinLogLevel() <= LogLevel.Info)
			{
				UnityEngine.Debug.Log(string.Format(GetCurrentTime() + " " + message, args));
			}
		}

		public override void Notice(string message, params object[] args)
		{
			if(GetMinLogLevel() <= LogLevel.Notice)
			{
				UnityEngine.Debug.Log(string.Format(GetCurrentTime() + " " + message, args));
			}
		}

		public override void Warning(string message, params object[] args)
		{
			if(GetMinLogLevel() <= LogLevel.Warning)
			{
				UnityEngine.Debug.LogWarning(string.Format(GetCurrentTime() + " " + message, args));
			}
		}

		public override void Error(string message, params object[] args)
		{
			if(GetMinLogLevel() <= LogLevel.Error)
			{
				UnityEngine.Debug.LogError(string.Format(GetCurrentTime() + " " + message, args));
			}
		}
	}
}
