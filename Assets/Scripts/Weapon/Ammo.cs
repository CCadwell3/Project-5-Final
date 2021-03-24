using UnityEngine;

public class Ammo : Weapons
{
    [Header("Projectile Behavior")]
    public float ttl = 5;

    public float projectileDamage = 25;
    public Rigidbody rbAmmo;
    public Transform from;

    // Start is called before the first frame update
    public override void Start()
    {
        from = transform.root.GetComponent<Transform>();
        transform.parent = null;//remove parent
        Destroy(gameObject, ttl);//destroy after time to live expires
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        GameObject collidedWith = other.gameObject;//reference what we hit
        Health collidedWithHealth = collidedWith.GetComponent<Health>();//reference the health component on other object

        if (collidedWithHealth != null)//if object has health
        {
            collidedWithHealth.Damage(projectileDamage);//send projectile damage to other objects damage function
        }
        Destroy(gameObject);//destroy projectile
    }
}