using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [Serializable]
    public class ButtonContainer
    {
        public GameObject buttonPrefab;
        public Transform buttonContainer;

        public void clearButtons()
        {
            Utilities.DestroyChildren(buttonContainer);
        }
        
        
        public GameObject AddButton(string buttonText, UnityAction onClick)
        {
            
            GameObject nwButton = AddButton(buttonText);
            nwButton.GetComponent<Button>().onClick.AddListener(onClick);
            
            return nwButton;
        }
        
        
        public GameObject AddButton(string buttonText)
        {
            GameObject nwButton = GameObject.Instantiate(buttonPrefab);
            nwButton.transform.SetParent(buttonContainer, false);
            var textMesh = nwButton.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            textMesh.text = buttonText;
        
            return nwButton;
        }
        
    }
}