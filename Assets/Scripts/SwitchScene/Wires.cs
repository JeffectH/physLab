using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wires : MonoBehaviour
{
    
    LineRenderer lineRen;
    // Start is called before the first frame update
    void Start()
    {
        lineRen = GetComponent<LineRenderer>();
        Debug.Log(lineRen);

        lineRen.positionCount = 0;

        Debug.Log(lineRen.positionCount);
    }

    public Camera Camera;
    GameObject last_on = null;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool ban = false;
            RaycastHit hit;
            Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(Camera.main.transform.position, fwd, out hit, 100.0f))
            {

                if (hit.collider.GetComponent<Rigidbody>() != null)
                {
                    if (hit.collider.GetComponent<Rigidbody>().transform.parent != null)
                    {
                        if (hit.collider.GetComponent<Rigidbody>().transform.parent.gameObject.name == "WiresPoint")
                        {
                            Vector3[] allNodes= new Vector3[lineRen.positionCount+1];
                            for (int i = 0; i < lineRen.positionCount; i++)
                            {
                                allNodes[i]=lineRen.GetPosition(i);
                                //Debug.Log(lineRen.GetPosition(i));
                            }
                            allNodes[lineRen.positionCount] = hit.collider.transform.position;
                            
                            for (int i = 0; i < lineRen.positionCount-1; i++)
                            {
                                if(allNodes[i] == allNodes[lineRen.positionCount])
                                {
                                    ban = true;
                                    break;
                                }

                                //Debug.Log(lineRen.GetPosition(i));
                            }
                            if (lineRen.positionCount >= 2)
                            {
                                if (allNodes[lineRen.positionCount - 1] == allNodes[lineRen.positionCount])
                                {
                                    Debug.Log("cancel");
                                    Vector3[] cancelNodes = new Vector3[lineRen.positionCount - 2];
                                    for (int i = 0; i < lineRen.positionCount - 2; i++)
                                    {
                                        cancelNodes[i] = lineRen.GetPosition(i);
                                        //Debug.Log(lineRen.GetPosition(i));
                                    }
                                    lineRen.positionCount -= 2;
                                    lineRen.SetPositions(cancelNodes);
                                }
                            }
                  
                            if (!ban)
                            {
                                lineRen.positionCount++;
                                lineRen.SetPositions(allNodes);
                            }
                            
                            //Debug.Log(lineRen.positionCount);
                            //Debug.Log(allNodes);

                        }



                    }


                }
            }
        }


    }
}

