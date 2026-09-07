using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Events;
using WheelOfFortune.Reward;
using WheelOfFortune.Zone;

namespace WheelOfFortune.UI
{
    public class LeaveButton : MonoBehaviour
    {
        [SerializeField] private Button ui_button_leave;
        [SerializeField] private ZoneData _zoneData;
        [SerializeField] private RewardManager _rewardManager;

        private bool _isSpinning;

        private void OnValidate()
        {
            if (ui_button_leave == null)
                ui_button_leave = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            ui_button_leave.onClick.AddListener(OnLeaveClicked);
            RefreshInteractable();
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<SpinStartedEvent>(OnSpinStarted);
            GameEventBus.Subscribe<SpinResultEvent>(OnSpinResult);
            GameEventBus.Subscribe<ZoneChangedEvent>(OnZoneChanged);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<SpinStartedEvent>(OnSpinStarted);
            GameEventBus.Unsubscribe<SpinResultEvent>(OnSpinResult);
            GameEventBus.Unsubscribe<ZoneChangedEvent>(OnZoneChanged);
        }

        private void OnSpinStarted(SpinStartedEvent e) { _isSpinning = true; RefreshInteractable(); }
        private void OnSpinResult(SpinResultEvent e) { _isSpinning = false; RefreshInteractable(); }
        private void OnZoneChanged(ZoneChangedEvent e) => RefreshInteractable();

        private void RefreshInteractable()
        {
            ui_button_leave.interactable = !_isSpinning && _zoneData.CurrentZoneType != ZoneType.Normal;
        }

        private void OnLeaveClicked()
        {
            _rewardManager.Leave();
        }
    }
}