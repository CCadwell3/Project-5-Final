using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private Image weaponIcon;

    [SerializeField]
    private Sprite unarmed;

    [SerializeField]
    private Text lifeText;

    [Header("Pause Screen")]
    public Image pauseScreen;

    public Button resumeButton;
    public Button quitButton;
    public Text pauseText;
    public Text resumeButtonText;
    public Text quitButtonText;

    [Header("Game Over Screen")]
    public Text gameOverText;

    public Button gameOverResumeButton;
    public Text gameOverResumeText;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();//find the GM

        //hide stuff .enabled does not properly hide buttons
        pauseScreen.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
        resumeButtonText.gameObject.SetActive(false);
        quitButtonText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverResumeButton.gameObject.SetActive(false);
        gameOverResumeText.gameObject.SetActive(false);
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

    public void PauseMenu()
    {
        //show pause menu items
        pauseScreen.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        pauseText.gameObject.SetActive(true);
        resumeButtonText.gameObject.SetActive(true);
        quitButtonText.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        //resume game
        //hide again
        pauseScreen.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
        resumeButtonText.gameObject.SetActive(false);
        quitButtonText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameManager.Unpause();
    }

    public void GameOver()
    {
        //show game over screen
        pauseScreen.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        quitButtonText.gameObject.SetActive(true);
        gameOverResumeButton.gameObject.SetActive(true);
        gameOverResumeText.gameObject.SetActive(true);
    }
    public void GameOverResume()
    {
        //resume game
        //hide again
        pauseScreen.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        quitButtonText.gameObject.SetActive(false);
        gameOverResumeButton.gameObject.SetActive(false);
        gameOverResumeText.gameObject.SetActive(false);
    }
}