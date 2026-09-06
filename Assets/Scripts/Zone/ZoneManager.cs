using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.Zone
{
    public class ZoneManager : MonoBehaviour
    {
        [SerializeField] private ZoneData _zoneData;

        private void Awake()
        {
            ResetProgress();
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<RewardWonEvent>(OnRewardWon);
            GameEventBus.Subscribe<RestartConfirmedEvent>(OnRestartConfirmed);
            GameEventBus.Subscribe<LeaveConfirmedEvent>(OnLeaveConfirmed);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<RewardWonEvent>(OnRewardWon);
            GameEventBus.Unsubscribe<RestartConfirmedEvent>(OnRestartConfirmed);
            GameEventBus.Unsubscribe<LeaveConfirmedEvent>(OnLeaveConfirmed);
        }
        private void OnRewardWon(RewardWonEvent e) => IncrementZone();
        private void OnRestartConfirmed(RestartConfirmedEvent e) => ResetProgress();
        private void OnLeaveConfirmed(LeaveConfirmedEvent e) => ResetProgress();
        
        public void ResetProgress()
        {
            _zoneData.Reset();
            GameEventBus.Publish(new ZoneChangedEvent(_zoneData.CurrentZone, _zoneData.CurrentZoneType));
        }

        public void IncrementZone()
        {
            _zoneData.IncrementZone();
            GameEventBus.Publish(new ZoneChangedEvent(_zoneData.CurrentZone, _zoneData.CurrentZoneType));
        }
        private void OnDestroy()
        {
            _zoneData.Reset();
        }
    }
}