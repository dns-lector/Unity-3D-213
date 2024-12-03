using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    private GameObject content;
    private Slider effectsVolumeSlider;
    private Toggle muteAllToggle;
    private Slider sensXSlider;
    private Slider sensYSlider;

    #region ambientVolume
    private Slider ambientVolumeSlider;
    private float defaultAmbientVolume;
    private void LoadAmbient()
    {
        ambientVolumeSlider = this.transform.Find("Content/Sound/AmbientSlider").GetComponent<Slider>();
        defaultAmbientVolume = ambientVolumeSlider.value;
        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume)))
        {
            GameState.ambientVolume = PlayerPrefs.GetFloat(nameof(GameState.ambientVolume));
            ambientVolumeSlider.value = GameState.ambientVolume;
        }
        else
        {
            GameState.ambientVolume = ambientVolumeSlider.value;
        }
    }
    public void OnAmbientVolumeChanged(Single value)
    {
        GameState.ambientVolume = value;
    }
    #endregion

    #region difficultyDropdown
    private TMPro.TMP_Dropdown difficultyDropdown;
    private int defaultDifficulty;
    private void LoadDifficulty()
    {
        difficultyDropdown = this.transform.Find("Content/Difficulty/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        defaultDifficulty = difficultyDropdown.value;
        if (PlayerPrefs.HasKey(nameof(GameState.difficulty)))
        {
            GameState.difficulty = (GameState.GameDifficulty) PlayerPrefs.GetInt(nameof(GameState.difficulty));
            difficultyDropdown.value = (int) GameState.difficulty;
        }
        else
        {
            GameState.difficulty = (GameState.GameDifficulty) difficultyDropdown.value;
        }
    }
    public void OnDifficultyChanged(int selectedIndex)
    {
        GameState.difficulty = (GameState.GameDifficulty) selectedIndex;
        // Debug.Log(GameState.difficulty);
    }
    #endregion

    void Start()
    {
        Transform contentTransform = this.transform.Find("Content");
        content = contentTransform.gameObject;

        LoadAmbient();
        LoadDifficulty();

        muteAllToggle = contentTransform.Find("Sound/MuteAllToggle").GetComponent<Toggle>();
        GameState.isMuted = muteAllToggle.isOn;
        sensXSlider = contentTransform.Find("Control/SensXSlider").GetComponent<Slider>();
        GameState.sensitivityLookX = sensXSlider.value;
        sensYSlider = contentTransform.Find("Control/SensYSlider").GetComponent<Slider>();
        GameState.sensitivityLookY = sensYSlider.value;

        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
            content.SetActive( ! content.activeInHierarchy );
        }
    }

    public void OnDefaultsButtonClick()
    {
        ambientVolumeSlider.value = defaultAmbientVolume;
        difficultyDropdown.value = defaultDifficulty;
    }
    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(GameState.ambientVolume), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(GameState.effectsVolume), GameState.effectsVolume);
        PlayerPrefs.SetInt(nameof(GameState.isMuted), GameState.isMuted ? 1 : 0);
        PlayerPrefs.SetInt(nameof(GameState.difficulty), (int)GameState.difficulty);
        PlayerPrefs.Save();
    }

    public void OnSensXChanged(Single value)
    {
        GameState.sensitivityLookX = value;
    }
    public void OnSensYChanged(Single value)
    {
        GameState.sensitivityLookY = value;
    }
    public void OnEffectsVolumeChanged(Single value)
    {
        GameState.effectsVolume = value;
    }
   
    public void OnMuteAllChanged(bool value)
    {
        GameState.isMuted = value;
    }

}
/* Д.З. Завершити роботу з меню "налаштування"
 * Забезпечити регулювання, збереження, відновлення, скидання 
 * усіх параметрів.
 */
