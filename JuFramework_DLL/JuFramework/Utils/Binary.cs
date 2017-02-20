using System.Text;

namespace JuFramework
{
	public partial class Binary
	{
		private readonly byte[] dataBytes;
		private string cachedText = null;

		public byte[] bytes { get { return dataBytes; } }

		public string text
		{
			get
			{
				if(cachedText == null)
				{
					cachedText = Encoding.UTF8.GetString(dataBytes);
				}

				return cachedText;
			}
		}

		public Binary(byte[] bytes)
		{
			dataBytes = bytes;
		}
	}
}
