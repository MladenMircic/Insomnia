using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer groundSprite;
    [SerializeField] private int breadcrumbCount = 10;
    [SerializeField] private GameObject safeZone;
    [SerializeField] private Sprite treeSprite;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private int treeCount;
    [SerializeField] private float treeSpawnThreshold;
    private int safeCount;

    private float breadcrumbOffsetY;
    private List<GameObject> breadcrumbs = new List<GameObject>();
    private List<GameObject> trees = new List<GameObject>();

    private System.Diagnostics.Stopwatch stopwatch;

    // How many safe zones are colliding with the player
    public int SafeCount
    {
        get
        {
            return safeCount;
        }
        set
        {
            safeCount = value;
            if (safeCount == 1)
            {
                if (!stopwatch.IsRunning)
                    stopwatch.Start();
            }
            if (safeCount == 0)
            {
                stopwatch.Stop();
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        generateSafeZones();
        generateTrees();
        stopwatch = new System.Diagnostics.Stopwatch();
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
        float lastX = -1;
        float lastY = -1;
        for (int i = 0; i < breadcrumbCount; i++)
        {
            GameObject breadcrumb = Instantiate(safeZone);
            breadcrumbs.Add(breadcrumb);
            float randomOffsetX = Random.Range(-groundHalfWidth, groundHalfWidth);
            breadcrumb.transform.SetPositionAndRotation(
                new Vector3(randomOffsetX, breadcrumbOffsetY),
                Quaternion.identity);
            if (lastX != -1)
            {
                Debug.DrawLine(
                    new Vector3(lastX, lastY),
                    new Vector3(randomOffsetX, breadcrumbOffsetY),
                    Color.red,
                    3000f
                );
            }
            lastX = randomOffsetX;
            lastY = breadcrumbOffsetY;
            breadcrumbOffsetY += offset;
        }
    }

    private void generateTrees()
    {
        for (int i = 0; i < treeCount; i++)
        {
            GameObject tree = Instantiate(treePrefab);
            float[] randomOffset = generateRandomPosition(i);
            bool chooseAnotherOffset = true;
            while (true)
            {
                if (trees.Count != 0)
                {
                    foreach (var instantiatedTree in trees)
                    {
                        float x1 = instantiatedTree.transform.position.x;
                        float y1 = instantiatedTree.transform.position.y;
                        if (Mathf.Sqrt(Mathf.Pow(randomOffset[0] - x1, 2) + Mathf.Pow(randomOffset[1] - y1, 2)) >= treeSpawnThreshold)
                        {
                            chooseAnotherOffset = false;
                            break;
                        }
                    }
                    if (!chooseAnotherOffset)
                    {
                        randomOffset = generateRandomPosition(i);
                        chooseAnotherOffset = true;
                    }
                }
                else break;
                if (!chooseAnotherOffset) break;
            }
            

            tree.transform.SetPositionAndRotation(
                new Vector3(randomOffset[0], randomOffset[1]),
                Quaternion.identity
            );
        }
    }

    private float[] generateRandomPosition(
        int index
    )
    {
        float mapWidth = groundSprite.bounds.size.x / 2;
        float mapHeight = groundSprite.bounds.size.y / 2;
        int quadrant = index % 4;
        float randomOffsetX = 0.0f, randomOffsetY = 0.0f;
        switch (quadrant)
        {
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
        return new float[] { randomOffsetX, randomOffsetY };
    }
}
