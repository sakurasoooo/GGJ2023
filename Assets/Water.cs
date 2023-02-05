using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Water : MonoBehaviour
{
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        Debug.Log(tilemap.GetTile(tilemap.WorldToCell(other.transform.position)).name);
    }
}
