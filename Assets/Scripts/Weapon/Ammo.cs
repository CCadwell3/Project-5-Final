using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [Header("Projectile Behavior")]
    public float ttl = 5;
    public float projectileDamage = 25;
    public float projectileSpeed;
    public Rigidbody rbAmmo;
    public Transform from;
    public AudioSource aud;
    [Header("Particles")]
    public ParticleSystem particlePre;
    public int particleTTL;
    public AudioClip hitSound;

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
    public void OnCollisionEnter(Collision collision)
    {
        this.gameObject.transform.parent = collision.transform;
        rbAmmo.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(gameObject, particleTTL);//destroy projectile
        aud.PlayOneShot(hitSound);//play sound
        ContactPoint contact = collision.GetContact(0);//get collision at index 0
        Quaternion rot = Quaternion.Inverse(transform.rotation);//reverse direction
        Vector3 pos = contact.point;//set position to contact point
        ParticleSystem particle = (Instantiate(particlePre, pos, rot));//create particle
        particle.Emit(1);//display 1 particle
        
        
        if (collision.gameObject.GetComponent<Pawn>() != null)
        {
            Pawn pawn = collision.gameObject.GetComponent<Pawn>();//get pawn object from hit object
            
            if (pawn.GetComponent<Health>() != null)//if object has health
            {
                Health colHealth = pawn.GetComponent<Health>();//reference the health component on other object
                colHealth.Damage(projectileDamage);//send projectile damage to other objects damage function               
            }          
        }
        
    }
    //can not use trigger and collision at the same time, setting an object to trigger disables collision
    public void OnTriggerEnter(Collider other)
     {
        
    }
    
}