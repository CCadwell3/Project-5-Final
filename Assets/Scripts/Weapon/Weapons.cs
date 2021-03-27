using UnityEngine;
using UnityEngine.Events;

public abstract class Weapons : MonoBehaviour
{
    public bool isAttacking = false;//tracker for weather or not to inflict damage

    public Sprite icon = null;//holder for visuals

    [Header("Damage")]
    public float weaponDamage = 100;

    [Header("Projectile Setup")]
    public Ammo projectilePrefab;
    public Ammo thrown;

    [Header("IK Points")]
    public Transform rightHandPoint;
    public Transform leftHandPoint;

    [Header("Events")]
    public UnityEvent OnMainAttackDown;
    public UnityEvent OnMainAttackUp;
    public UnityEvent OnAltAttackDown;
    public UnityEvent OnAltAttackUp;
    public UnityEvent OnSpellDown;
    public UnityEvent OnSpellUp;

    [Header("Character")]
    public Pawn pawn;
    private GameObject owner;
    private Transform origin;
    [SerializeField]
    private ParticleSystem spellParticle;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (transform.root.GetComponent<Pawn>())
        {
            pawn = transform.root.GetComponent<Pawn>();
            owner = pawn.gameObject;
            origin = owner.transform.Find("FirePoint");
            if (pawn.equippedWeapon != null)
            {
                if (pawn.equippedWeapon.GetComponent<ParticleSystem>() != null)//if the pawn has a particlesystem
                {
                    spellParticle = pawn.equippedWeapon.GetComponent<ParticleSystem>();//assign component
                }
            }
           
        }   
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public virtual void MainAttackDown()
    {
        OnMainAttackDown.Invoke();
        isAttacking = true;
    }

    public virtual void MainAttackUp()
    {
        OnMainAttackUp.Invoke();
        isAttacking = false;
    }

    public virtual void AltAttackDown()
    {
        OnAltAttackDown.Invoke();
    }

    public virtual void AltAttackUp()
    {
        OnAltAttackUp.Invoke();
    }

    public virtual void SpellFireDown()
    {
        OnSpellDown.Invoke();
    }
    public virtual void SpellFireUp()
    {
        OnSpellUp.Invoke();
    }

    //Longsword
    public virtual void LongSwordAttackStart()
    {
        if (!pawn.anim.GetBool("LongSwordAttack"))//if not attacking
        {
            pawn.anim.SetBool("LongSwordAttack", true);//play attack animation
        }
    }

    public virtual void LongSwordAttackEnd()
    {
        if (pawn.anim.GetBool("LongSwordAttack"))//if attacking
        {
            pawn.anim.SetBool("LongSwordAttack", false);//stop attack animation
        }
        else
        {
            //nothing
        }
    }

    public virtual void LongSwordAltStart()
    {
        //block
        pawn.anim.SetBool("SwordBlock", true);//start block anim
    }

    public virtual void LongSwordAltEnd()
    {
        //return to normal
        if (pawn.anim.GetBool("SwordBlock"))//make sure blocking is happening
        {
            pawn.anim.SetBool("SwordBlock", false);//stop block animation
        }
    }

    //dagger
    public virtual void DaggerAttackStart()
    {
        if (!pawn.anim.GetBool("DaggerAttack"))//if not attacking
        {
            pawn.anim.SetBool("DaggerAttack", true);//play attack animation
        }
    }

    public virtual void DaggerAttackEnd()
    {
        if (pawn.anim.GetBool("DaggerAttack"))//if attacking
        {
            pawn.anim.SetBool("DaggerAttack", false);//stop attack animation
        }
    }

    public virtual void DaggerAltStart()
    {
        //nothing this attack happens on button up
    }

    public virtual void DaggerAltEnd()
    {
        //Throw
        Throw(origin);
    }

    //spear
    public virtual void SpearAttackStart()
    {
        if (!pawn.anim.GetBool("SpearAttack"))//if not attacking
        {
            pawn.anim.SetBool("SpearAttack", true);//play attack animation
        }
    }

    public virtual void SpearAttackEnd()
    {
        if (pawn.anim.GetBool("SpearAttack"))//if attacking
        {
            pawn.anim.SetBool("SpearAttack", false);//stop attack animation
        }
    }

    public virtual void SpearAltStart()
    {
        //throw
        Throw(origin);
    }

    public virtual void SpearAltEnd()
    {
        //nothing -- This fires on attack down
    }
    public virtual void FireBallSpell()
    {
        FireSpell(origin);
    }

    public void Throw(Transform origin)
    {
        
        Ammo thrown = Instantiate(projectilePrefab, origin.position, origin.rotation, origin) as Ammo;//create projectile object
        Ammo thrownScript = thrown.GetComponent<Ammo>();//get component from new projectile
        thrown.gameObject.layer = gameObject.layer;//assign thrown object to parent objecst layer
        thrownScript.from = origin;
    }


    public void FireSpell(Transform origin)
    {
        Ammo spell = Instantiate(projectilePrefab, origin.position, origin.rotation, origin) as Ammo;//create projectile object
        Ammo spellScript = spell.GetComponent<Ammo>();//get component from new projectile
        spell.gameObject.layer = gameObject.layer;//assign thrown object to parent objecst layer
        spellScript.from = origin;
        
    }







    //collision events
    public virtual void OnCollisionEnter(Collision collision)
    {
    }

    public virtual void OnTriggerEnter(Collider thingWeHit)//make sure to use collider and not collision.  Pass in weapon damage from weapon
    {
        if (GetComponentInParent<Health>().isDead == false)//make sure attacker is still alive.
        {
            if (isAttacking == true)
            {
                if (thingWeHit.transform.root.GetComponent<Pawn>())//if we run into a pawn object
                {
                    if (thingWeHit.GetComponent<Health>())//if target has a health component
                    {
                        Health health = thingWeHit.GetComponent<Health>();//reference for health component of object we are hitting
                        health.Damage(weaponDamage);//call objects damage function, give it the weapon damage of equipped weapon
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }
        }
    }
}