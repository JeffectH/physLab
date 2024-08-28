using UnityEngine;




public class TestDrag : MonoBehaviour
{
    private Vector3 pointScreen;
    public Camera Camera;
    public bool State;


    private void Start()
    {
        State = false;

    }

    private void Update()
    {
        if (Interactable.Instance.is2DRay)
        {
             if (State)
                    {
            
                        pointScreen = Camera.WorldToScreenPoint(gameObject.transform.position);
                        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointScreen.z);
                        Vector3 curPosition = Camera.ScreenToWorldPoint(curScreenPoint);
                        transform.position = curPosition;
                    }
            
                    if (Input.GetMouseButtonDown(0))
                    {
                        State = true;
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        State = false;
                    }
        }

       
    }

}
