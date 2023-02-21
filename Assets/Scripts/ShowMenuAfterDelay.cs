using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectMenu))]

public class ShowMenuAfterDelay : MonoBehaviour
{
    const float delay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ShowMenu", delay);
    }

    void ShowMenu()
    {
        GetComponent<SelectMenu>().PanelToggle(0); //show first panel
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
