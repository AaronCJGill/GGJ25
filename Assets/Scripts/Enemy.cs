using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int points;
    public bool moveRight;

    [SerializeField]
    GameObject particle;

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
        Move();
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
        ParticleSystem ps = particle.GetComponent<ParticleSystem>();
        //make this take on the sprite of this enemy
        //ps.material to some shit
            //change the material of this

    }
}
