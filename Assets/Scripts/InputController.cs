using UnityEngine;

public class InputController : Controller
{
    //store camera following player
    private Camera Cam;

    // Start is called before the first frame update
    public override void Start()
    {
        Cam = Camera.main;//get main camera
        pawn = GetComponent<Pawn>();
        base.Start();//run start from controller
    }

    // Update is called once per frame
    public override void Update()
    {
        //send movement speed to pawn (x and z)
        pawn.Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));

        //call rotate towards mouse
        pawn.RotateTowards(GetMousePosition());

        
        if (Input.GetButtonDown("Primary Attack"))
        {
            if (pawn.equippedWeapon)//make sure there is a weapon equipped
            {
                //Main attack start
                pawn.equippedWeapon.MainAttackDown();
            }
        }
        if (Input.GetButtonUp("Primary Attack"))
        {
            if (pawn.equippedWeapon)//make sure there is a weapon equipped
            {
                //Main attack end
                pawn.equippedWeapon.MainAttackUp();
            }
        }
        if (Input.GetButtonDown("Alt Attack"))
        {
            if (pawn.equippedWeapon)//if there is a weapon
            {
                //Secondary attack start
                pawn.equippedWeapon.AltAttackDown();
            }
        }
        if (Input.GetButtonUp("Alt Attack"))
        {
            if (pawn.equippedWeapon)//if there is a weapon
            {
                //Secondary end
                pawn.equippedWeapon.AltAttackUp();
            }
        }
        
        if (Input.GetButtonDown("Menu") && GameManager.instance.paused == false)
        {
            GameManager.instance.Pause();
        }
        else if (Input.GetButtonDown("Menu") && GameManager.instance.paused == true)
        {
            GameManager.instance.Unpause();
        }
        if (Input.GetButtonDown("Spell"))
        {
            pawn.equippedSpell.SpellFireDown();
        }
        if (Input.GetButtonUp("Spell"))
        {
            pawn.equippedSpell.SpellFireUp();
        }
        base.Update();//run update from controller
    }

    private Vector3 GetMousePosition()
    {
        //create vector
        Vector3 mousePointInTheWorld = new Vector3();

        //set plane to up in worldspace and players position
        Plane charPlane = new Plane(Vector3.up, pawn.transform.position);

        //create ray
        Ray viewFinder;

        //look through player camera to mouse point, assign to viewfinder
        viewFinder = Cam.ScreenPointToRay(Input.mousePosition);

        //find the point where the 2 rays cross
        float crossDistance;

        if (charPlane.Raycast(viewFinder, out crossDistance))//if the 2 rays intersect output distance
        {
            mousePointInTheWorld = viewFinder.GetPoint(crossDistance);// use rays to get distance
        }
        //send ray cross point out
        return mousePointInTheWorld;
    }
}