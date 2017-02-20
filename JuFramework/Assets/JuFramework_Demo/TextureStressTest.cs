using JuFramework;

public class TextureStressTest : UnityEngine.MonoBehaviour
{
	public UnityEngine.MeshRenderer meshRenderer;

	//private Texture sprite;
	private Texture texture;
	private Material material;
	private UnityEngine.Color[] pixels;
	private int height;
	private int width;

	// 480x270 -> 129600 pixels
	private void Start()
	{
		//sprite = Core.resource.LoadTexture("Tilesets/lamp-16x16_2");
		texture = Core.resource.CreateTexture(480, 270);
		height = texture.GetHeight();
		width = texture.GetWidth();
		pixels = ((UnityTexture)texture).GetUnityTexture2D().GetPixels();

		material = Core.resource.CreateMaterial("Sprites/Default");
		material.Set("_MainTex", texture);

		meshRenderer.material = (UnityMaterial)material;
	}

	private void Update()
	{
		for (int i = 0; i < 100; ++i)
		{
			//DrawTexture(Random.Int(width - 1), Random.Int(height - 1), sprite);
			DrawRectangle(Random.Int(width - 1), Random.Int(height - 1), 120, 50, Random.Color());
		}

		//((UnityTexture)texture).GetUnityTexture2D().SetPixels(pixels);
		texture.Apply();
	}

	private void DrawRectangleBorder(int posX, int posY, int width, int height, Color color)
	{
		if(posX + width > this.width)
		{
			width = this.width - posX;
		}

		if(posY + height > this.height)
		{
			height = this.height - posY;
		}

		if(posY > 0)
		{
			for (int x = posX; x < (posX + width); ++x)
			{
				pixels[posY * this.width + x] = color;
			}
		}

		if(posY + height < this.height)
		{
			for (int x = posX; x < (posX + width); ++x)
			{
				pixels[(posY + height) * this.width + x] = color;
			}
		}

		if(posX > 0)
		{
			for (int y = posY; y < (posY + height); ++y)
			{
				pixels[y * this.width + posX] = color;
			}
		}

		if(posX + width < this.width)
		{
			for (int y = posY; y < (posY + height); ++y)
			{
				pixels[y * this.width + (posX + width)] = color;
			}
		}
	}

	private void DrawRectangle(int posX, int posY, int width, int height, Color color)
	{
		if(posX + width > this.width)
		{
			width = this.width - posX;
		}

		if(posY + height > this.height)
		{
			height = this.height - posY;
		}

		var pixels = new UnityEngine.Color[width * height];

		for (int y = 0; y < height; ++y)
		{
			for (int x = 0; x < width; ++x)
			{
				pixels[y * width + x] = color;
			}
		}

		((UnityTexture)this.texture).GetUnityTexture2D().SetPixels(posX, posY, width, height, pixels);
	}

	private void DrawTexture(int posX, int posY, Texture texture)
	{
		int width = texture.GetWidth();
		int height = texture.GetHeight();

		if(posX + width > this.width)
		{
			width = this.width - posX;
		}

		if(posY + height > this.height)
		{
			height = this.height - posY;
		}

		var texturePixels = ((UnityTexture)texture).GetUnityTexture2D().GetPixels(0, 0, width, height);

		((UnityTexture)this.texture).GetUnityTexture2D().SetPixels(posX, posY, width, height, texturePixels);
	}
}
