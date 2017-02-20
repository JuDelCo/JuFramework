
namespace JuFramework
{
	public class Clock
	{
		private Time startTime;

		public Clock()
		{
			Reset();
		}

		public Time Reset()
		{
			var timeElapsed = GetTimeElapsed();

			startTime = Time.Now();

			return timeElapsed;
		}

		public Time GetTimeElapsed()
		{
			return Time.Now() - startTime;
		}
	}
}
