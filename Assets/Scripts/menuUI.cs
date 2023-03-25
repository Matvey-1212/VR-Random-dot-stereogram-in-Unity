using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuUI : MonoBehaviour
{
    private Canvas _UICanvas;
    public GameObject Objects;

    private void Start()
    //получение данных объекта
    {
        _UICanvas = GetComponent<Canvas>();
    }
    public void ToggleMenu()
    //включение и отключение видимости объекта Canvas
    {
        if(Objects.activeSelf)
        {
            Objects.SetActive(false);
        }
        else
        {
            //Objects.SetActive(true);
        }
        _UICanvas.enabled = !_UICanvas.enabled;
    }
}
