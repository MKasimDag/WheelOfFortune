using TMPro;
using UnityEngine;

namespace WheelOfFortune.Zone
{
    public class ZoneBarItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ui_text_zone_value;
        public RectTransform RectTransform => (RectTransform)transform;

        private void OnValidate()
        {
            if (ui_text_zone_value == null)
                ui_text_zone_value = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetData(int zoneNumber, Color textColor)
        {
            ui_text_zone_value.text = zoneNumber.ToString();
            ui_text_zone_value.color = textColor;
            ui_text_zone_value.enabled = true;
        }

        public void SetEmpty()
        {
            ui_text_zone_value.enabled = false;
        }
    }
}