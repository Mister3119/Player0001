using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingString : MonoBehaviour
{
    private ManagerSettingsUI managerSettingsUI;

    public string Name;
    public Text Text;
    public List<string> Texts;
    public int ID;

    private void OnEnable()
    {
        managerSettingsUI = ManagerSettingsUI.instance;
        managerSettingsUI.settingStrings.Add(this);
        Move(managerSettingsUI.GetInt(Name));
    }

    public void Button(int _Value)
    {
        ID = Mathf.Clamp(ID + _Value, 0, Texts.Count - 1);
        Move(ID);
        managerSettingsUI.ChangeIntAdd(Name);
    }

    public void Move(int _ID)
    {
        
        ID = _ID;
        managerSettingsUI.SetInt(Name, _ID);
        if(Text != null)
            Text.text = Texts[_ID];
    }

    public void Refrech()
    {
        Move(ID);
    }
}
