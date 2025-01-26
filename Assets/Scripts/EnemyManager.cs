using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    //three lanes
    [SerializeField]
    Transform fawSpawnerL, fawSpawnerR, midSpawnerL, midSpawnerR, closeSpawnerL, closeSpawnerR;
    [Header("Prefabs")]
    [SerializeField]
    List<GameObject> farEnemies = new List<GameObject>();
    [SerializeField]
    List<GameObject> midEnemies = new List<GameObject>();
    [SerializeField]
    List<GameObject> closeEnemies = new List<GameObject>();
    [SerializeField]
    bool useSpawnPatterns = false;

    [SerializeField]
    List<GameObject> farnemiesGroup = new List<GameObject>();
    [SerializeField]
    List<GameObject> midEnemiesGroup = new List<GameObject>();
    [SerializeField]
    List<GameObject> closeEnemiesGroup = new List<GameObject>();

    float farTimer, midTimer, closeTimer;

    [SerializeField]
    [Tooltip("The X is going to be the minimun time it takes to spawn, the Y will be the maximum time to spawn. DO NOT LET X BE BIGGER THAN Y")]
    [Header("Spawn Timers")]
    Vector2 fartimerrange, midTimerRange, closeTimerRange;
    //timer is set to whatever the time should be
    public float speedMultiplier {
        get
        {
            //basically speeds up the enemy in the last half of the round. 
            //the last 25% of the round the enemies gradually become faster
            //we multiply this number with the speed and add it back to the unmodified speed: speed += speed * x
            //accessed in the 
            float x = (Time.time > BottleBehavior.instance.levelTimerMax / 2) ? 0 : (Time.time / BottleBehavior.instance.levelTimerMax) * 0.25f;
            return x;
        }
    }

    public static EnemyManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null && instance != this)
        {
            instance = this;
        }
    }

    enum loc
    {
        far,
        mid,
        close
    }

    private void Start()
    {
        farTimer = Time.time + (Random.Range(fartimerrange.x,fartimerrange.y));
        midTimer = Time.time + (Random.Range( midTimerRange.x, midTimerRange.y));
        closeTimer = Time.time + (Random.Range(closeTimerRange.x, closeTimerRange.y));

        //BottleBehavior.instance.levelTimerMax = Time.time + 40;
    }

    // Update is called once per frame
    void Update()
    {
        //checks each of the timers


        if (Time.time > farTimer)
        {
            //spawn an enemy at the far position
            if (useSpawnPatterns)
                spawnEnemy(farnemiesGroup[Random.Range(0, farnemiesGroup.Count)], loc.far);
            else
                spawnEnemy(farEnemies[Random.Range(0, farEnemies.Count)], loc.far);
            //reset enemy timer
            farTimer = Time.time + (Random.Range(fartimerrange.x, fartimerrange.y));

            //Debug.Log("Timer: " + farTimer +  "  == " + ( farTimer - Time.time));

        }

        if (Time.time > midTimer)
        {
            //spawn an enemy at the mid position
            if (useSpawnPatterns)
                spawnEnemy(midEnemiesGroup[Random.Range(0, farnemiesGroup.Count)], loc.mid);
            else
                spawnEnemy(midEnemies[Random.Range(0, midEnemies.Count)], loc.mid);
            //reset enemy timer
            midTimer = Time.time + (Random.Range(midTimerRange.x, midTimerRange.y));

        }

        if (Time.time > closeTimer)
        {
            //spawn an enemy at the close position
            if (useSpawnPatterns)
                spawnEnemy(closeEnemiesGroup[Random.Range(0, closeEnemiesGroup.Count)], loc.close);
            else
                spawnEnemy(closeEnemies[Random.Range(0, closeEnemies.Count)], loc.close);
            //reset enemy timer
            closeTimer = Time.time + (Random.Range(closeTimerRange.x, closeTimerRange.y));

        }
    }


    //speed should be initialized differently if we get closer to the deadline
    //should go up to 25% faster. (timer/timermax * 0.25f) 
    private void spawnEnemy(GameObject enemy, loc location)
    {
        int x = Random.Range(0, 2);//either 0 or 1


        //spawns the enemy at a 
        switch (location)
        {
            case loc.far:
                if (x == 0)
                {
                    GameObject g = Instantiate(enemy, fawSpawnerL);  //left side
                    //Enemy e = g.GetComponent<Enemy>();
                    //e.initialize(e.speed);
                }
                else
                    Instantiate(enemy, fawSpawnerR);  //right side
                break;
            case loc.mid:
                if (x == 0)
                    Instantiate(enemy, midSpawnerL);  //left side
                else
                    Instantiate(enemy, midSpawnerR);  //right side
                break;
            case loc.close:
                if (x == 0)
                    Instantiate(enemy, closeSpawnerL);  //left side
                else
                    Instantiate(enemy, closeSpawnerR);  //right side
                break;
            default:
                break;
        }

    }




}
