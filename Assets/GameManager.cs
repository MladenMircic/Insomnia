using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer groundSprite;
    [SerializeField] private int breadcrumbCount = 10;
    private int treeCount;
    [SerializeField] private Sprite circle;
    private float breadcrumbOffsetY;
    [SerializeField] private Sprite treeSprite;
    [SerializeField] private GameObject treePrefab;


    // Start is called before the first frame update
    void Start()
    {
        treeCount = 500;
        generateSafeZones();
        generateTrees();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateSafeZones()
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

    private void generateTrees()
    {
        float mapWidth = groundSprite.bounds.size.x / 2;
        float mapHeight = groundSprite.bounds.size.y / 2;
        for (int i = 0; i < treeCount; i++)
        {
            var tree = Instantiate(treePrefab);
            var quadrant = i % 4;
            Debug.Log(quadrant);
            float randomOffsetX = 0.0f, randomOffsetY = 0.0f;
            switch (quadrant) {
                case 0:
                    randomOffsetX = Random.Range(0, mapWidth);
                    randomOffsetY = Random.Range(0, mapHeight);
                    break;
                case 1:
                    randomOffsetX = Random.Range(-mapWidth, 0);
                    randomOffsetY = Random.Range(0, mapHeight);
                    break;
                case 2:
                    randomOffsetX = Random.Range(-mapWidth, 0);
                    randomOffsetY = Random.Range(-mapHeight, 0);
                    break;
                case 3:
                    randomOffsetX = Random.Range(0, mapWidth);
                    randomOffsetY = Random.Range(-mapHeight, 0);
                    break; 
            }
            tree.transform.SetPositionAndRotation(
                new Vector3(randomOffsetX, randomOffsetY),
                Quaternion.identity);
        }
    }
}
