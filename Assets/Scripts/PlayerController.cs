using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private SceneManager sceneManager;
    [SerializeField]
    private float playerTreeScanRadius;
    [SerializeField]
    private LayerMask treeLayerMask;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("PeriodicalDamageOutZone");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");

            // Movement
            rb2d.velocity = new Vector2(xAxis, yAxis) * speed;

            // Change direction
            if (rb2d.velocity.magnitude > 0.01f)
            {
                spriteRenderer.flipX = rb2d.velocity.x < 0;
            }


            animator.SetFloat("Speed", rb2d.velocity.magnitude);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    private IEnumerator PeriodicalDamageOutZone()
    {
        while (true)
        {
            Debug.Log("RADI");
            yield return new WaitForSeconds(3f);
            if (gameManager.SafeCount == 0)
            {
                gameManager.PlayerHealth -= 10;
            }
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }
}
 