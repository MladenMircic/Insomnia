using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private GameManager gameManager;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") &&
            gameManager.isUnlockedEnd())
        {
            animator.SetTrigger("OpenGate");
        }
    }

    public void setGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
