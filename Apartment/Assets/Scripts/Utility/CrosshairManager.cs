using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CrosshairManager : MonoBehaviour
{
    private Crosshair crosshair;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = new Crosshair(GameObject.Find("Crosshair"));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCrosshair();
    }

    private void UpdateCrosshair()
    {
        // Update crosshair state depending on what the player is looking at
        var (type, _) = PlayerInteractions.Instance.InteractionCheck();
        if (type == InteractableType.Grabbable)
        {
            crosshair.SetGrab();
        }
        else if (type == InteractableType.Slottable && PlayerInteractions.Instance.GetGrabStatus())
        {
            crosshair.SetSlot();
        }
        else if (type == InteractableType.Usable)
        {
            crosshair.SetUse();
        }
        else
        {
            crosshair.SetNeutral();
        }
    }
    
}

public class Crosshair
{
    private GameObject obj;
    private UnityEngine.UI.Image img;
    private TextMeshProUGUI tooltip;

    public Crosshair(GameObject gameObject)
    {
        obj = gameObject;
        img = obj.GetComponent<UnityEngine.UI.Image>();
        tooltip = obj.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetNeutral()
    {
        img.color = Color.white;
        tooltip.text = "";
    }

    public void SetGrab()
    {
        img.color = Color.red;
        tooltip.text = "(E)";
    }

    public void SetSlot()
    {
        img.color = Color.green;
        tooltip.text = "(E)";
    }

    public void SetUse()
    {
        img.color = Color.blue;
        tooltip.text = "(Q)";
    }
}
