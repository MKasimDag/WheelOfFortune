using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Config;

namespace WheelOfFortune.Spin
{
    public class SliceView : MonoBehaviour
    {
        [SerializeField] private Image ui_image_icon;
        [SerializeField] private TextMeshProUGUI ui_text_amount_value;

        private void OnValidate()
        {
            if (ui_image_icon == null)
                ui_image_icon = GetComponentInChildren<Image>();
            if (ui_text_amount_value == null)
                ui_text_amount_value = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetData(SliceData slice, int zone)
        {
            ui_image_icon.sprite = slice.Icon;

            if (slice is SliceRewardData reward)
            {
                ui_text_amount_value.gameObject.SetActive(true);
                ui_text_amount_value.text = reward.GetAmount(zone).ToString();
            }
            else
            {
                ui_text_amount_value.gameObject.SetActive(false);
            }
        }
    }
}