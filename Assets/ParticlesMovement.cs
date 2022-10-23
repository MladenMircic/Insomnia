using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesMovement : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    private float h, v;
    private float secondsSinceLastInt;
    [SerializeField] private float intervalInSeconds = 1.0f;
    void Start()
    {
        h = 0.0f;
        v = 0.0f;
        currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        this.transform.Translate(h * 5.0f * Time.deltaTime, v * 5.0f * Time.deltaTime, 0.0f);

        secondsSinceLastInt += Time.deltaTime;
        if (secondsSinceLastInt >= intervalInSeconds)
		{
			TakeDamage(3);
            secondsSinceLastInt = 0.0f;
		}

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

    void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
}
