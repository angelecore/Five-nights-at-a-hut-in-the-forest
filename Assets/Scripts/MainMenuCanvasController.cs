using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvasController : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainMenu;

    [SerializeField]
    private RectTransform optionsMenu;

    [SerializeField]
    private Slider musicVolumeSlider;

    [SerializeField]
    private Slider effectsVolumeSlider;

    [SerializeField]
    private MixerController mixerController;

    public Text Highscorefield;

    int highscore => PlayerPrefs.GetInt("Highscore", 1);

    private static void Show(Component component)
    {   
        component.gameObject.SetActive(true);
    }

    private static void Hide(Component component)
    {
        component.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
        UpdateSliders();
    }

    public void StartGame()
    {
        Scenes.LoadNextScene();
    }

    public void ExitGame()
    {
        Scenes.ExitGame();
    }

    public void ShowMainMenu()
    {
        Highscorefield.text = $"Current HighScore: {highscore}";
        Show(mainMenu);
        Hide(optionsMenu);
    }

    public void ShowOptionsMenu()
    {
        Hide(mainMenu);
        Show(optionsMenu);
    }

    public void SetEffectsVolumeFromSlider()
    {
        mixerController.SetEffectsVolume(effectsVolumeSlider.value);
    }

    public void SetMusicVolumeFromSlider()
    {
        mixerController.SetMusicVolume(musicVolumeSlider.value);
    }

    private void UpdateSliders()
    {
        musicVolumeSlider.SetValueWithoutNotify(mixerController.MusicVolume);
        effectsVolumeSlider.SetValueWithoutNotify(mixerController.EffectsVolume);
    }
}
