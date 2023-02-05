using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public int count = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (count <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Disconnect()
    {
        count--;
    }

    private void Connect()
    {
        count++;
    }
}
