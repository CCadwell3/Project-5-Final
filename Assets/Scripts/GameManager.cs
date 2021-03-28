using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private Object LongSwordPre;
    private Object SpearPre;
    private Object DaggerPre;
    private Object HarmPre;
    private Object HealPre;
    private Object EnemyPre;
    private Object PlayerPre;

    [Header("SpawnPoints")]
    public Transform[] spearSpawn;

    public Transform[] longSwordSpawn;
    public Transform[] daggerSpawn;
    public Transform[] harmSpawn;
    public Transform[] healSpawn;
    public Transform[] enemySpawn;
    public Transform playerSpawn;

    [Header("Spawn Timing")]
    public float playerSpawnDelay = 5;

    [SerializeField]
    private float nextPlayerSpawn;

    public float enemySpawnDelay = 5;

    [SerializeField]
    private float nextEnemySpawn;

    public float weaponSpawnDelay = 5;

    [SerializeField]
    private float nextWeaponSpawn;

    public float buffSpawnDelay = 5;

    [SerializeField]
    private float nextBuffSpawn;

    public float debuffSpawnDelay = 5;

    [SerializeField]
    private float nextDebuffSpawn;

    [Header("Spawn max numbers")]
    public float maxSpears = 1;

    public float maxSwords = 1;
    public float maxDaggers = 1;
    public float maxEnemies = 1;
    public float maxBuffs = 1;
    public float maxDebuffs = 1;

    [Header("Player Properties")]
    public int lives = 4;

    //arrays to hold object counts
    private GameObject[] spears;

    private GameObject[] longSwords;
    private GameObject[] daggers;
    private GameObject[] harms;
    private GameObject[] heals;
    private GameObject[] enemies;
    private GameObject[] players;

    private int rnd;
    public static GameManager instance { get; private set; }
    public bool paused = false;//tracking wether or not game is paused.
    public Player player;
    public Enemy enemy;

    [Header("Events")]
    public UnityEvent onPause;
    public UnityEvent onResume;
    public UnityEvent onGameOverResume;
    public UnityEvent onGameOver;

    //Singleton  only one instance
    private void Awake()
    {
        //make sure there is always only 1 instance
        if (GameManager.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    private void Start()
    {
        //load prefabs into objects
        LongSwordPre = Resources.Load("Prefabs/PULongsword");
        DaggerPre = Resources.Load("Prefabs/PUDagger");
        SpearPre = Resources.Load("Prefabs/PUSpear");
        HealPre = Resources.Load("Prefabs/HealPre");
        HarmPre = Resources.Load("Prefabs/HarmPre");
        EnemyPre = Resources.Load("Prefabs/Vampire");
        PlayerPre = Resources.Load("Prefabs/Player");
    }

    // Update is called once per frame
    private void Update()
    {
        player = FindObjectOfType<Player>();//store player in game manager
        enemy = FindObjectOfType<Enemy>();//store enemies in gm
    }

    private void FixedUpdate()
    {
        //get a count of objects
        spears = GameObject.FindGameObjectsWithTag("Spear");
        daggers = GameObject.FindGameObjectsWithTag("Dagger");
        longSwords = GameObject.FindGameObjectsWithTag("LongSword");
        harms = GameObject.FindGameObjectsWithTag("Harm");
        heals = GameObject.FindGameObjectsWithTag("Heal");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        players = GameObject.FindGameObjectsWithTag("Player");

        //spawn the player
        if (player == null && lives > 0)//if there is not a player and we still have lives left
        {
            GameObject Player = (GameObject)Instantiate(PlayerPre, playerSpawn.position, playerSpawn.rotation);//spawn player
            player = FindObjectOfType<Player>();//reference player object
            lives--;//subtract a life
        }
        else if (player == null && lives == 0)//no player, and no lives left
        {
            //game over
            GameOver();
        }
        //weapons
        if (Time.time > nextWeaponSpawn)
        {
            //Spear
            if (spears.Length < maxSpears)
            {
                rnd = Random.Range(0, spearSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new spear at random spear spawn point
                GameObject Spear = (GameObject)Instantiate(SpearPre, spearSpawn[rnd].position, spearSpawn[rnd].rotation);
            }
            //Dagger
            if (daggers.Length < maxDaggers)
            {
                rnd = Random.Range(0, daggerSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new dagger at random dagger spawn point
                GameObject Dagger = (GameObject)Instantiate(DaggerPre, daggerSpawn[rnd].position, daggerSpawn[rnd].rotation);
            }
            //Sword
            if (longSwords.Length < maxSwords)
            {
                rnd = Random.Range(0, longSwordSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new sword at random sword spawn point
                GameObject LongSword = (GameObject)Instantiate(LongSwordPre, longSwordSpawn[rnd].position, longSwordSpawn[rnd].rotation);
            }

            nextWeaponSpawn = Time.time + weaponSpawnDelay;//reset timer for next spawn
        }

        //debuffs
        if (Time.time > nextDebuffSpawn)
        {
            if (harms.Length < maxDebuffs)
            {
                rnd = Random.Range(0, harmSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new harm pickup at the harm spawn point
                GameObject Harm = (GameObject)Instantiate(HarmPre, harmSpawn[rnd].position, harmSpawn[rnd].rotation);
            }
            nextDebuffSpawn = Time.time + debuffSpawnDelay;//reset timer for next spawn
        }
        //buffs
        if (Time.time > nextBuffSpawn)
        {
            if (heals.Length < maxBuffs)
            {
                rnd = Random.Range(0, healSpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                //create a new heal pickup at random heal spawn point
                GameObject Heal = (GameObject)Instantiate(HealPre, healSpawn[rnd].position, healSpawn[rnd].rotation);
            }
            nextBuffSpawn = Time.time + buffSpawnDelay;//reset timer for next spawn
        }
        //enemies
        if (Time.time > nextEnemySpawn)
        {
            if (enemies.Length < maxEnemies)
            {
                rnd = Random.Range(0, enemySpawn.Length - 1);//Generate random number based on how many spawn points have been assigned
                GameObject Enemy = (GameObject)Instantiate(EnemyPre, enemySpawn[rnd].position, enemySpawn[rnd].rotation);//spawn enemy at enemy spawn point
            }
            nextEnemySpawn = Time.time + enemySpawnDelay;//reset timer for next spawn
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;//stop time
        paused = true; //set paused to true
        instance.onPause.Invoke();//call onpause event
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;//resume normal time
        paused = false;//paused bool off
        instance.onResume.Invoke();//resume event
    }

    public void QuitGame()
    {
        Application.Quit();//quit game
        //#if is preprossor code, if unity editor is running, this statement is true
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//stop editor
#endif
    }

    public void GameOver()
    {
        Time.timeScale = 0f;//stop time
        paused = true; //set paused to true
        instance.onGameOver.Invoke();//call gameover
    }

    public void GameOverResume()
    {
        Time.timeScale = 1.0f;//resume normal time
        paused = false;//paused bool off
        lives = 4;//reset lives
        instance.onGameOverResume.Invoke();//resume event
        //clear enemies
        Enemy[] e = FindObjectsOfType<Enemy>();//get a list of all the enemys
        for (int i = 0; i < e.Length; i++)
        {
            e[i].health.Damage(10000);//damage enemy with 10000 points
            i++;//increment counter
        }
    }
}