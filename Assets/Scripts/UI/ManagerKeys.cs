using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SafeGuard;
using System.IO;

public class ManagerKeys : MonoBehaviour
{
    public static ManagerKeys instance;

    public GameObject PanelInfo;
    public List<Key> saveInput;
    ChangeKey keyScript;

    bool Change;

    public List<ChangeKey> Keys = new List<ChangeKey>();

    public void SetKey(ChangeKey script)
    {
        keyScript = script;
        Change = true;
        PanelInfo.GetComponent<RectTransform>().position = script.TextButton.GetComponent<RectTransform>().position;
        PanelInfo.SetActive(true);
    }

    void Awake()
    {
        //file.Save(GameManager.instance.pathSave.pathInput, saveInput);
        
        if (instance != null)
        {
            Debug.LogError("Plus d'un Object ManagerKeys dans la scene.");
        }
        else
        {
            instance = this;
        }

        load();
    }

    private void Start()
    {
        if (PanelInfo != null)
            PanelInfo.SetActive(false);
    }

    void Update()
    {
        if (Change)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    for(int i= 0; i < Keys.Count; i++)
                    {
                        if (kcode == Keys[i].keycode)
                        {
                            Keys[i].SetNone();
                            SetInput(saveInput, Keys[i].Name, "None");
                        }
                    }
                    ChangeKey(kcode);
                }
            }
        }
    }

    public void load()
    {
        if (File.Exists(Application.dataPath + "/saves/Input.dat"))
        {
            saveInput = (List<Key>)file.Load("/saves/Input.dat");
        }
        else
        {
            file.Save("/saves/Input.dat", saveInput);
        }

    }

    private void SetInput(List<Key> _saveInput, string _name, string _key)
    {
        for (int i = 0; i < _saveInput.Count; i++)
        {
            if (_saveInput[i].Name == _name)
            {
                _saveInput[i].key = _key;
            }
        }
    }

    public void ChangeKey(KeyCode kcode)
    {
        SetInput(saveInput, keyScript.Name, kcode.ToString());
        keyScript.keycode = kcode;
        keyScript.TextButton.text = kcode.ToString();
        PanelInfo.SetActive(false);
        Change = false;
        Save();
        print("Save");
    }

    public void Save()
    {
        file.Save(GameManager.instance.pathSave.pathInput, saveInput);
    }
}

[System.Serializable]
public class Key
{
    public string Name;
    public string key;
}
