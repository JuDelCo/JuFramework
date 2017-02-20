
namespace JuFramework
{
	public partial struct Quat
	{
		public static implicit operator UnityEngine.Quaternion(Quat q)
		{
			return new UnityEngine.Quaternion(q.x, q.y, q.z, q.w);
		}

		public static explicit operator Quat(UnityEngine.Quaternion q)
		{
			return new Quat(q.x, q.y, q.z, q.w);
		}
	}
}
