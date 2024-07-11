using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolueControl : Singleton<VolueControl>
{
    public Slider Music;
    public Slider SFX;
    public void SetResolution1920()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void SetResolution1600()
    {
        Screen.SetResolution(1600, 900, false);
    }
    public void SetResolution4()
    {
        Screen.SetResolution(400, 300, false);
    }
}
