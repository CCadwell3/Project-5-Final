using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private GameManager gameManager;
    public Material bar;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        bar = GetComponent<Image>().material;//get material from image object
    }

    // Update is called once per frame
    private void Update()
    {
        SetHealthBarColor(gameManager.player.health.health / gameManager.player.health.maxHealth);//pass number to color changer
    }

    public void SetHealthBarColor(float value)
    {
        bar.SetFloat("_FillLevel", value);//set fill ammount for orb

        /*old code from color changing life bar
         * 
        if (bar.fillAmount < 0.2f)//if less than 20% health
        {
            bar.color = Color.red;//assign color to health bar
        }
        else if (bar.fillAmount < 0.4f)//less than 40% health
        {
            bar.color = Color.yellow;//set bar to yellow
        }
        else//more than 40% health
        {
            bar.color = Color.green;//green bar
        }
        */
    }
}