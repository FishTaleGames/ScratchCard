using UnityEngine;
using UnityEngine.UI;

public class ScratchCardUI : MonoBehaviour
{
    public Button restartButton;
    public Texture[] Brushes;
	public Toggle[] BrushToggles;
	public Toggle ProgressToggle;
	public Text ProgressText;

    public ScratchCardController CardController;
    public ScratchCardEraseProgress EraseProgress;

    void Start()
	{
		Application.targetFrameRate = 60;
		ProgressToggle.isOn = PlayerPrefs.GetInt("Toggle", 0) == 0;
		EraseProgress.OnProgress += OnEraseProgress;

        restartButton.onClick.AddListener(Restart);

		for (var i = 0; i < BrushToggles.Length; i++)
		{
			BrushToggles[i].onValueChanged.AddListener(OnChange);
		}
		BrushToggles[PlayerPrefs.GetInt("Brush")].isOn = true;
	}

	public void OnChange(bool val)
	{
		for (var i = 0; i < BrushToggles.Length; i++)
		{
			if (BrushToggles[i].isOn)
			{
                CardController.SetEraseTexture(Brushes[i]);
				PlayerPrefs.SetInt("Brush", i);
				break;
			}
		}
	}

	public void OnCheck(bool check)
	{
		EraseProgress.gameObject.SetActive(ProgressToggle.isOn);
		PlayerPrefs.SetInt("Toggle", ProgressToggle.isOn ? 0 : 1);
	}

	public void Restart()
	{
#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2
		Application.LoadLevel(0);
#else
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
#endif
	}

	public void OnEraseProgress(float progress)
	{
		ProgressText.text = "Progress: " + Mathf.Round(progress * 100f).ToString() + " %";
	}
}