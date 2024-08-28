using UnityEngine;

public class StoksTrigger : MonoBehaviour
{
    private GameObject _ballActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObjects>()) 
        {
            BallController.instance.SpeedChange();
        }
    }

    private void BallResistance(GameObject BallActive) 
    {
        //BallActive.GetComponent<Rigidbody>().drag = 20;
    }
}
