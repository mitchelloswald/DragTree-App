using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour

{
    public GameObject pauseUI;

    public void OnClick()
    {
        pauseUI.SetActive(false);
    }
}

