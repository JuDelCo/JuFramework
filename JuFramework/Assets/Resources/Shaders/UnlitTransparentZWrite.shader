
Shader "Unlit/Transparent ZWrite"
{
	Properties
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			Lighting Off
			SetTexture [_MainTex] { combine texture }
		}
	}
}
