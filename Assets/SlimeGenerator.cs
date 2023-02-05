using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject SlimePrefab;
    private int counter = 29;

    private int difficulty = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generator());
    }

    private IEnumerator Generator()
    {
        while (true)
        {
            difficulty = Mathf.Clamp(difficulty + 1, 0, 7);
            for (int i = 0; i < difficulty; i++)
            {
                if (counter > 0)
                {
                    counter--;
                    Instantiate(SlimePrefab, spawnPoints[Random.Range(0,spawnPoints.Length)].position ,Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(30);
        }
    }
}
