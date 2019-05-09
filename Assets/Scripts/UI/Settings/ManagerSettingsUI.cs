using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SafeGuard;
using System.IO;

public class ManagerSettingsUI : MonoBehaviour {

    public static ManagerSettingsUI instance;

    public string pathSave = "/saves/Settings.dat";

    [Header("ListOption")]
    public xmlReader traduction;
    public List<ListCategory> listCategory;

    public VarSave varSave;

    public List<SettingInt> settingInts;
    public List<SettingBool> settingBools;
    public List<SettingString> settingStrings;

    public List<string> ChangeInt;
    public List<string> ChangeBool;

    public float RatioScreen;

    FullScreenMode fullScreenMode;

    private void Awake()
    {
        if (File.Exists(Application.dataPath + pathSave))
        {
            varSave = (VarSave)file.Load(pathSave);
        }
        else
        {
            file.Save(pathSave, varSave);
        }

        switch (GetInt("Ratio"))
        {
            case 0:
                RatioScreen = 1.7777777777777f;
                break;
            case 1:
                RatioScreen = 1.3333333333333f;
                break;
        }

        if (instance != null)
        {
            Debug.LogError("Plus d'un Object ManagerSettingsUI dans la scene.");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    { 
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.ExclusiveFullScreen:
                SetInt("Screen", 0);
                break;
            case FullScreenMode.Windowed:
                SetInt("Screen", 1);
                break;
            case FullScreenMode.FullScreenWindow:
                SetInt("Screen", 2);
                break;
        }

        switch (QualitySettings.GetQualityLevel())
        {
            case 0:
                SetInt("Quality", 0);
                break;
            case 1:
                SetInt("Quality", 1);
                break;
            case 2:
                SetInt("Quality", 2);
                break;
        }

        file.Save(pathSave, varSave);
    }

    public VarSave getVarSave()
    {
        if (File.Exists(Application.dataPath + pathSave))
        {
            varSave = (VarSave)file.Load(pathSave);
        }
        else
        {
            file.Save(pathSave, varSave);
        }
        

        return varSave;
    }

    public void ChangeLanguage()
    {
        traduction.ValueChangeCheck(GetInt("Language"));

        for (int i = 0; i < settingInts.Count; i++)
        {
            settingInts[i].Refrech();
        }

    }

    private void RefrechAll()
    {
        for (int i = 0; i < settingInts.Count; i++)
        {
            settingInts[i].Refrech();
        }

        for (int i = 0; i < settingBools.Count; i++)
        {
            settingBools[i].Refrech();
        }
    }

    public void SetInt(string name, int _value)
    {
        for (int i = 0; i < varSave.varSaveInts.Count; i++)
        {
            if (varSave.varSaveInts[i].Name == name)
            {
                varSave.varSaveInts[i].value = _value;
            }
        }
    }

    public void SetBool(string name, bool _value)
    {
        for (int i = 0; i < varSave.varSaveBools.Count; i++)
        {
            if (varSave.varSaveBools[i].Name == name)
            {
                varSave.varSaveBools[i].value = _value;
                ChangeBoolAdd(name);
            }
        }
    }

    public int GetInt(string name)
    {
        int _value = 0;

        for (int i = 0; i < varSave.varSaveInts.Count; i++)
        {
            if (varSave.varSaveInts[i].Name == name)
            {
                _value = varSave.varSaveInts[i].value;
            }
        }
        return _value;
    }

    public bool GetBool(string name)
    {
        bool _value = false;

        for (int i = 0; i < varSave.varSaveBools.Count; i++)
        {
            if (varSave.varSaveBools[i].Name == name)
            {
                _value = varSave.varSaveBools[i].value;
            }
        }
        return _value;
    }

    public void ChangeIntAdd(string _name)
    {
        bool DejaAjouter = false;
        for (int i = 0; i < ChangeInt.Count; i++)
        {
            if (ChangeInt[i] == _name)
                DejaAjouter = true;
        }

        if (!DejaAjouter)
            ChangeInt.Add(_name);
    }

    public void ChangeBoolAdd(string _name)
    {
        bool DejaAjouter = false;
        for (int i = 0; i < ChangeBool.Count; i++)
        {
            if (ChangeBool[i] == _name)
                DejaAjouter = true;
        }

        if (!DejaAjouter)
            ChangeBool.Add(_name);
    }

    public void CategoryOpen(int value)
    {

        for (int i = 0; i < listCategory.Count; i++){
            if(listCategory[i].Category != null){
                if(listCategory[i].Category.activeSelf){
                    listCategory[i].Category.SetActive(false);
                }
            }
        }

        if(listCategory[value].Category != null){
            listCategory[value].Category.SetActive(true);
        }

        varSave = (VarSave)file.Load(pathSave);
    }

    private void Quality(int value)
    {
        QualitySettings.SetQualityLevel(value, true);
    }

    public void fullScreen(int value)
    {
        switch (value)
        {
            case 0:
                fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                fullScreenMode = Screen.fullScreenMode;
                break;
        }
        Screen.fullScreenMode = fullScreenMode;
    }

    public void SetResolution(int _ID)
    {
        int _heigth;

        switch (_ID)
        {
            case 0:
                _heigth = 720;
                break;
            case 1:
                _heigth = 768;
                break;
            case 2:
                _heigth = 1080;
                break;
            case 3:
                _heigth = 1440;
                break;
            default:
                _heigth = Screen.height;
                break;
        }
        Screen.SetResolution((int)Mathf.Round(_heigth * RatioScreen), _heigth, Screen.fullScreenMode);
        print(_heigth);
    }

    public void SetRatio(int _ratioID)
    {
        switch (_ratioID)
        {
            case 0:
                RatioScreen = 1.7777777777777f;
                break;
            case 1:
                RatioScreen = 1.3333333333333f;
                break;
        }
        Screen.SetResolution((int)Mathf.Round(Screen.height * RatioScreen), Screen.height, Screen.fullScreenMode);
    }

    public void Apply()
    {
        file.Save(pathSave, varSave);

        for (int i = 0; i < ChangeInt.Count; i++)
        {
            switch(ChangeInt[i])
            {
                case "Language":
                    ChangeLanguage();        
                    break;
                case "Quality":
                    Quality(GetInt("Quality"));
                    break;
                case "Screen":
                    fullScreen(GetInt("Screen"));
                    break;
                case "Ratio":
                    SetRatio(GetInt("Ratio"));
                    break;
                case "Resolution":
                    SetResolution(GetInt("Resolution"));
                    break;
            }
            print(ChangeInt[i]);
            ChangeInt.Clear();
        }

        for(int i = 0; i < ChangeInt.Count; i++)
        {
            switch (ChangeBool[i])
            {
                case "":

                    break;
            }
            ChangeBool.Clear();
        }
    }

    public void OnEnable()
    {
        for (int i = 0; i < listCategory.Count; i++)
        {
            if (listCategory[i].Category != null)
            {
                if (listCategory[i].Category.activeSelf)
                {
                    listCategory[i].Category.SetActive(false);
                }
            }
        }
    }
}

[System.Serializable]
public class VarSave
{
    public List<VarSaveInt> varSaveInts;
    public List<VarSaveBool> varSaveBools;
}

[System.Serializable]
public class VarSaveInt
{
    public string Name;
    public int value;
}

[System.Serializable]
public class VarSaveBool
{
    public string Name;
    public bool value;
}

[System.Serializable]
public class ListCategory
{
    public string Name;
    public GameObject Category;

}
