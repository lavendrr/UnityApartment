using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
            Vector3 newPosition = new Vector3(transform.position.x, restingPosition.y, restingPosition.z);
            // Pull(player.transform);
            if (transform.position.x <= restingPosition.x)
            {
                newPosition.x = restingPosition.x;
            }
            else if (transform.position.x > restingPosition.x + 0.025)
            {
                newPosition.x = (float)(restingPosition.x + 0.025);
            }
            Quaternion angle = Quaternion.AngleAxis((newPosition.x - restingPosition.x) * 1200, new Vector3(1, 0, 0));
            newPosition.y += (newPosition.x - restingPosition.x) * 2f;
            transform.SetPositionAndRotation(newPosition, angle);
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
