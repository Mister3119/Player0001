using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBool : MonoBehaviour
{
    private ManagerSettingsUI managerSettingsUI;

    public string Name;
    public Image Image;
    public bool ID;

    public void OnEnable()
    {
        managerSettingsUI = ManagerSettingsUI.instance;
        managerSettingsUI.settingBools.Add(this);
        Move(managerSettingsUI.GetBool(Name));
    }

    public void Button()
    {
        ID = !ID;
        Move(ID);
        managerSettingsUI.ChangeBool.Add(Name);
    }

    public void Move(bool _value)
    {
        ID = _value;
        managerSettingsUI.SetBool(Name, _value);
        Image.enabled = _value;
    }

    public void Refrech()
    {
        Move(ID);
    }
}
