using UnityEngine;

public class LazyKeybindings : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (Screen.fullScreen == true)
            {
                float rw = 1280f;
                float rh = 720f;
                Screen.SetResolution(Mathf.RoundToInt(rw), Mathf.RoundToInt(rh), false);
            }
            else
            {
                PlayerPrefs.SetInt("UnitySelectMonitor", 0);
                var display = Display.displays[0];
                int resWidth = display.systemWidth;
                int resHeight = display.systemHeight;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.SetResolution(resWidth, resHeight, true);
            }
        }
    }
}