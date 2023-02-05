using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerStatus
    {
        rooted,
        unroot
    }
    private float speed = 1f;
    [SerializeField] PlayerStatus playerStatus = PlayerStatus.unroot;
    [SerializeField] private Transform target;

    [SerializeField] private Transform waterPool;
    [SerializeField] private GameObject rootPrefab;
    [SerializeField] private GameObject UI;
    private Transform destination;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private Water water;

    private float attackCost = 5.0f;

    private Coroutine walk = null;

    private bool canMove = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        water = GetComponent<Water>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetWater());
    }

    // Update is called once per frame
    void Update()
    {
        if (GetDiretion().x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (GetDiretion().x < 0)
        {
            spriteRenderer.flipX = true;
        }
        AttackPrep();
    }

    private IEnumerator GetWater()
    {
        while (true)
        {
            if (waterPool != null)
            {
                if (water.currentWater < water.maxWater && playerStatus == PlayerStatus.rooted)
                {
                    waterPool.SendMessage("GetWater", transform, SendMessageOptions.DontRequireReceiver);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Attack()
    {
        if (target != null)
        {
            if (water.currentWater >= attackCost)
            {
                water.AddWater(-attackCost);
                GameObject go = Instantiate(rootPrefab, target.transform.position, Quaternion.identity);
                //set parent
            }
        }
    }

    private void AttackPrep()
    {
        if (playerStatus == PlayerStatus.rooted && target != null && water.currentWater >= attackCost)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Slime"))
        {
            if (target == null)
            {
                target = other.transform;
            }
        }

        if (other.CompareTag("Water"))
        {
            if (waterPool == null)
            {
                waterPool = other.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Slime"))
        {
            if (target != null && other.transform == target.transform)
            {
                target = null;
            }
        }

        if (other.CompareTag("Water"))
        {
            if (waterPool != null && other.transform == waterPool.transform)
            {
                waterPool = null;
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public void Unroot()
    {
        playerStatus = PlayerStatus.unroot;
        canMove = true;
        UI.SetActive(true);
    }

    public void Root()
    {
        playerStatus = PlayerStatus.rooted;
        canMove = false;
        UI.SetActive(true);
    }

    public void SetRoot()
    {
        animator.SetBool("Rooted", playerStatus == PlayerStatus.unroot);
        if (walk != null)
        {
            StopMove();
        }

        if (playerStatus == PlayerStatus.unroot)
        {
            canMove = false;
        }
    }

    private void StartMove(Transform transform)
    {
        if (playerStatus == PlayerStatus.unroot && canMove)
        {

            if (walk != null)
            {
                StopMove();
            }
            destination = transform;
            destination.SendMessage("Connect", SendMessageOptions.DontRequireReceiver);
            walk = StartCoroutine(Move());
        }
        else
        {
            // transform.SendMessage("Disconnect", SendMessageOptions.DontRequireReceiver);
        }
    }

    private IEnumerator Move()
    {
        Vector2 pos = transform.position;
        Vector2 des = destination.position;
        animator.SetBool("Walk", true);
        while (Vector2.Distance(pos, des) > 0.01f)
        {
            Vector2 dir = (des - pos).normalized;
            rb2d.MovePosition((Vector2)transform.position + dir * Time.deltaTime * speed);
            pos = transform.position;


            yield return null;
        }
        StopMove();
    }

    private Vector2 GetDiretion()
    {
        Vector2 pos = transform.position;
        if (destination != null)
        {
            Vector2 des = destination.position;
            if (Vector2.Distance(des, pos) < 0.01f)
            {
                return Vector2.zero;
            }
            return (des - pos).normalized;
        }

        if (target != null)
        {
            Vector2 des = target.position;
            if (Vector2.Distance(des, pos) < 0.01f)
            {
                return Vector2.zero;
            }
            return (des - pos).normalized;
        }

        return Vector2.zero;
    }

    private void StopMove()
    {
        StopCoroutine(walk);
        animator.SetBool("Walk", false);
        if (destination != null)
        {
            destination.SendMessage("Disconnect", SendMessageOptions.DontRequireReceiver);

        }
        destination = null;
    }
}
