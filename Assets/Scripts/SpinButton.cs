using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Spin;

namespace WheelOfFortune.UI
{
    public class SpinButton : MonoBehaviour
    {
        [SerializeField] private Button ui_button_spin;
        [SerializeField] private SpinController _spinController;

        private void OnValidate()
        {
            if (ui_button_spin == null)
                ui_button_spin = GetComponent<Button>();
        }

        private void Start()
        {
            ui_button_spin.onClick.AddListener(_spinController.Spin);
        }
    }
}