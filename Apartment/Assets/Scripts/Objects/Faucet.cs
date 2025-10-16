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
    private ParticleSystem waterVFX;

    void Start()
    {
        player = GameObject.Find("Player");
        originalParent = transform.parent;
        restingPosition = transform.position;
        waterVFX = GameObject.Find("WaterVFX").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (beingDragged)
        {
            bool playVFX = true;
            Vector3 newPosition = new Vector3(transform.position.x, restingPosition.y, restingPosition.z);
            // Pull(player.transform);
            if (transform.position.x <= restingPosition.x)
            {
                newPosition.x = restingPosition.x;
                playVFX = false;
            }
            else if (transform.position.x > restingPosition.x + 0.025)
            {
                newPosition.x = (float)(restingPosition.x + 0.025);
            }

            if (playVFX && !waterVFX.isPlaying)
            {
                waterVFX.Play();
            }
            else if (!playVFX && waterVFX.isPlaying)
            {
                waterVFX.Stop();
            }

            Quaternion angle = Quaternion.AngleAxis((newPosition.x - restingPosition.x) * 1200, new Vector3(1, 0, 0));
            Quaternion angle2 = Quaternion.AngleAxis(Mathf.Clamp(transform.parent.position.z - newPosition.z, -1f, 1f) * 45f, new Vector3(0, 0, 1));
            Quaternion angle3 = angle * angle2;
            var emission = waterVFX.emission;
            emission.rateOverTime = (newPosition.x - restingPosition.x) * 40000f;
            newPosition.y += (newPosition.x - restingPosition.x) * 2f;
            transform.SetPositionAndRotation(newPosition, angle3);
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
