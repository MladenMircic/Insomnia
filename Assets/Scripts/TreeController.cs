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

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        Debug.Log(sceneManager);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D playerColliderInZone = Physics2D.OverlapCircle(
            transform.position,
            playerOverlapRadius,
            playerMask
        );
        Collider2D playerColliderOutZone = Physics2D.OverlapCircle(
            transform.position,
            playerOverlapRadius * 2,
            playerMask
        );
        if (gameManager.SafeCount >= 1 && playerColliderInZone != null)
        {
            animator.SetBool("PlayerInZone", true);
            sceneManager.NumOfEyesOpen++;
        }
        else
        {
            animator.SetBool("PlayerInZone", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ChangeOpacity(0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
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

}