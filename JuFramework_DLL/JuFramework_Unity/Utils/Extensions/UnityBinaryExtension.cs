
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

			// TODO:
			//UnityEngine.Resources.UnloadAsset(textAsset);
			UnityEngine.Object.Destroy(textAsset);
		}
	}
}
