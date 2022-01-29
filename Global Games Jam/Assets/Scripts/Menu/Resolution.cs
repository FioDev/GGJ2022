using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public Dropdown resolutionDropDown;
    public Toggle toggleButton;

    UnityEngine.Resolution[] resolutions;

    void Awake()
    {
        if (Screen.fullScreen)
        {
            toggleButton.isOn = true;
        }
        else
        {
            toggleButton.isOn = false;
        }
    }

    void Start()
    {
        resolutions = Screen.resolutions;

        if (resolutionDropDown != null)
        {
            resolutionDropDown.ClearOptions();
        }

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResIndex = i;
            }
        }

        if (resolutionDropDown != null)
        {
            resolutionDropDown.AddOptions(options);
            resolutionDropDown.value = currentResIndex;
            resolutionDropDown.RefreshShownValue();
        }

    }

    public void SetResolution(int resIndex)
    {
        UnityEngine.Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool full)
    {
        Screen.fullScreen = full;
    }
}
