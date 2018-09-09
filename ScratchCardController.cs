using UnityEngine;
using UnityEngine.UI;

public class ScratchCardController : MonoBehaviour
{
    public Camera MainCamera;

    public ScratchCardMain Card;
    public ScratchCardEraseProgress Progress;

    public Sprite ScratchSurfaceSprite;
	public Texture EraseTexture;
	
	public GameObject ImageCard;
	public Shader MaskShader;
	public Shader BrushShader;
	public Shader MaskProgressShader;
	
	private Material scratchSurfaceMaterial;
	private Material eraserMaterial;
	private Material progressMaterial;
	
	void Awake()
	{
		if (Card.MainCamera == null)
		{
			Card.MainCamera = MainCamera;
		}
		
		if (Card.ScratchSurface == null)
		{
			scratchSurfaceMaterial = new Material(MaskShader);
			scratchSurfaceMaterial.mainTexture = ScratchSurfaceSprite.texture;
			Card.ScratchSurface = scratchSurfaceMaterial;
		}
		
		if (Card.Eraser == null)
		{
			eraserMaterial = new Material(BrushShader);
			eraserMaterial.mainTexture = EraseTexture;
			Card.Eraser = eraserMaterial;
		}
		
		if (Card.Progress == null)
		{
			progressMaterial = new Material(MaskProgressShader);
			Card.Progress = progressMaterial;
		}
		
		Card.Surface = ImageCard.transform;
		var image = ImageCard.GetComponent<Image>();
		image.sprite = ScratchSurfaceSprite;
		image.material = scratchSurfaceMaterial;
	}
	
	public void SetEraseTexture(Texture texture)
	{
		eraserMaterial.mainTexture = texture;
	}

    public void ResetScratchCard()
    {
        Card.Reset();
    }
}