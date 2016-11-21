using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class LanguageControl : MonoBehaviour {

	public TextAsset GameAsset;


	public Dictionary<string,string> translation = new Dictionary<string, string>();

	// Use this for initialization
	void Start () {
		//get file path
		//PlayerPrefs.SetString("SELECTED_LANGUAGE","POLISH");

		loadXmlLangConfig();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	 * loads language config file to search a file with selected language 
	 */
	private void loadXmlLangConfig() {

		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		xmlDoc.LoadXml(GameAsset.text); // load the file.
		XmlNodeList langList = xmlDoc.GetElementsByTagName("LANGUAGE"); 
		
		foreach (XmlNode l in langList) {
			XmlAttributeCollection langAtributes =  l.Attributes;
			
			foreach (XmlAttribute a in langAtributes) {
				if (a.Name == "name") {
					if (a.InnerText == PlayerPrefs.GetString("SELECTED_LANGUAGE","ENGLISH")) {

						foreach (XmlNode s in l.ChildNodes) {
							//add translation into dictionary
							translation.Add(s.Name, s.InnerText);	
							//Debug.Log("Read: " + s.Name + " = " + s.InnerText);
						}
					}
				}
			}			
		}

	}


}
