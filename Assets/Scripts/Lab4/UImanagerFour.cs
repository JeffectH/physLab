
using UnityEngine;
using TMPro;
using System;

public class UImanagerFour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stopWatch;
    [SerializeField] private TextMeshProUGUI _length;

    private float value;
    private void OnEnable()
    {
        CollisionBallLabFour.UpdateLegth += UpdateLength;
    }
    private void OnDisable()
    {
        CollisionBallLabFour.UpdateLegth -= UpdateLength;

    }

    private void UpdateLength()
    {
        if (CollisionBallLabFour.Hit)
        {
            switch (SwitchInstalation._index)
            {
                case 0:
                    value = UnityEngine.Random.Range(14.5f, 20f);
                    break;
                case 1:
                    value = UnityEngine.Random.Range(14.5f, 20f);
                    break;
                case 2:
                    value = UnityEngine.Random.Range(14f, 17f);

                    break;
                case 3:
                    value = UnityEngine.Random.Range(13f, 16f);
                    break;
            }

            _length.text = Math.Round(value,2).ToString() + " см";
        }
    }
}
