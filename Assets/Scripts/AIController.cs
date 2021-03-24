using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(GameObject))]
[RequireComponent(typeof(Animator))]
public class AIController : Controller
{
    [SerializeField]
    private float meleeDistance = 3f;

    [SerializeField]
    private float throwDistance = 7f;

    private GameObject target;
    private GameManager gameManager;

    [SerializeField, Tooltip("Delay time for enemy shots")]
    private float fireDelay = 1;

    [SerializeField, Tooltip("Enemy can shoot again in...")]
    private float fireTime;

    // Start is called before the first frame update
    public override void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        base.Start();//run start from controller
    }

    // Update is called once per frame
    public override void Update()
    {
        if (gameManager.paused)//if game manager is paused
        {
            return;//exit loop
        }

        if (pawn.nav.enabled)
        {
            if (gameManager.player == true)//if there is a player object
            {
                if (gameManager.player.health.isDead == false)//make sure the player is alive
                {
                    gameManager.enemy.nav.isStopped = false;//start navigating

                    target = gameManager.player.gameObject;//set target to player object

                    gameManager.enemy.nav.SetDestination(target.transform.position);//tell the nav agent where to go

                    Vector3 input = gameManager.enemy.nav.desiredVelocity;//get velocity from nav agent

                    pawn.Move(input);//pass info to pawn object

                    //attacking
                    float distance = Vector3.Distance(transform.position, target.transform.position);//get distance to player



                    if (distance < meleeDistance)
                    {
                        //Main attack start
                        pawn.equippedWeapon.MainAttackDown();
                    }
                    else if (distance > meleeDistance && distance < throwDistance)
                    {
                        //alt attack start
                        if (Time.time > fireTime)//check time before next fire
                        {
                            pawn.equippedWeapon.AltAttackDown();
                            pawn.equippedWeapon.AltAttackUp();
                            fireTime = Time.time + fireDelay;//add 1 second
                        }
                    }
                    else//too far from player
                    {
                        pawn.equippedWeapon.MainAttackUp();//stop attacking
                    }

                }
            }
            else if (gameManager.player == false || gameManager.player.health.isDead == true)//no player found, or dead player
            {
                gameManager.enemy.nav.isStopped = true;//stop navigating
                gameManager.enemy.anim.SetFloat("Forward", 0);//stop moving
                gameManager.enemy.anim.SetFloat("Right", 0);//stop moving
                pawn.equippedWeapon.MainAttackUp();//stop attacking
            }

        }
        base.Update();//run update from controller
    }

    private void OnAnimatorMove()//runs after animator decides how to change
    {
        gameManager.enemy.nav.velocity = gameManager.enemy.anim.velocity;//set the nav agent speed to the same as the animator speed so object moves at root motion animate speed
    }
}