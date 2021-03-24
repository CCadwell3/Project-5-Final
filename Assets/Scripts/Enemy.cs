using UnityEngine;

public class Enemy : Pawn
{
    //serialized so this can be accessed in editor
    [SerializeField, Tooltip("The max speed of the player")]
    private float speed = 6f;

    [SerializeField, Tooltip("How high char can jump")]
    private float jumpForce;

    public Health health;

    [SerializeField]
    private Drops drops;

    [SerializeField]
    private Weapons[] StartingWeapons;

    //vars for jumping
    private bool grounded;

    //movement
    private float moveRight;

    private float moveUp;

    [SerializeField]
    private Transform weaponContainer;

    public override void Awake()
    {
        rbpawn = GetComponent<Rigidbody>();//get rigidbody
        grounded = true;//set grounded to true (say we are on the ground)
        RagOff();
        int selection = Random.Range(0, (StartingWeapons.Length - 1));//generate random number up to weapons length
        EquipWeapon(StartingWeapons[selection]);//use random number to assign a weapon to enemy
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Move(Vector3 moveDirection)
    {
        //convert from "stick space" to worldspace so movement is based on player rotation
        moveDirection = transform.InverseTransformDirection(moveDirection);

        anim.SetFloat("Forward", moveDirection.z * speed);//set animation speed to input value * speed
        moveUp = anim.GetFloat("Forward");//set move up to match

        anim.SetFloat("Right", moveDirection.x * speed);//set animation speed to input value * speed
        moveRight = anim.GetFloat("Right");//set move right to match

        base.Move(moveDirection);//call move from parent
    }

    public override void Jump(Vector3 moveDirection)
    {
        if (grounded == true)//make sure we are on the ground
        {
            grounded = false;//set check flag to false (because we should be in the air)
            anim.SetTrigger("Jump");//tell the animation to play
            rbpawn.velocity = new Vector3(moveDirection.x, jumpForce, moveDirection.z);//add y axis jumpforce to current movement
        }
    }

    public override void EquipWeapon(Weapons weaponToEquip)
    {
        if (!equippedWeapon)
        {
            equippedWeapon = Instantiate(weaponToEquip) as Weapons;//spawn weapon
            equippedWeapon.transform.parent = weaponContainer;//set weapon container as weapons parent
            equippedWeapon.gameObject.layer = gameObject.layer;//assign weapon to parents layer
            equippedWeapon.transform.localPosition = weaponToEquip.transform.localPosition;//position weapon
            equippedWeapon.transform.localRotation = weaponToEquip.transform.localRotation;//rotate weapon
        }
        else
        {
            Destroy(equippedWeapon.gameObject);//get rid of old weapon
            equippedWeapon = Instantiate(weaponToEquip) as Weapons;//spawn weapon
            equippedWeapon.transform.parent = weaponContainer;//parent weapon to container (hold position)
            equippedWeapon.gameObject.layer = gameObject.layer;//assign weapon to parents layer
            equippedWeapon.transform.localPosition = weaponToEquip.transform.localPosition;//position weapon
            equippedWeapon.transform.localRotation = weaponToEquip.transform.localRotation;//rotate weapon
        }

        base.EquipWeapon(weaponToEquip);
    }

    //collision detection (right now to check if we are on the ground or not)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))//if the item collided with has the tag "ground"
        {
            anim.ResetTrigger("Jump");//tell animation to stop
            grounded = true;//set flag to true
        }
    }
}