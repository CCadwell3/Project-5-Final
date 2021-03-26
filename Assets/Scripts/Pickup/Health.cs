using UnityEngine;
using UnityEngine.Events;

public class Health : Pickups
{
    [Header("Events")]
    [SerializeField, Tooltip("Raised when object is healed")]
    private UnityEvent onHeal;

    [SerializeField, Tooltip("Raised when object is damaged")]
    private UnityEvent onDamage;

    [SerializeField, Tooltip("Raised when object dies")]
    private UnityEvent onDie;

    [SerializeField, Tooltip("Seconds to ragdoll for")]
    private int ragTimer = 5;

    [SerializeField, Tooltip("Current Health")]
    public float health;

    public float maxHealth = 100;
    public float percent;

    private float overKill;
    private float overHeal;

    public bool isDead = false;

    public AudioClip deathSound;
    public AudioClip[] hitsounds;
    public AudioSource audiosource;

    // Start is called before the first frame update
    public override void Start()
    {
        health = maxHealth;//give object some life
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        percent = health / maxHealth;
        base.Update();
    }

    //how to handle damage
    public void Damage(float damage)
    {
        int rnd = Random.Range(0, hitsounds.Length - 1);//get random number
        audiosource.PlayOneShot(hitsounds[rnd]);//play a random hit sound

        damage = Mathf.Max(damage, 0);//make sure damage is a positive number

        if (damage > health)//if damage is greater than current health
        {
            overKill = damage - health;//get the amount of overkill damage
            health = Mathf.Clamp(health - damage, 0f, health);//subtract damage from health, making sure not to subtract more than current health value
        }
        else//damage not more than current health
        {
            overKill = 0;//output 0
            health = Mathf.Clamp(health - damage, 0f, health);//subtract damage from health, making sure not to subtract more than current health value
        }
        SendMessage("OnDamage", SendMessageOptions.DontRequireReceiver);
        onDamage.Invoke();

        if (health == 0)//if health reaches 0
        {
            SendMessage("onDie", SendMessageOptions.DontRequireReceiver);//tell every object this is attched to to look for its onDie method -dont error if none
            onDie.Invoke();//call onDie
        }
    }

    //how to handle healing
    public void Heal(float heal)
    {
        heal = Mathf.Max(heal, 0);//make sure the number is positive

        if (heal > (maxHealth - health))//if the ammount healed would put the target over max health
        {
            overHeal = heal - (maxHealth - health);//get amount of overhealing
        }
        else//if healing does not result in over heal
        {
            overHeal = 0;//no overheal
        }
        health = Mathf.Clamp(health + heal, 0, maxHealth);//heal for an ammount not to exceed max health
        SendMessage("OnHeal", SendMessageOptions.DontRequireReceiver);//tell every object this is attched to to look for its onDie method no error if not found
        onHeal.Invoke();
    }

    public void Death()
    {
        audiosource.PlayOneShot(deathSound);//play death sound
        isDead = true;//let other things know this is dead.
        GameObject.Destroy(this.gameObject, ragTimer);//destroy game object after set seconds seconds
    }
}