
namespace JuFramework
{
	public abstract partial class Graphics
	{
		public abstract void Begin();
		public abstract void End();
		public abstract void Flush();

		public abstract void SetCamera(Camera camera, Vector3f position, Quat rotation);
		public abstract void SetProjectionMatrix(Matrix4 projectionMatrix);
		public abstract void SetWorldToViewMatrix(Matrix4 viewMatrix);
		public abstract void SetModelToWorldMatrix(Matrix4 modelMatrix);
		public abstract void SetViewport(FloatRect viewport);
		public abstract void SetCullingMode(bool inverted);
		public abstract void SetWireframe(bool active);

		public abstract void ClearAuto(bool enabled, Color color);
		public abstract void ClearColor(Color color);
		public abstract void ClearDepth(float depth = 1.0f);
		public abstract void Clear(Color color, float depth = 1.0f);

		public abstract void DrawMesh(Mesh mesh);
	}
}
