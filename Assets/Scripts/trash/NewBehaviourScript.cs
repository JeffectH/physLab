using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    private float test2;
    public void test(float emitter) 
    {
        test2 = Mathf.Lerp(0.2f, 0.4f, emitter);

        ParticleSystem.startLifetime = test2;
    }
}
