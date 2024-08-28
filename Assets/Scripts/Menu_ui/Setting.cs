using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public float sound_level;
    public float music_level;
    public GameObject sound_level_text;
    public GameObject music_level_text;
    // Start is called before the first frame update
    void Start()
    {
        sound_level_text.GetComponent<TextMeshProUGUI>().text = sound_level.ToString();
        music_level_text.GetComponent<TextMeshProUGUI>().text = music_level.ToString();
    }

    public void set_sound_level(float index)
    {
        sound_level = index;
        sound_level_text.GetComponent<TextMeshProUGUI>().text = sound_level.ToString();

    }
    public void set_music_level(float index)
    {
        music_level = index;
        music_level_text.GetComponent<TextMeshProUGUI>().text = music_level.ToString();

    }
}
