using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingInt : MonoBehaviour
{
    private ManagerSettingsUI managerSettingsUI;

    public string Name;
    public Text Text;
    public List<string> VarTradutionName;
    public int ID;

    public void OnEnable()
    {
        managerSettingsUI = ManagerSettingsUI.instance;
        managerSettingsUI.settingInts.Add(this);
        Move(managerSettingsUI.GetInt(Name));
    }

    public void Button(int _Value)
    {
        ID = Mathf.Clamp(ID + _Value, 0, VarTradutionName.Count - 1);
        Move(ID);
        managerSettingsUI.ChangeIntAdd(Name);
    }

    public void Move(int _ID)
    {
        string _Name;
        ID = _ID;
        managerSettingsUI.traduction.languages[managerSettingsUI.traduction.currentLanguage].TryGetValue(VarTradutionName[_ID], out _Name);
        managerSettingsUI.SetInt(Name, _ID);
        if(Text != null)
            Text.text = _Name;
    }

    public void Refrech()
    {
        Move(ID);
    }
}
