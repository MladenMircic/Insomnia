using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer groundSprite;
    [SerializeField] private int breadcrumbCount = 10;
    [SerializeField] private Sprite circle;
    private float breadcrumbOffsetY;


    // Start is called before the first frame update
    void Start()
    {
        generateBreadcrumbs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateBreadcrumbs()
    {
        breadcrumbOffsetY = groundSprite.transform.position.y -
            groundSprite.bounds.size.y / 2;
        float offset = groundSprite.bounds.size.y / breadcrumbCount;
        float groundHalfWidth = groundSprite.bounds.size.x / 2;
        for (int i = 0; i < breadcrumbCount; i++)
        {
            GameObject breadcrumb = new GameObject("breadcrumb" + i);
            breadcrumb.AddComponent<SpriteRenderer>();
            breadcrumb.GetComponent<SpriteRenderer>().sprite = circle;
            float randomOffsetX = Random.Range(-groundHalfWidth, groundHalfWidth);
            breadcrumb.transform.SetPositionAndRotation(
                new Vector3(randomOffsetX, breadcrumbOffsetY),
                Quaternion.identity);
            breadcrumbOffsetY += offset;
        }
    }
}
