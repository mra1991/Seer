using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetResolution : MonoBehaviour
{
    private Dropdown dropDown;
    private Resolution[] _GFXRes;

    // Start is called before the first frame update
    void Start()
    {
        dropDown = GetComponent<Dropdown>();
        _GFXRes = Screen.resolutions;
        List<string> dropOptions = new List<string>();
        int pos = 0, i = 0;
        Resolution currentResolution = Screen.currentResolution;
        foreach (Resolution r in _GFXRes) //finding current pos
        {
            string val = r.ToString();
            dropOptions.Add(val);
            if (r.width == currentResolution.width && r.height == currentResolution.height && r.refreshRate == currentResolution.refreshRate)
            {
                pos = i;
            }
            i++;
        }
        dropDown.AddOptions(dropOptions);
        dropDown.value = pos;
    }

    public void SetRes()
    {
        Resolution r = _GFXRes[dropDown.value];
        Screen.SetResolution(r.width, r.height, Screen.fullScreenMode, r.refreshRate);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
