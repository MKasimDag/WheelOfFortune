using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Events;

namespace WheelOfFortune.UI
{
    public class BombPopup : PopupBase
    {
        [SerializeField] private Button ui_button_restart;

        private void OnValidate()
        {
            if (ui_button_restart == null)
                ui_button_restart = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            ui_button_restart.onClick.AddListener(OnRestartClicked);
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<SpinResultEvent>(OnSpinResult);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<SpinResultEvent>(OnSpinResult);
        }

        private void OnSpinResult(SpinResultEvent e)
        {
            if (e.Result.IsBomb) Show();
        }

        private void OnRestartClicked()
        {
            Hide();
            GameEventBus.Publish(new RestartConfirmedEvent());
        }
    }
}