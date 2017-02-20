using System.Collections;
using System.Collections.Generic;

namespace JuFramework.TileMapData
{
	public class AnimationCollection : IEnumerable<AnimationFrame>
	{
		public readonly List<AnimationFrame> frames = new List<AnimationFrame>();

		public IEnumerator<AnimationFrame> GetEnumerator()
		{
			return frames.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return frames.GetEnumerator();
		}
	}
}
