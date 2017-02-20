using UnityEngine;
using System.Collections.Generic;
using JuFramework.TileMapTiledImporter;

namespace JuFramework
{
	public class TileMapTiledRenderer : MonoBehaviour
	{
		public TiledFileObject tiledFileObject;
		public bool generated;

		private void Start()
		{
			if(generated)
			{
				return;
			}

			if(tiledFileObject == null || tiledFileObject.type != TiledFileObjectType.TileMap)
			{
				return;
			}

			var tileMap = (new TiledImporter()).ParseMap(tiledFileObject.data);

			GenerateTileMap((new TileMapMeshGenerator()).GenerateLayers(tileMap));
		}

		public void GenerateTileMap(List<TileMapMeshLayer> layers)
		{
			for (int layerIndex = 0; layerIndex < layers.Count; ++layerIndex)
			{
				var layer = layers[layerIndex];

				var layerGameObject = new GameObject();
				layerGameObject.name = "Layer_" + layerIndex + "_" + layer.name;
				layerGameObject.transform.position = new Vector3f(0, 0, (layers.Count - 1) - layerIndex);
				layerGameObject.transform.SetParent(this.transform, false);

				if (! layer.visible)
				{
					layerGameObject.SetActive(false);
				}

				var material = new UnityMaterial(new UnityShader(UnityEngine.Shader.Find("Unlit/Texture")));
				material.Set("_MainTex", layer.texture);

				for (int meshIndex = 0; meshIndex < layer.chunks.Count; ++meshIndex)
				{
					var meshChunkGameObject = new GameObject();
					meshChunkGameObject.name = "TileLayer Chunk " + meshIndex;
					meshChunkGameObject.transform.SetParent(layerGameObject.transform, false);
					meshChunkGameObject.AddComponent<MeshFilter>().mesh = (UnityMesh)layer.chunks[meshIndex];

					var meshRenderer = meshChunkGameObject.AddComponent<MeshRenderer>();
					meshRenderer.material = material;
					meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
					meshRenderer.receiveShadows = false;
					meshRenderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
					meshRenderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
				}
			}
		}
	}
}
