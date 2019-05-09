using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 5 / 100;
        style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        float fps = 1.0f / deltaTime;
        int quality = QualitySettings.GetQualityLevel();
        string text = Mathf.Round(fps) + " fps - " + quality + " Quality";
        GUI.Label(rect, text, style); 
    }
}