using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] float damage = 34f;
    [SerializeField] LayerMask attackLayer;
    private BoxCollider2D boxCollider2D;

    private void Awake() {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void Attack()
    {
        Collider2D[] results = new Collider2D[10]; ;
        ContactFilter2D filter2d = new ContactFilter2D();
        filter2d.useLayerMask = true;
        filter2d.useTriggers = false;
        filter2d.SetLayerMask(attackLayer);
        int count = boxCollider2D.OverlapCollider(filter2d, results);
        for (int i = 0; i < count; i++)
        {
            if (results[i].CompareTag("Slime"))
            {
                results[i].SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                break;
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
