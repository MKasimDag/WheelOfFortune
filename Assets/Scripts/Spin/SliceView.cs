using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Config;

namespace WheelOfFortune.Spin
{
    public class SliceView : MonoBehaviour
    {
        [SerializeField] private Image ui_image_icon;

        private void OnValidate()
        {
            if (ui_image_icon == null)
                ui_image_icon = GetComponentInChildren<Image>();
        }

        public void SetData(SliceData slice)
        {
            ui_image_icon.sprite = slice.Icon;
        }
    }
}