using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune.Zone
{
    public class ZoneBarItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ui_text_zone_value;

        private void OnValidate()
        {
            if (ui_text_zone_value == null)
                ui_text_zone_value = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetData(int zoneNumber, Color textColor)
        {
            ui_text_zone_value.text = zoneNumber.ToString();
            ui_text_zone_value.color = textColor;
        }
    }
}