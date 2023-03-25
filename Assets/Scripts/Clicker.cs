using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class Clicker : MonoBehaviour
{
    //объекты передаваемые в скрипт
    public List<GameObject> things;
    public PostProcessSettings postProcessSettings;
    int currentItem = 0;
    public GameObject menu;
    public GameObject Objects;
    
    private int n = 0;

    public void Next()
    {
        
        if (n == 0)
        {
            //проверка активен ли объект
            if (menu.GetComponent<Canvas>().enabled)
                //отключение объекта
            {
                menu.GetComponent<Canvas>().enabled = !menu.GetComponent<Canvas>().enabled;
            }

            if (!Objects.activeSelf)
            {
                //включение объекта
                Objects.SetActive(true);
            }
            //отключение активной фигуры на суене
            things[currentItem].SetActive(false);

            currentItem = (currentItem + 1) % things.Count;

            things[currentItem].SetActive(true);
            
        }
        n += 1;
        if (n == 3)
        {
            n = 0;
        }
    }

    public void Reveal()
    {
        //вызов функции включения стереошума
        postProcessSettings.Reveal();
    }
}
