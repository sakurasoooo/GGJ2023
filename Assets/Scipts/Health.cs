using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Health : MonoBehaviour
{
    
    private float maxHealth = 100;
    [Range(0.0f, 100.0f)]
    public float currentHealth = 100;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
}
