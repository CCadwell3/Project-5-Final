using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class UIManager : MonoBehaviour
{
    //remember to add footsteps


    private GameManager gameManager;
    [SerializeField, Tooltip("Main audio control for everything")]
    private AudioMixer mixer;


    [SerializeField, Tooltip("The slider value vs decibel volume curve")]
    private AnimationCurve volumeVsDecibels;

    [SerializeField]
    private Image weaponIcon;

    [SerializeField]
    private Sprite unarmed;

    [SerializeField]
    private Text lifeText;

    [Header("Prefab Canvas")]
    public Canvas mainMenu;
    public Canvas pauseMenu;
    public Canvas gameOverMenu;
    public Canvas settingsMenu;
    [Header("Elements")]
    public Button applyButton;
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Toggle fullScreenToggle;
    public Dropdown resolutionDropDown;
    public Dropdown qualityDropDown;
    public AudioSource audioSource;

    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public Resolution[] resolutions;
    
    // Start is called before the first frame update
    private void Start()
    {
        resolutions = Screen.resolutions;//get resolution array
        //build dropdown for screen resolutions and quality levels
        List<string> options = new List<string>();//create list to hold resolutions

        resolutionDropDown.ClearOptions();//clear anything that might be there

        for (int index = 0; index < resolutions.Length; index++)//loop through all possible resolutions system can use
        {
            options.Add(string.Format("{0} x {1}", resolutions[index].width, resolutions[index].height));//add each to the list
        }

        resolutionDropDown.AddOptions(options);//add list to the dropdown

        // Build quality levels
        qualityDropDown.ClearOptions();//clear anything that might already exist
        qualityDropDown.AddOptions(QualitySettings.names.ToList());//add the quality levels to the dropdown

        if (SceneManager.GetActiveScene().name == ("MainMenue"))//if we are in the main menu
        {
            DisableSettings();
        }
        else if (SceneManager.GetActiveScene().name == ("Main"))
        {
            gameManager = FindObjectOfType<GameManager>();
            DisableMain();
            DisablePauseMenu();
            DisableSettings();
            DisableGameOver();
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
            lifeText.text = string.Format("X {0}", gameManager.lives);//update lives text

        if (gameManager)//we have an active game manager
        {
            if (gameManager.player.equippedWeapon != null)//and there is an equipped weapon
                weaponIcon.overrideSprite = gameManager.player.equippedWeapon.icon;//stick the icon in the weapon icon slot
            else
            {
                weaponIcon.overrideSprite = unarmed;
            }
        }
    }
    
    public void OnMouseOver()
    {
        audioSource.PlayOneShot(buttonHover);
    }
    public void ButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }
    //show pause menu
    public void EnablePauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
    }
    //clear all menus
    public void DisablePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
    }
    //show game over
    public void EnableGameOverMenu()
    {
        gameOverMenu.gameObject.SetActive(true);
    }
    //exit game
    public void QuitGame()
    {
        Application.Quit();//quit game
        //#if is preprossor code, if unity editor is running, this statement is true
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//stop editor
#endif
    }
    //start the game
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
    //resume from game over screen
    public void GameOverResume()
    {
        gameOverMenu.gameObject.SetActive(false);
    }

    //settings
    //show settings menue
    public void EnableSettings()
    {
        settingsMenu.gameObject.SetActive(true);

        //set volume sliders to whatever they are in player prefs, if non existant, set to max value
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.maxValue);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.maxValue);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("Effects Volume", effectsVolumeSlider.maxValue);

        fullScreenToggle.isOn = Screen.fullScreen;//check or uncheck box based on screen status
        qualityDropDown.value = QualitySettings.GetQualityLevel();//set dropdown to current state
        applyButton.interactable = false;//no touching the apply button
    }
    //hides settings menu
    public void DisableSettings()
    {
        settingsMenu.gameObject.SetActive(false);
    }
    //shows main menu
    public void EnableMain()
    {
        mainMenu.gameObject.SetActive(true);
    }
    //hides main menu
    public void DisableMain() 
    {
        mainMenu.gameObject.SetActive(false);
    }
    //hide pause menu
    public void DisableGameOver()
    {
        gameOverMenu.gameObject.SetActive(false);
    }

    //apply settings

    //be sure to use dynamic functions in inspector
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);//pass the dropdown selection to qualitylevel
    }
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];//pass index to resolution array
        Screen.SetResolution(resolution.width, resolution.height, fullScreenToggle);//set resolution and fullscreen
    }
    public void ScreenToggle(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
    public void BarUpdate()
    {
        mixer.SetFloat("masterVolume", volumeVsDecibels.Evaluate(masterVolumeSlider.value));//use the animation curve to set volume
        mixer.SetFloat("musicVolume", volumeVsDecibels.Evaluate(musicVolumeSlider.value));
        mixer.SetFloat("effectsVolume", volumeVsDecibels.Evaluate(effectsVolumeSlider.value));
        //save playerprefs

    }

}