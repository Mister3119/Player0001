using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {
    public ManagerSettingsUI managerSettingsUI;
    public ManagerKeys managerKeys;
    public List<ListMenu> listMenu;
    [Header("Sound")]
    public AudioClip audioClip;

    private void Awake()
    {
        managerSettingsUI.SetResolution(managerSettingsUI.GetInt("Resolution"));
        managerSettingsUI.SetRatio(managerSettingsUI.GetInt("Ratio"));
    }

    void Start () {
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();
        OpenMenu(0);

        VarSave varSave = managerSettingsUI.getVarSave();
        managerSettingsUI.traduction.ValueChangeCheck(varSave.varSaveInts[0].value);
        managerKeys.load();
    }

    public void Demarer()
    {
        SceneManager.LoadScene(1);
    }


    public void OpenMenu(int value){
        for(int i = 0; i < listMenu.Count; i++){
            if(listMenu[i].Menu != null){
                if(listMenu[i].Menu.activeSelf){
                    listMenu[i].Menu.SetActive(false);
                }
            }
        }

        if(listMenu[value].Menu != null){
            listMenu[value].Menu.SetActive(true);
        }
    }

    public void Quitter()
    {
        Application.Quit();
    }
}

[System.Serializable]
public class ListMenu
{
    public string Name;
    public GameObject Menu;
}
