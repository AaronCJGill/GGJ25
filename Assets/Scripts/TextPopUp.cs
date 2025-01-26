using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopUp : MonoBehaviour
{
    [SerializeField]
    TMP_Text textpop;
    [SerializeField]
    Animator animator;



    public void initThis(int Points)
    {
        textpop.text = "+"+Points;

        int r = Random.Range(0, 4);
        switch (r)
        {
            case 1:
                animator.SetTrigger("Trigger1");

                break;
            case 2:
                animator.SetTrigger("Trigger2");
                break;
            case 3:
            default:
                animator.SetTrigger("Trigger3");

                break;
        }
        //3 different animations 
    }

    public void deleteSelf()
    {
        Destroy(gameObject);
    }
}
