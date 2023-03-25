using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wristUI : MonoBehaviour
{
    private Canvas _UICanvas;

    private void Start()
    {
        _UICanvas = GetComponent<Canvas>();
    }
    public void ToggleMenu()
    {
        _UICanvas.enabled = !_UICanvas.enabled;
    }
}
