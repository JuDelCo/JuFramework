
namespace JuFramework
{
	class StopWatch
	{
		private Clock counter;
		private Time pauseStartTime;
		private Time pausedTime;
		private bool started = false;
		private bool paused = false;

		public StopWatch(bool autoStart = true)
		{
			counter = new Clock();

			if(autoStart)
			{
				Start();
			}
		}

		public void Start()
		{
			if(IsStarted())
			{
				return;
			}

			started = true;
			paused = false;

			Reset();
		}

		public void Reset()
		{
			if(! IsStarted())
			{
				return;
			}

			counter.Reset();
			pauseStartTime = Time.Zero();
			pausedTime = Time.Zero();
		}

		public void Pause()
		{
			if(! IsStarted() || IsPaused())
			{
				return;
			}

			pauseStartTime = counter.GetTimeElapsed();
			paused = true;
		}

		public void Resume()
		{
			if(! IsPaused())
			{
				return;
			}

			pausedTime += counter.GetTimeElapsed() - pauseStartTime;
			paused = false;
		}

		public void Stop()
		{
			if(! IsStarted())
			{
				return;
			}

			started = false;
			paused = false;
		}

		public void AddTime(float seconds)
		{
			AddTime(Time.Seconds(seconds));
		}

		public void AddTime(Time time)
		{
			pausedTime -= time;
		}

		public bool IsStarted()
		{
			return started;
		}

		public bool IsPaused()
		{
			return paused;
		}

		public Time GetTimeElapsed()
		{
			if(IsStarted())
			{
				var elapsedTime = counter.GetTimeElapsed();

				if(IsPaused())
				{
					return (elapsedTime - pausedTime - (elapsedTime - pauseStartTime));
				}
				else
				{
					return (elapsedTime - pausedTime);
				}
			}

			return Time.Zero();
		}
	}
}
