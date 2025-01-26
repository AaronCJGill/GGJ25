using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int points;
    public bool moveRight;
    public bool isKilled = false;


    [SerializeField]
    GameObject particle;

    //kill speed editable values
    //x values
    [SerializeField] float killSpeedXMin = -20;
    [SerializeField] float killSpeedXMax = 20;
    
    //y values
    [SerializeField] float killSpeedYMin = 20;
    [SerializeField] float killSpeedYMax = 40;

    
    float killspeedX, killspeedY;
    [SerializeField]
    AudioSource _as;

    //animation variables
    [SerializeField] GameObject kidAnimatorController;
    
    //enemies can get up to 25% faster as time goes on
    public void initialize(float speed, bool moveRight)
    {
        this.moveRight = moveRight;
        this.speed = speed;
    }

    public void selfInit()
    {
        speed += (speed * EnemyManager.instance.speedMultiplier);
        moveRight = (transform.position.x > 0) ? false : true;
    }
    private void Awake()
    {
        selfInit();
    }
    private void Update()
    {
        if (!isKilled)
            Move();
        else
            killBehavior();
    }
    private void Move()
    {
        if (moveRight)
            transform.Translate(speed * Vector2.right * Time.deltaTime);
        else
        {
            transform.Translate(-1 * speed * Vector2.right * Time.deltaTime);
            kidAnimatorController.GetComponent<SpriteRenderer>().flipX = true;
        }

        if(transform.position.x <= -20 || transform.position.x >= 20)
        {
            Destroy(gameObject);
        }
    }


    public void Kill()
    {
        Debug.Log("Kill");
        isKilled = true;
        //make a sound
        //communicate that this enemy has died
        //play animation
        kidAnimatorController.GetComponent<Animator>().SetTrigger("hit");
        BottleBehavior.instance.addPoints(points);
        

        killspeedY = Random.Range(killSpeedYMin, killSpeedYMax);
        killspeedX = Random.Range(killSpeedXMin, killSpeedXMax);

        //TODO:
        //ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        //make this take on the sprite of this enemy
        //ps.material to some shit
        //change the material of this

    }

    public void killBehavior()
    {

        //move in random direction
        transform.Translate(new Vector2(killspeedX * Time.deltaTime, killspeedY * Time.deltaTime));

    }
}
