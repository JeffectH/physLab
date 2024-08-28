
using UnityEngine;

[RequireComponent (typeof(IControllablle))]
public class InputController : MonoBehaviour
{

    private IControllablle controllablleObject;

    private void Start()
    {
        controllablleObject = GetComponent<IControllablle>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            controllablleObject.Move();
        }
    }
}
