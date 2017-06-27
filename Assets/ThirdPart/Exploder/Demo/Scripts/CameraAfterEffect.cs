using UnityEngine;
using System.Collections;

public enum CameraType
{
	NONE,
	GREEN_VISION,
	THERMAL_VISION,
}
public class CameraAfterEffect : MonoBehaviour
{
	public Shader shaderG;
	public Shader shaderT;
	private Material mat;
	private Shader shaderOri;
	private CameraType camType = CameraType.NONE;
	// Use this for initialization
	void Start ()
	{
		mat = new Material (shaderG);
	}
	public void SetVision(CameraType _camType)
	{
		camType = _camType;
		if (camType == CameraType.GREEN_VISION)
			mat.shader = shaderG;
		else if(camType == CameraType.THERMAL_VISION)
			mat.shader = shaderT;
			
	}
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (camType == CameraType.NONE)
			Graphics.Blit (source, destination);
		else
			Graphics.Blit (source, destination, mat);
	}

}

