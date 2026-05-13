using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class MessageUIManager: MonoBehaviour
    {
        public GameObject popupContainer;

        public TextMeshProUGUI titleText;
        public TextMeshProUGUI messageText;
        
        
        public GameObject infoPanelContainer;
        public Animator infoPanelAnimator;
        public TextMeshProUGUI infoPanelText;
        public float infoPanelDuration = 3f;

        private Coroutine coroutineInfoPanel;
        
        
        public static MessageUIManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                return;
            }

            Instance = this;
        }


        public void ShowInfoPanel(string message)
        {
            //stop previous if needed
            if (coroutineInfoPanel != null)
                StopCoroutine(coroutineInfoPanel);
            
            coroutineInfoPanel = StartCoroutine(ShowInfoPanelEnumerator(message));
        }

        private IEnumerator ShowInfoPanelEnumerator(string message)
        {
            infoPanelContainer.SetActive(true);
            infoPanelText.text = message;
            infoPanelAnimator.SetBool("show", true);
            yield return new WaitForSeconds(infoPanelDuration);
            ClosePopup();
        }


        public void ClosePopup()
        {
            infoPanelAnimator.SetBool("show", false);
        }
        
        public void ShowPopup(string title, string message)
        {
            popupContainer.gameObject.SetActive(true);
            titleText.text = title;
            messageText.text = message;
        }


        public void HidePopup()
        {
            popupContainer.gameObject.SetActive(false);
        }

    }
}