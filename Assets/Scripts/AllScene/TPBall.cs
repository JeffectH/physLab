using UnityEngine;

public class TPBall : MonoBehaviour
{
    public Transform point;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObjects>())
        {
            if (other.GetComponent<InteractableObjects>().gameObject.Equals(InstalationThree.instance._activeCargo))
                return;


            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = point.position;

        }
    }
}
