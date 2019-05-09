using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System;
using UnityEngine.UI;

public class ChangeKey : MonoBehaviour
{
    public string Name;

    public Text TextButton;

    public ManagerKeys managerKeys;
    public KeyCode keycode;


    void Start()
    {
        managerKeys = gameObject.transform.parent.parent.parent.parent.GetComponent<ManagerKeys>();

        keycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), GetInput(managerKeys.saveInput, Name));

        managerKeys.Keys.Add(gameObject.GetComponent<ChangeKey>());

        TextButton.text = keycode.ToString();
    }

    //outil permetant de recuperé la touche souvegardé présédament
    private string GetInput(List<Key> _saveInput, string name)
    {
        string _name = "None";

        for (int i = 0; i < _saveInput.Count; i++)
        {
            if (_saveInput[i].Name == name)
            {
                _name = _saveInput[i].key;
            }
        }
        return _name;
    }

    //asigner none
    public void SetNone()
    {
        keycode = (KeyCode)System.Enum.Parse(typeof(KeyCode), "None");
        managerKeys.Save();
        TextButton.text = keycode.ToString();
    }

    //evoyer l'event click boutton au manager key 
    public void ButtonAction()
    {
        managerKeys.SetKey(this);
    }
}

