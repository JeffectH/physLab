using System.Collections.Generic;
using UnityEngine;

public class CalculateDataLabSix : MonoBehaviour
{
    public DataLabSix Data;

    [SerializeField]
    public List<float> currentListTime = new List<float>();
    public List<float> calculateVelocity = new List<float>();

    public Transform PointOne;
    public Transform PointTwo;

    public float distanceToTravel;

    private void Start()
    {
        distanceToTravel = Vector3.Distance(PointOne.position, PointTwo.position);

        int RandomValue = Random.Range(0, 2);

        if (RandomValue == 0)
        {
            ChangeTemperature(0);
        }
        else if (RandomValue == 1)
        {
            ChangeTemperature(3);
        }
    }

    // ќбщий метод дл€ расчета начальной скорости
    private void CalculateInitialVelocity(List<float> timeList)
    {
        calculateVelocity.Clear();

        for (int i = 0; i < timeList.Count; i++)
        {
            float velocity = distanceToTravel / timeList[i];
            calculateVelocity.Add(velocity);
        }
    }

    public void ChangeTemperature(int temperatureIndex)
    {
        currentListTime.Clear();

        // ќпределим словарь, который св€жет индексы времени с соответствующими списками данных
        Dictionary<int, List<float>> dataLists = new Dictionary<int, List<float>>
    {
        { 0, Data.CastorTemperature30 },
        { 1, Data.CastorTemperature50 },
        { 2, Data.CastorTemperature70 },
        { 3, Data.GlycerinTemperature20 },
        { 4, Data.GlycerinTemperature40 },
        { 5, Data.GlycerinTemperature60 },
        { 6, Data.GlycerinTemperature80 }
    };

        // ѕроверим, что индекс температуры действителен
        if (!dataLists.ContainsKey(temperatureIndex))
        {
            Debug.LogError("Ќедопустимый индекс температуры!");
            return;
        }

        foreach (var item in dataLists[temperatureIndex])
        {

            currentListTime.Add(item);
        }

        CalculateInitialVelocity(currentListTime);
    }

}
