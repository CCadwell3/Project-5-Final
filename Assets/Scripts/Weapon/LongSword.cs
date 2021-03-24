using UnityEngine;

public class LongSword : Weapons
{
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

    public override void MainAttackDown()
    {
        base.MainAttackDown();
    }

    public override void MainAttackUp()
    {
        base.MainAttackUp();
    }

    public override void AltAttackDown()
    {
        Debug.Log("Alt down sent up the chain");
        base.AltAttackDown();
    }

    public override void AltAttackUp()
    {
        base.AltAttackUp();
    }
}