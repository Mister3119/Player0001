using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Four : MonoBehaviour
{

    public Material Four_On;
    public Material Four_Off;

    public Light light;

    public MeshRenderer Mesh;

    bool actived = true;

    public void Action()
    {
        if (actived)
        {
            Mesh.material = Four_Off;
            light.gameObject.SetActive(false);
        }
        else
        {
            light.gameObject.SetActive(true);
            Mesh.material = Four_On;
        }
        actived = !actived;
    }
}
