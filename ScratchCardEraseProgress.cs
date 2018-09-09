using UnityEngine;

public class ScratchCardEraseProgress : MonoBehaviour
{
	public ScratchCardMain Card;

	public event ProgressHandler OnProgress;
	public event ProgressHandler OnCompleted;
	public delegate void ProgressHandler(float progress);

	private Camera thisCamera;
	private RenderTexture renderPercent;
	private Vector3 RightUp = new Vector3(1, 1, 0);
	private float currentProgress;
	private bool isCompleted;

	void Start()
	{
		CreateRenderTexture();
	}

	void OnPostRender()
	{
		if (Card.IsScratching)
		{
			GL.PushMatrix();
			Card.Progress.SetPass(0);
			GL.LoadOrtho();
			GL.Begin(GL.QUADS);
			GL.Color(Color.white);
			GL.TexCoord(Vector3.zero);
			GL.Vertex3(0, 0, 0);
			GL.TexCoord(Vector3.up);
			GL.Vertex3(0, 1, 0);
			GL.TexCoord(RightUp);
			GL.Vertex3(1, 1, 0);
			GL.TexCoord(Vector3.right);
			GL.Vertex3(1, 0, 0);
			GL.End();
			GL.PopMatrix();

			CalcProgress();
		}
	}

	private void CreateRenderTexture()
	{
		thisCamera = GetComponent<Camera>();
		if (thisCamera != null)
		{
			renderPercent = new RenderTexture(1, 1, 0, RenderTextureFormat.ARGB32);
			renderPercent.Create();
			thisCamera.targetTexture = renderPercent;
		}
		else
		{
			Debug.LogError("Camera not found!");
		}
	}

	private void CalcProgress()
	{
		if (!isCompleted)
		{
			var myTexture2D = new Texture2D(renderPercent.width, renderPercent.height, TextureFormat.ARGB32, false, true);
			myTexture2D.ReadPixels(new Rect(0, 0, renderPercent.width, renderPercent.height), 0, 0, false);
			myTexture2D.Apply();
			var red = myTexture2D.GetPixel(0, 0).r;
			currentProgress = red;
			if (OnProgress != null)
			{
				OnProgress(red);
			}
			if (red == 1f)
			{
				if (OnCompleted != null)
				{
					OnCompleted(red);
				}
				isCompleted = true;
			}
		}
	}

	public float GetProgress()
	{
		return currentProgress;
	}
}