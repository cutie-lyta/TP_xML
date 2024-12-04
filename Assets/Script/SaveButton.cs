using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SaveButton : MonoBehaviour
{
    private int _counter;
    
    [SerializeField] TimeCounter _time;
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TMP_Text _counterText;

    private void Awake()
    {
        XmlDocument xmlDoc = new XmlDocument();
        if (!File.Exists(Application.dataPath + "/save.xml")) return;
        
        xmlDoc.Load(Application.dataPath + "/save.xml");

        foreach (XmlNode xmlNode in xmlDoc.ChildNodes[1])
        {
            switch (xmlNode.Name)
            {
                case "time":
                    _time.CurrentTime = float.Parse(xmlNode.InnerText, CultureInfo.InvariantCulture);
                    break;
                case "counter":
                    _counter = int.Parse(xmlNode.InnerText);
                    _counterText.text = "Nombre de sauvegarde: " + _counter;
                    break;
                case "name":
                    _inputField.text = xmlNode.InnerText;
                    break;
            }
        }
    }

    public void OnClick()
    {
        _counter++;
        _counterText.text = "Nombre de sauvegarde: " + _counter;
        
        // Prepare context
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            NewLineOnAttributes = true
        };
        
        
        // Open document
        XmlWriter writer;
        if(File.Exists(Application.dataPath + "/save.xml")) File.Delete(Application.dataPath + "/save.xml"); 
        writer = XmlWriter.Create(Application.dataPath + "/save.xml", settings);
        
        writer.WriteStartDocument();
        writer.WriteStartElement("Save");
        
        // Save variable
        // Counter
        writer.WriteStartElement("counter");
        writer.WriteString(_counter.ToString());
        writer.WriteEndElement();
        
        // Time
        writer.WriteStartElement("time");
        writer.WriteString(_time.CurrentTime.ToString(CultureInfo.InvariantCulture));
        writer.WriteEndElement();
        
        // Time
        writer.WriteStartElement("name");
        writer.WriteString(_inputField.text);
        writer.WriteEndElement();
        
        // Close document
        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
    }
}
