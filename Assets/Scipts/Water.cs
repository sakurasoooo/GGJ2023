using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Water : MonoBehaviour
{
    public float maxWater = 100;
    [Range(0.0f, 100.0f)]
    public float currentWater = 100;

    public Health health;

    [SerializeField] WaterBar waterBar;

     void Awake()
    {
        waterBar.SetMaxWater(maxWater);
        health = GetComponent<Health>();
    }
    private void Start() {
        StartCoroutine(RecoveryHealth());
    }

    private void Update() {
        waterBar.SetWater(currentWater);
    }

    private IEnumerator RecoveryHealth()
    {
        while(true)
        {
            if(currentWater>=0)
            {
                if(health.currentHealth <= health.maxHealth *  0.75f )
                {
                    float value = Mathf.Min(currentWater,Mathf.Min(5.0f, health.maxHealth - health.currentHealth));
                    currentWater -= value;
                    health.Recovery(value);
                    waterBar.SetWater(currentWater);
                }
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    public void AddWater(float water)
    {
        currentWater = Mathf.Clamp(currentWater + water, 0,maxWater);
    }
}
