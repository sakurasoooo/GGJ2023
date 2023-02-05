using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlock : MonoBehaviour
{
    [SerializeField] private float maxWater = 100;
    [SerializeField] private float currentWater = 100;

    private Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        currentWater = Random.Range(maxWater / 2, maxWater);
    }

    private void Update()
    {
        material.SetFloat("_Dissolve", currentWater / maxWater);
    }
    private void LateUpdate()
    {
        if (currentWater <= 0)
        {
            Destroy(gameObject);
        }


    }

    private void GetWater(Transform receiver)
    {
        float value = Mathf.Min(10f, currentWater);
        currentWater -= value;
        receiver.SendMessage("AddWater", value);
        receiver.SendMessage("Recovery", value);


    }
}
