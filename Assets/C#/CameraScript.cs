using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public SpriteRenderer tree;

    // Use this for initialization
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = tree.bounds.size.x / tree.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = tree.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = tree.bounds.size.y / 2 * differenceInSize;
        }
    }
}