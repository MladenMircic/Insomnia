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
    [SerializeField] private int matrixSize;
    private int safeCount;
    static object lockObject;

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
                Debug.Log(stopwatch.ElapsedMilliseconds / 1000);
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        generateSafeZones();
        generateTrees();
        lockObject = new object();
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
        var matrix = Matrix(matrixSize);

        var quadrants = new int[] { 1, 2, 3, 4 };

        foreach (int quadrant in quadrants)
        {
            for (int i = 0; i < matrixSize; i++) 
            {
                var module = Random.Range(2, 5);
                for (int j = 0; j < matrixSize; j++) 
                {
                    GameObject tree = Instantiate(treePrefab);
                    var treeW = tree.GetComponent<SpriteRenderer>().bounds.size.x;
                    var treeY = tree.GetComponent<SpriteRenderer>().bounds.size.y;
                    float posX = 0.0f;
                    float posY = 0.0f;
                    if ((j+1+ i) % module == 0) 
                    {
                        switch (quadrant) 
                        {
                            case 1:
                                posX = (j * treeW * 1.1f);
                                posY = (i * treeY);
                                break;
                            case 2:
                                posX = -(j * treeW * 1.1f);
                                posY = (i * treeY);
                                break;
                            case 3:
                                posX = -(j * treeW * 1.1f);
                                posY = -(i * treeY * 1.15f);
                                break;
                            case 4:
                                posX = (j * treeW * 1.1f);
                                posY = -(i * treeY * 1.15f);
                                break;
                        }
                        
                        tree.transform.SetPositionAndRotation(
                            new Vector3(posX, posY),
                            Quaternion.identity);
                    }
                }
            }
        }
    }

    private float[] generateRandomPosition(
        int index
    )
    {
        float mapWidth = groundSprite.bounds.size.x / 2;
        float mapHeight = groundSprite.bounds.size.y / 2;
        int quadrant = index % 4;
        Debug.Log(quadrant);
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

        int[,] Matrix(int n)
    {
        var result = new int[n, n];                    

        int level = 0,
            counter = 1;
        while (level < (int)Mathf.Ceil(n / 2f))
        {
            // Start at top left of this level.
            int x = level, 
                y = level;
            // Move from left to right.
            for (; x < n - level; x++)           
                result[y, x] = counter++;            
            // Move from top to bottom.
            for (y++, x--; y < n - level; y++)            
                result[y, x] = counter++;            
            // Move from right to left.
            for (x--, y--; x >= level; x--)            
                result[y, x] = counter++;            
            // Move from bottom to top. Do not overwrite top left cell.
            for (y--, x++; y >= level + 1; y--)            
                result[y, x] = counter++;            
            // Go to inner level.
            level++;
        }

        return result;
    }
}
