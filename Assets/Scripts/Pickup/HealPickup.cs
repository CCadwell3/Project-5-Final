using UnityEngine;

public class HealPickup : Pickups
{
    [SerializeField, Tooltip("How much healing to apply")]
    private float healing = 15;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnPickup(Player player)
    {
        player.health.Heal(healing);
        base.OnPickup(player);
    }
}