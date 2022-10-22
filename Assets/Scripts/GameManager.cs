using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer groundSprite;
    [SerializeField] private int breadcrumbCount = 10;
    [SerializeField] private GameObject safeZone;
    private int safeCount;
    static object lockObject;

    private float breadcrumbOffsetY;
    private List<GameObject> breadcrumbs = new List<GameObject>();

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
        generateBreadcrumbs();
        lockObject = new object();
        stopwatch = new System.Diagnostics.Stopwatch();
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
}
