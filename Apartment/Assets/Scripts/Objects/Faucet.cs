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
            Vector3 newPosition = new Vector3(transform.position.x, restingPosition.y, restingPosition.z);
            // Pull(player.transform);
            if (transform.position.x < restingPosition.x)
            {
                // transform.SetPositionAndRotation(restingPosition, transform.rotation);
                newPosition.x = restingPosition.x;
            }
            else if (transform.position.x > restingPosition.x + 0.1)
            {
                // transform.SetPositionAndRotation(new Vector3(restingPosition.x + 1 / 10, restingPosition.y, restingPosition.z), transform.rotation);
                newPosition.x = restingPosition.x + 1 / 10;
            }
            Quaternion angle = Quaternion.AngleAxis((transform.position.x - restingPosition.x) * 450, new Vector3(1, 0, 0));
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
