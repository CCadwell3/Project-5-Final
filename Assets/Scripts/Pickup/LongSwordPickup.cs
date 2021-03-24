public class LongSwordPickup : Pickups
{
    public LongSword sword;

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
        player.EquipWeapon(sword);
        base.OnPickup(player);
    }
}