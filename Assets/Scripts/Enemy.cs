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

    float killspeedX, killspeedY;
    [SerializeField]
    AudioSource _as;

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
            transform.Translate(-1 * speed * Vector2.right * Time.deltaTime);

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
        BottleBehavior.instance.addPoints(points);

        killspeedY = Random.Range(20, 40);
        killspeedX = Random.Range(-20, 20);

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
