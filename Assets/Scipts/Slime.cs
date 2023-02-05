using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private GameObject fx;
    [SerializeField] private float speed = 0.3f;
    [SerializeField] float damage = 10f;
    [SerializeField] LayerMask attackLayer;
    [SerializeField] private Transform destination = null;
    [SerializeField] private Transform target = null;

    private Rigidbody2D rb2D;

    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider2D;

    private bool canMove = false;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        destination = GameObject.Find("Target").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        Vector2 dir = GetDestination();
        if (dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            rb2D.MovePosition(rb2D.position + GetDestination() * Time.fixedDeltaTime * speed);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (target == null)
            {
                target = other.transform;
            }
        }
        else
        if (other.CompareTag("BigTree"))
        {
            if (target == null)
            {
                target = other.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (target != null && other.transform == target.transform)
            {
                target = null;
            }
        }
        else
        if (other.CompareTag("BigTree"))
        {
            if (target != null && other.transform == target.transform)
            {
                target = null;
            }
        }
    }

    private Vector2 GetDestination()
    {
        if (target != null)
        {
            return (target.transform.position - transform.position).normalized;
        }
        if (destination != null)
        {
            return (destination.position - transform.position).normalized;
        }
        return (transform.position - transform.position).normalized;
    }

    private void SetMove()
    {
        canMove = !canMove;
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
            if (results[i].CompareTag("Player"))
            {
                results[i].SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                break;
            }
            else
            {
                if (results[i].CompareTag("BigTree"))
                {
                    results[i].SendMessage("TakeDamage", damage / 2, SendMessageOptions.DontRequireReceiver);
                    break;
                }
            }
        }
    }

    private void Death()
    {
        Instantiate(fx,transform.position,Quaternion.identity);
        GameManager.gameManager.AddScore();
        Destroy(gameObject);
    }
}
