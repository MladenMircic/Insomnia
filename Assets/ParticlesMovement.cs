using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesMovement : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private float h, v;
    void Start()
    {
        h = 0.0f;
        v = 0.0f;
    }

    
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        this.transform.Translate(h * 5.0f * Time.deltaTime, v * 5.0f * Time.deltaTime, 0.0f);

        if (v != 0.0f || h != 0.0f) 
        {
            PlayDust();
        }
        else 
        {
            StopDust();
        }
    }

    void PlayDust() 
    {
        if (!particleSystem.isPlaying) 
        {
            particleSystem.Play();
        }
    }

    void StopDust() 
    {
        if (particleSystem.isPlaying) 
        {
            particleSystem.Stop();
        }
    }
}
