using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetGFX : MonoBehaviour
{
    private Dropdown dropDown;
    private string[] _GFXNames;

    // Start is called before the first frame update
    void Start()
    {
        dropDown = GetComponent<Dropdown>();
        _GFXNames = QualitySettings.names;
        List<string> dropOptions = new List<string>();
        foreach (string s in _GFXNames)
        {
            dropOptions.Add(s);
        }
        dropDown.AddOptions(dropOptions);
        dropDown.value = QualitySettings.GetQualityLevel();
    }

    public void SetGfx()
    {
        QualitySettings.SetQualityLevel(dropDown.value, true);
    }
}
