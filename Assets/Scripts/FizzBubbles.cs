using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizzBubbles : MonoBehaviour
{
    [SerializeField]
    float lifetime = 3;
    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //activate their thingy
            collision.transform.GetComponent<Enemy>().Kill();
        }
    }
}
