using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Events;
using WheelOfFortune.Reward;

namespace WheelOfFortune.UI
{
    public class BombPopup : PopupBase
    {
        [SerializeField] private Button ui_button_restart;
        [SerializeField] private Button ui_button_revive;
        [SerializeField] private RewardManager _rewardManager;
        [SerializeField] private int _revivePrice = 25;

        private void Start()
        {
            ui_button_restart.onClick.AddListener(OnRestartClicked);
            ui_button_revive.onClick.AddListener(OnReviveClicked);
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
            if (!e.Result.IsBomb) return;

            ui_button_revive.interactable = _rewardManager.GetPersistentGold() >= _revivePrice;
            Show();
        }

        private void OnRestartClicked()
        {
            Hide();
            GameEventBus.Publish(new RestartConfirmedEvent());
        }

        private void OnReviveClicked()
        {
            if (!_rewardManager.TryRevive(_revivePrice)) return;
            Hide();
        }
    }
}