using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextZone : MonoBehaviour
{
    public GameObject DialogueSystem;
    private Vector3 scale;

    private bool inTextZone;

    private void Update()
    {
        DialogueSystem.transform.localScale = scale;
        scale.z = 1;

        if (inTextZone)
        {
            scale.x = Mathf.Lerp(scale.x, 2, 0.3f);
            scale.y = Mathf.Lerp(scale.y, 2, 0.3f);
        }
        else if (!inTextZone)
        {
            scale.x = Mathf.Lerp(scale.x, 0, 0.3f);
            scale.y = Mathf.Lerp(scale.y, 0, 0.3f);
        }

    }
    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Player")
        {
            inTextZone = true;
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Player")
        {
            inTextZone = false;
        }
    }
}
