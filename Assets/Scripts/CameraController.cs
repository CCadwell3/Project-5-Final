using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Distance from player to camera")]
    public float camHeight = 7;

    private GameManager gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameManager.paused)//if game manager is paused
        {
            return;//exit loop
        }
        if (gameManager.player)
        {
            transform.position = gameManager.player.transform.position + new Vector3(0, camHeight, 0);//tell the camera to move to a point 7 units above the player
        }
        else
        {
            //do nothing
        }
    }
}