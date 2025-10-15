using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{
    private bool beingDragged = false;
    private GameObject player;
    private Transform originalParent;
    private Vector3? positionDelta;
    private Vector3 restingPosition;

    void Start()
    {
        player = GameObject.Find("Player");
        originalParent = transform.parent;
        restingPosition = transform.position;
    }

    void Update()
    {
        if (beingDragged)
        {
            // Pull(player.transform);
            if (transform.position.x < restingPosition.x)
            {
                transform.SetPositionAndRotation(new Vector3(restingPosition.x, transform.position.y, transform.position.z), transform.rotation);
            }
            else if (transform.position.x > restingPosition.x + 0.1)
            {
                transform.SetPositionAndRotation(new Vector3(restingPosition.x + 1/10, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }

    void OnUse()
    {
        if (beingDragged)
        {
            beingDragged = false;
            Debug.Log("stopped dragging!");
            positionDelta = null;
            transform.parent = originalParent;
        }
        else
        {
            beingDragged = true;
            Debug.Log("started dragging!");
            transform.parent = player.transform;
        }
    }
    
    void Pull(Transform playerTransform)
    {
        if (positionDelta == null)
        {
            positionDelta = playerTransform.position - transform.position;
        }
        transform.position = playerTransform.position - (Vector3)positionDelta;
    }
}
