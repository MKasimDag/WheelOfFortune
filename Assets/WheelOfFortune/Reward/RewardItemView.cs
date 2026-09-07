using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune.Reward
{
    public class RewardItemView : MonoBehaviour
    {
        [SerializeField] private Image ui_image_icon;
        [SerializeField] private TextMeshProUGUI ui_text_count_value;

        private void OnValidate()
        {
            if (ui_image_icon == null)
                ui_image_icon = GetComponentInChildren<Image>();
            if (ui_text_count_value == null)
                ui_text_count_value = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetData(Sprite icon, int count)
        {
            ui_image_icon.sprite = icon;
            ui_text_count_value.text = count.ToString();
        }
    }
}