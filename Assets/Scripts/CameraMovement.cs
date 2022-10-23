using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private RectTransform groundTransform;

    private float cameraHalfWidth;
    private float cameraHalfHeight;
    private float groundHalfWidth;
    private float groundHalfHeight;


    // Start is called before the first frame update
    void Start()
    {
        Camera main = Camera.main;
        cameraHalfHeight = main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * main.aspect;
        groundHalfWidth = groundTransform.position.x + groundTransform.rect.width / 2;
        groundHalfHeight = groundTransform.position.y + groundTransform.rect.height / 2;
    }

    // Update is called once per frame
    void Update()
    {


        //Debug.Log(player.position.y);

        if (player.position.y + cameraHalfHeight < groundHalfHeight &&
            player.position.y - cameraHalfHeight > -groundHalfHeight)
        {
            transform.position = new Vector3(
                transform.position.x,
                player.position.y + offset.y,
                offset.z
            );
        }

        if (player.position.x + cameraHalfWidth < groundHalfWidth &&
            player.position.x - cameraHalfWidth > -groundHalfWidth)
        {
            transform.position = new Vector3(
                player.position.x + offset.x,
                transform.position.y,
                offset.z
            );
        }

        //transform.position = new Vector3(
        //     player.position.x + offset.x,
        //     transform.position.y,
        //     offset.z);

    }
}
