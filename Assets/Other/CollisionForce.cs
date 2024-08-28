using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionForce : MonoBehaviour
{

    public float force;

    public bool col = false;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.GetComponent<BallTWo>()) 
        {

            if (!col) 
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(collision.transform.forward * force);
                GetComponent<Rigidbody>().AddForce(transform.forward * force);
                col = true;
            }

        }
    }
}
