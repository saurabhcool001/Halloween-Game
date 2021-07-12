using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float sideDragSensitivity;
    public float minVelocityThreshold;
    Vector3 ballRot;
    Rigidbody ballBody;

    Vector3 refVelocity;
    Vector3 forceDir = Vector3.zero;
    void Start()
    {
        ballBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
  
        if (Input.GetMouseButton(0) && Player.Instance.playerState == State.Throw)
        {
            if (Input.GetAxis("Mouse X") != 0)
            {
                sideDragSensitivity = ballBody.velocity.z * 2 *  Time.deltaTime; 
                ballRot.x = (Input.GetAxis("Mouse X") - Input.GetAxis("Mouse Y")) * sideDragSensitivity;


                    ballBody.velocity += ballRot;

            }
        }
       
    }


    [EasyButtons.Button]
    void Throw()
    {
        this.GetComponent<Rigidbody>().AddForce(Player.Instance.transform.forward * 10, ForceMode.VelocityChange);
    }


    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy"))
        {
            CameraFollow.Instance.CamShake();
            Player.Instance.hitFX.transform.position = other.transform.position;
            Player.Instance.hitFX.Play();
            Player.Instance.enemyBloodFX.transform.position = other.transform.position;
            Player.Instance.enemyBloodFX.Play();
            other.GetComponent<Animator>().SetBool("isDead_" + Random.Range(1, 4), true);
            EnemyManager.Instance.Dead(other.gameObject);

            ballBody.AddRelativeForce(transform.forward * 5, ForceMode.VelocityChange);
        }
    }

}
