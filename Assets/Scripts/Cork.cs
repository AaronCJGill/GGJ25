using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cork : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    private void Start()
    {
        Debug.Log("Cork Object Spawned");
    }
    void Update()
    {
        //moves up constantly
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (transform.position.y > 50)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
        }
    }


}
