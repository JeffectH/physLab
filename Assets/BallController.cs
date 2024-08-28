using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController instance;

    public CalculateDataLabSix CalculateData;

    public int currenttime;

    private Rigidbody rb;
    private bool isSpeed = false;
    private float startTime;


    private void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
    }

    public void SpeedChange()
    {
        rb.isKinematic = true;
        isSpeed = true;
        startTime = Time.time;
    }

    private void Update()
    {
        float currentTime = Time.time - startTime;
        if (isSpeed)
        {
            if (currentTime >= CalculateData.currentListTime[currenttime])
            {
                Debug.Log(currentTime);
                isSpeed = false;
            }
        }

        if (isSpeed)
            transform.Translate(-Vector3.up * CalculateData.calculateVelocity[currenttime] * Time.deltaTime, Space.World);

    }
}
