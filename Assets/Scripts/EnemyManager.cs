using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    float levelTimerMax;
    //three lanes
    [SerializeField]
    Transform fawSpawnerL, fawSpawnerR, midSpawnerL, midSpawnerR, closeSpawnerL, closeSpawnerR;
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

    [SerializeField]
    float farTimer, midTimer, closeTimer;
    [SerializeField]
    Vector2 fartimerrange, midTimerRange, closeTimerRange;
    //timer is set to whatever the time should be
    public float speedMultiplier {
        get
        {
            float x = (Time.time > levelTimerMax / 2) ? 0 : (Time.time / levelTimerMax) * 0.25f;
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
        instance = this;
        
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

        levelTimerMax = Time.time + 40;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > farTimer)
        {
            //spawn an enemy at the far position
            if (useSpawnPatterns)
                spawnEnemy(farnemiesGroup[Random.Range(0, farnemiesGroup.Count)], loc.far);
            else
                spawnEnemy(farEnemies[Random.Range(0, farEnemies.Count)], loc.far);
            //reset enemy timer
            farTimer = Time.time + (Random.Range(fartimerrange.x, fartimerrange.y));

            Debug.Log("Timer: " + farTimer +  "  == " + ( farTimer - Time.time));

        }
    }


    //speed should be initialized differently if we get closer to the deadline
    //should go up to 25% faster. (timer/timermax * 0.25f) 
    private void spawnEnemy(GameObject enemy, loc location)
    {
        int x = Random.Range(0, 2);

        //basically speeds up the enemy in the last half of the round. 
        //the last 25% of the round the enemies gradually become faster

        //half of the time
        //time.time/maxtime


        //spawns the enemy at a 
        switch (location)
        {
            case loc.far:
                if (x == 0)
                {
                    GameObject g = Instantiate(enemy, fawSpawnerL);  //left side
                    Enemy e = g.GetComponent<Enemy>();
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
