using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Xml;

public class xmlReader : MonoBehaviour
{
    public TextAsset dictionary;

    public string languageName;
    public int currentLanguage;

    public List<TextReference> textReference;

    public List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    void Awake()
    {
        Reader();
    }

    private void Start()
    {

    }

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictionary.text);
        XmlNodeList languageList = xmlDoc.GetElementsByTagName("language");

        foreach(XmlNode laguageValue in languageList)
        {
            XmlNodeList languageContent = laguageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach(XmlNode value in languageContent)
            {
                obj.Add(value.Name, value.InnerText);
            }

            languages.Add(obj);
        }
    }

    public void ValueChangeCheck(int value)
    {
        currentLanguage = value;
        languages[currentLanguage].TryGetValue("Name", out languageName);
        for (int i = 0; i < obj.Count; i++)
        {
            if (i < textReference.Count)
            {
                string _Name = "";
                languages[currentLanguage].TryGetValue(textReference[i].VarName, out _Name);
                if (textReference[i].Text != null)
                    textReference[i].Text.text = _Name;
            }      
        }      
    }

    public void changeVarText(string _VarName, Text _text)
    {
        string _Name = "";
        languages[currentLanguage].TryGetValue(_VarName, out _Name);
        _text.text = _Name;
    }


}

[System.Serializable]
public class TextReference
{
    public string VarName;
    public Text Text;
}
