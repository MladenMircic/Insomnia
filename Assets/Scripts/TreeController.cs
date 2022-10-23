using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreeController : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private int playerOverlapRadius;
    private GameManager gameManager;
    private SceneManager sceneManager;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool lastIterOpenEyes = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        StartCoroutine("CheckPlayerCollision");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ChangeOpacity(0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ChangeOpacity(1f);
        }
    }

    private void ChangeOpacity(float opacity)
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            opacity);
    }

    private IEnumerator CheckPlayerCollision()
    {
        while (true)
        {
            Collider2D collider = Physics2D.OverlapCircle(
                transform.position,
                playerOverlapRadius,
                playerMask
            );
            if (gameManager.SafeCount > 0 &&  collider != null)
            {
                animator.SetBool("PlayerEnteredZone", true);
                if (!lastIterOpenEyes)
                {
                    sceneManager.NumOfEyesOpen++;
                }
                lastIterOpenEyes = true;
            }
            else
            {
                animator.SetBool("PlayerEnteredZone", false);
                if (lastIterOpenEyes)
                {
                    lastIterOpenEyes = false;
                    sceneManager.NumOfEyesOpen--;
                }
            }
            yield return new WaitForSeconds(.5f);
        }
    }

}