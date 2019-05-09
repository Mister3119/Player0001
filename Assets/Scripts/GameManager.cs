using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SafeGuard;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Light[] lights;

    public PathSave pathSave;
    public ParticleSystem[] particleSystems;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Plus d'un Object GameManager dans la scene.");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        lights = FindObjectsOfType<Light>();
        particleSystems = FindObjectsOfType<ParticleSystem>();

        if(QualitySettings.GetQualityLevel() == 0)
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < lights.Length; i++)
            {
                LightShadows lightShadows = LightShadows.Hard;
                lights[i].shadows = lightShadows;
            }
        }
    }
}

[System.Serializable]
public class PathSave
{
    public string pathSetting = "/saves/Settings.dat";
    public string pathInput = "/saves/Input.dat";
}

