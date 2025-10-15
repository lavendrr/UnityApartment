using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucet : MonoBehaviour
{
    private bool beingDragged = false;
    private GameObject player;
    private Vector3 ?positionDelta;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (beingDragged)
        {
            Pull(player.transform);
        }
    }

    void OnUse()
    {
        if (beingDragged)
        {
            beingDragged = false;
            Debug.Log("stopped dragging!");
            positionDelta = null;
        }
        else
        {
            beingDragged = true;
            Debug.Log("started dragging!");
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
