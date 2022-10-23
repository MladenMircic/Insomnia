using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform groundTransform;
    [SerializeField] private int breadcrumbCount = 10;
    [SerializeField] private GameObject safeZone;
    [SerializeField] private Sprite treeSprite;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject medicinePrefab;
    [SerializeField] private int treeCount;
    [SerializeField] private float treeSpawnThreshold;
    [SerializeField] private int matrixSize;
    private int safeCount;

    private float groundHalfWidth;
    private float groundHalfHeight;

    private float breadcrumbOffsetY;
    private List<GameObject> breadcrumbs = new List<GameObject>();
    private List<GameObject> trees = new List<GameObject>();

    private bool unlockEnd = false;
    private int medicineCount;
    private List<GameObject> medicineList = new List<GameObject>();

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

    private int medicineCollected;
    public int MedicineCollected { 
        get { return medicineCollected; } 
        set 
        {
            medicineCount = value;
            if (medicineCollected == medicineCount)
            {
                unlockEnd = true;
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        groundHalfWidth = groundTransform.position.x + groundTransform.rect.width / 2;
        groundHalfHeight = groundTransform.position.y + groundTransform.rect.height / 2;
        generateSafeZones();
        generateTrees();
        generateMedicine();
        stopwatch = new System.Diagnostics.Stopwatch();
    }

    private void generateSafeZones()
    {
        GameObject safeZones = GameObject.Find("SafeZones");
        breadcrumbOffsetY = groundTransform.position.x -
            groundTransform.rect.height / 4;
        float offset = groundTransform.rect.height / breadcrumbCount;
        float groundHalfWidth = groundTransform.rect.width / 2;
        for (int i = 0; i < breadcrumbCount; i++)
        {
            GameObject breadcrumb = Instantiate(safeZone);
            breadcrumb.transform.parent = safeZones.transform;
            breadcrumbs.Add(breadcrumb);
            float randomOffsetX = Random.Range(-groundHalfWidth, groundHalfWidth);
            breadcrumb.transform.SetPositionAndRotation(
                new Vector3(randomOffsetX, breadcrumbOffsetY),
                Quaternion.identity);
            breadcrumbOffsetY += offset;
        }
    }

    private void generateTrees()
    {
        GameObject trees = GameObject.Find("Trees");
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
                        if (Mathf.Abs(tree.transform.position.x) > groundTransform.rect.width / 2 ||
                            Mathf.Abs(tree.transform.position.y) > groundTransform.rect.height)
                        {
                            Destroy(tree);
                        }
                        tree.transform.parent = trees.transform;
                    }
                }
            }
        }
    }

    private void generateMedicine()
    {
        medicineCount = Random.Range(3, 8);
        for (int i = 0; i < medicineCount; i++)
        {
            float x = Random.Range(-groundHalfWidth, groundHalfWidth);
            float y = Random.Range(-groundHalfHeight, groundHalfHeight);

            GameObject medicine = Instantiate(medicinePrefab);
            medicine.transform.SetLocalPositionAndRotation(
                new Vector3(x, y),
                Quaternion.identity
            );
            medicineList.Add(medicine);
        }
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
