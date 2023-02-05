using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", 2);
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
