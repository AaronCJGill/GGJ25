using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cork : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    
    void Update()
    {
        //moves up constantly
        transform.Translate(Vector2.up * speed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Kill();
        }
    }


}
