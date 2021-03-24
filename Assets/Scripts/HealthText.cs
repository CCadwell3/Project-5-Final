using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HealthText : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    public Health health;

    [SerializeField]
    private Text text;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();//reference game manager
        text = GetComponent<Text>();//set text component
    }

    // Update is called once per frame
    private void Update()
    {
        health = gameManager.player.health;//get player health object
        float pct = (health.health / health.maxHealth) * 100f;
        text.text = string.Format("{0}%", pct);
    }
}