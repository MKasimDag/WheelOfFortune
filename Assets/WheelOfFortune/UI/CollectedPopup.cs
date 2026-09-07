using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.UI
{
    public class CollectedPopup : PopupBase
    {
        [SerializeField] private Reward.RewardListView _listView;
        [SerializeField] private UnityEngine.UI.Button ui_button_ok;

        private void Start()
        {
            ui_button_ok.onClick.AddListener(Hide);
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<LeaveConfirmedEvent>(OnLeaveConfirmed);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<LeaveConfirmedEvent>(OnLeaveConfirmed);
        }

        private void OnLeaveConfirmed(LeaveConfirmedEvent e)
        {
            _listView.Populate(e.CollectedRewards);
            Show();
        }
    }
}