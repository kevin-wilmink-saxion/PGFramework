using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UpdateTextBasedOnSliderUI : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI text;

        private void Start()
        {
            UpdateText(slider.value);
        }

        private void OnEnable()
        {
            if (slider != null)
            {
                slider.onValueChanged.AddListener(UpdateText);
            }
        }

        private void OnDisable()
        {
            if (slider != null)
            {
                slider.onValueChanged.RemoveListener(UpdateText);
            }
        }

        private void UpdateText(float value)
        {
            if (text != null && slider != null)
            {
                text.text = $"{value}/{slider.maxValue}";
            }
        }
    }
}