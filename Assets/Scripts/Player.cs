using UnityEngine;

[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(Animator))]
public class Player : Pawn
{
    //serialized so this can be accessed in editor
    [SerializeField, Tooltip("The max speed of the player")]
    private float speed = 6f;

    [SerializeField, Tooltip("Character's turning speed")]
    private float rotateSpeed = 90;

    public Health health;

    //movement
    private float moveRight;

    private float moveUp;

    [SerializeField]
    private Transform weaponContainer;

    [SerializeField]
    private Transform spellContainer;

    public override void Awake()
    {
        base.Awake();
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

    public override void RotateTowards(Vector3 targetPoint)
    {
        if (health.isDead == false)//only do this if we are still alive
        {
            //create local var for rotation quaternion
            Quaternion targetRotation;

            //find rotation to point
            Vector3 vectorToTarget = targetPoint - transform.position; //endpoint - startpoint  where we are going to look, and where we are looking
            targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);//determine where to look
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);//combine the previous steps, and look based on frames
            base.RotateTowards(targetPoint);
        }
    }

    public override void EquipWeapon(Weapons weaponToEquip)
    {
        if (!equippedWeapon)
        {
            equippedWeapon = Instantiate(weaponToEquip) as Weapons;//spawn weapon
            equippedWeapon.transform.parent = weaponContainer;//set weapon container as weapons parent
            equippedWeapon.gameObject.layer = gameObject.layer;//assign weapon to parents layer                  -----new
            equippedWeapon.transform.localPosition = weaponToEquip.transform.localPosition;//position weapon
            equippedWeapon.transform.localRotation = weaponToEquip.transform.localRotation;//rotate weapon
        }
        else
        {
            Destroy(equippedWeapon.gameObject);//get rid of old weapon
            equippedWeapon = Instantiate(weaponToEquip) as Weapons;//spawn weapon
            equippedWeapon.transform.parent = weaponContainer;//parent weapon to container (hold position)
            equippedWeapon.gameObject.layer = gameObject.layer;//assign weapon to parents layer                  -----new
            equippedWeapon.transform.localPosition = weaponToEquip.transform.localPosition;//position weapon
            equippedWeapon.transform.localRotation = weaponToEquip.transform.localRotation;//rotate weapon
        }
        base.EquipWeapon(weaponToEquip);
    }
    public override void EquipSpell(Weapons spellToEquip)
    {
        if (!equippedSpell)
        {
            equippedSpell = Instantiate(spellToEquip) as Weapons;//spawn weapon
            equippedSpell.transform.parent = spellContainer;//set weapon container as weapons parent
            equippedSpell.gameObject.layer = gameObject.layer;//assign weapon to parents layer                  -----new
            equippedSpell.transform.localPosition = spellToEquip.transform.localPosition;//position weapon
            equippedSpell.transform.localRotation = spellToEquip.transform.localRotation;//rotate weapon
        }
        else
        {
            Destroy(equippedWeapon.gameObject);//get rid of old weapon
            equippedSpell = Instantiate(spellToEquip) as Weapons;//spawn weapon
            equippedSpell.transform.parent = weaponContainer;//parent weapon to container (hold position)
            equippedSpell.gameObject.layer = gameObject.layer;//assign weapon to parents layer                  -----new
            equippedSpell.transform.localPosition = spellToEquip.transform.localPosition;//position weapon
            equippedSpell.transform.localRotation = spellToEquip.transform.localRotation;//rotate weapon
        }
        base.EquipSpell(spellToEquip);
    }
}