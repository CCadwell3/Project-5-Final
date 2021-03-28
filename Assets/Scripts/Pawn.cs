using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Pawn : MonoBehaviour
{
    //create  objects
    public Animator anim;
    public Weapons equippedWeapon;
    public Rigidbody rbpawn;
    public NavMeshAgent nav;
    public Weapons equippedSpell;
    public ParticleSystem hitParticle;
    public AudioClip hitSound;

    public virtual void Awake()
    {
        RagOff();//stop ragdoll
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();//get the animator
        rbpawn = GetComponent<Rigidbody>();//get rigidbody component
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public virtual void Move(Vector3 moveDirection)
    {
    }

    public virtual void RotateTowards(Vector3 targetPoint)
    {
    }

    public virtual void Jump(Vector3 moveDirection)
    {
    }

    public virtual void EquipWeapon(Weapons weaponToEquip)
    {
    }
    public virtual void EquipSpell(Weapons spellToEquip)
    {
    }

    public void OnAnimatorIK(int layerIndex)
    {
        if (!equippedWeapon)
        {
            return;//no weapon, do nothing
        }
        if (equippedWeapon.rightHandPoint)//weapon has a right hand point
        {
            //set 2 handed weapon
            anim.SetIKPosition(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, equippedWeapon.rightHandPoint.rotation);

            //set weights
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        }
        if (equippedWeapon.leftHandPoint)//weapon has a left hand point
        {
            //position and rotation
            anim.SetIKPosition(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, equippedWeapon.leftHandPoint.rotation);
            //weight
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        }
    }

    public void RagOn()
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();//get all the rigidbodies in the objects children, store in an array
        Collider[] cols = GetComponentsInChildren<Collider>();//get all the colliders from the objects children, store in array
        Rigidbody mainBody = GetComponent<Rigidbody>();//get main rigidbody
        Collider mainCol = GetComponent<Collider>();// get main collider

        if (nav)//if there is a nav mesh agent
        {
            nav.enabled = false;//shut down navigation
        }

        anim.enabled = false;//disable animations

        //turn on all the ragdoll colliders
        foreach (Collider collider in cols)
        {
            collider.enabled = true;
        }
        //enable physics
        foreach (Rigidbody rigidbody in rbs)
        {
            rigidbody.isKinematic = false;
        }

        mainBody.isKinematic = true;//turn off physics for main rigidbody
        mainCol.enabled = false;//turn off main collider
    }

    public void RagOff()
    {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();//get all the rigidbodies in the objects children, store in an array
        Collider[] cols = GetComponentsInChildren<Collider>();//get all the colliders from the objects children, store in array
        Rigidbody mainBody = GetComponent<Rigidbody>();//get main rigidbody
        Collider mainCol = GetComponent<Collider>();// get main collider

        if (GetComponent<NavMeshAgent>())//if there is a nav mesh agent
        {
            NavMeshAgent nav = GetComponent<NavMeshAgent>();//get nav agent
            nav.enabled = true;//enable navigation
        }

        //turn off all the ragdoll colliders
        foreach (Collider collider in cols)
        {
            collider.enabled = false;
        }
        //disable physics
        foreach (Rigidbody rigidbody in rbs)
        {
            rigidbody.isKinematic = true;
        }

        mainBody.isKinematic = false;//turn on physics for main rigidbody
        mainCol.enabled = true;//turn on main collider
        anim.enabled = true;//enable animations
    }
}