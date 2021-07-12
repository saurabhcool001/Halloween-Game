using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterScript : MonoBehaviour
{
    public float rotateSpeed;
    public bool onX, onY, onZ;

    //public bool move;

    void Update()
    {
        if(onX)
        {
            transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        }

        if (onY)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }

        if (onZ)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }


        //if(move)
        //{
        //    int rand = Random.Range(1, 3);

        //    if(rand == 1)
        //    {
        //        transform.Translate(transform.position)
        //    }
        //}

    }
}
