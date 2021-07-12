using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterScript : MonoBehaviour
{
    public float rotateSpeed;
    public bool onX, onY, onZ;

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
    }
}
