using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteAlways]
public class Health : MonoBehaviour
{

    public float maxHealth = 100;
    [Range(0.0f, 100.0f)]
    public float currentHealth = 100;

    [SerializeField] HealthBar healthBar;
    private SpriteRenderer spriteRenderer;

    private bool invincibility = false;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Start()
    {
        currentHealth = maxHealth;


    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);

        spriteRenderer.color = new Color(1,
            Mathf.Clamp01(spriteRenderer.color.g + Time.deltaTime * 2),
            Mathf.Clamp01(spriteRenderer.color.b + Time.deltaTime * 2),
            Mathf.Clamp01(spriteRenderer.color.a + Time.deltaTime * 2)
            );

    }

    public void TakeDamage(float damage)
    {
        if(!invincibility)
        {

        
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth < 0)
        {
            currentHealth = 0;
            SendMessage("Death",SendMessageOptions.DontRequireReceiver);
        }
        spriteRenderer.color = new Color(1,0,0,0.5f);
        }
    }

    private IEnumerator InvincibilityFrame()
    {
        yield return new WaitForSeconds(0.1f);

        invincibility = false;
    }

    private void SetInvincibility()
    {
        invincibility = true;
        StartCoroutine(InvincibilityFrame());
    }

    public void Recovery(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
    }
}
