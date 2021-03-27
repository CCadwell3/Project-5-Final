using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Header("Projectile Behavior")]
    public float ttl = 5;
    public float projectileDamage = 25;
    public float projectileSpeed;
    public Rigidbody rbAmmo;
    public Transform from;

    // Start is called before the first frame update
    public void Start()
    {
        from = transform.root.Find("FirePoint");
        rbAmmo.velocity = transform.TransformDirection(Vector3.forward * projectileSpeed); ;//add speed to projectile
        transform.parent = null;//remove parent
        Destroy(gameObject, ttl);//destroy after time to live expires
    }

    // Update is called once per frame
    public void Update()
    {
    }

    public void OnTriggerEnter(Collider other)
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