
using UnityEngine;

public class Tools : MonoBehaviour, IControllablle
{
    
    public virtual void Move()
    {
        
    }

    public void OutlineOffOn()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
