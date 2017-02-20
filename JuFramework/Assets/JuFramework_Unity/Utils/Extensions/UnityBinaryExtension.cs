
namespace JuFramework
{
	public partial class Binary
	{
		public Binary(UnityEngine.TextAsset textAsset, bool loadText = false)
		{
			dataBytes = textAsset.bytes;

			if(loadText)
			{
				cachedText = textAsset.text;
			}

			UnityEngine.Resources.UnloadAsset(textAsset);
		}
	}
}
