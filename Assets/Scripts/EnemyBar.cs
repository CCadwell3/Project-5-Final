using UnityEngine;
using UnityEngine.UI;

public class EnemyBar : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private Image bar;

    [SerializeField]
    private Health health;

    // Start is called before the first frame update
    private void Start()
    {
        health = transform.root.GetComponent<Health>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void LateUpdate()
    {
        if (health)//if there is a health component
        {
            float pct = health.health / health.maxHealth;//get a number
            bar.fillAmount = pct;//set fill ammount for bar
        }
    }
}