using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInstalation : MonoBehaviour
{
    public List<GameObject> Instalation = new List<GameObject>();

    public GameObject FantomBallOne;
    public GameObject FantomBallTwo;

    public List<Transform> NewBallPosition = new List<Transform>();
    public List<Transform> Point = new List<Transform>();

    public static Transform PointInstalation;
    public static int _index;

    private void Start()
    {
        PointInstalation = Point[1];
        _index = 1;
    }

    public void Switch(int index)
    {
        for (int i = 0; i < Instalation.Count; i++)
        {
            if (i == index)
            {
                Instalation[i].SetActive(true);

                _index = i;

                switch (i)
                {
                    case 0:
                        FantomBallOne.transform.position = NewBallPosition[0].position;
                        FantomBallTwo.transform.position = NewBallPosition[1].position;
                        PointInstalation = Point[0];
                        LabFourInstalation.Instance.ResetBallPosition();
                        break;
                    case 1:
                        FantomBallOne.transform.position = NewBallPosition[2].position;
                        FantomBallTwo.transform.position = NewBallPosition[3].position;
                        PointInstalation = Point[1];
                        LabFourInstalation.Instance.ResetBallPosition();
                        break;
                    case 2:
                        FantomBallOne.transform.position = NewBallPosition[4].position;
                        FantomBallTwo.transform.position = NewBallPosition[5].position;
                        PointInstalation = Point[2];
                        LabFourInstalation.Instance.ResetBallPosition();
                        break;
                    case 3:
                        FantomBallOne.transform.position = NewBallPosition[6].position;
                        FantomBallTwo.transform.position = NewBallPosition[7].position;
                        PointInstalation = Point[3];
                        LabFourInstalation.Instance.ResetBallPosition();
                        break;
                }
            }
            else
            {
                Instalation[i].SetActive(false);
            }
        }
    }
}