using UnityEngine;

public abstract class Pickups : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        Player player = col.GetComponent<Player>();
        if (player)
        {
            OnPickup(player);
        }
    }

    public virtual void OnPickup(Player player)
    {
        Destroy(gameObject);//remove object
    }
}