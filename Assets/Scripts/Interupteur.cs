using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interupteur : MonoBehaviour {

    public TargetLight[] targetLight;

    public Animator animator;

    public Material Lamp_On;
    public Material Lamp_Off;

    public void Action()
    {
        if (targetLight[0].light.activeSelf)
        {
            Animation();
            for(int i = 0; i < targetLight.Length; i++)
            {
                targetLight[i].light.SetActive(false);
                if (targetLight[i].Mesh != null)
                    targetLight[i].Mesh.material = Lamp_Off;
            }          
        }
        else
        {
            Animation();
            for (int i = 0; i < targetLight.Length; i++)
            {
                targetLight[i].light.SetActive(true);
                if(targetLight[i].Mesh != null)
                    targetLight[i].Mesh.material = Lamp_On;
            }
        }
    }

    void Animation()
    {
        if (animator != null)
            animator.SetBool("ON", !animator.GetBool("ON"));
    }


}

[System.Serializable]
public class TargetLight
{
    public GameObject light;
    public MeshRenderer Mesh;

}
