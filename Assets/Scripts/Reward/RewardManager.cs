using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Events;
using WheelOfFortune.Spin;

namespace WheelOfFortune.Reward
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private RewardCacheData _rewardData;

        private void OnEnable()
        {
            GameEventBus.Subscribe<RewardWonEvent>(OnRewardWon);
            GameEventBus.Subscribe<RestartConfirmedEvent>(OnRestartConfirmed);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<RewardWonEvent>(OnRewardWon);
            GameEventBus.Unsubscribe<RestartConfirmedEvent>(OnRestartConfirmed);
        }

        private void OnRewardWon(RewardWonEvent e) => Add(e.RewardId, e.Amount, e.Icon);
        private void OnRestartConfirmed(RestartConfirmedEvent e) => ResetProgress();

        public void Add(string id, int amount, Sprite icon)
        {
            _rewardData.Add(id, amount, icon);
            GameEventBus.Publish(new RewardChangedEvent());
        }

        public void ResetProgress()
        {
            _rewardData.Reset();
            GameEventBus.Publish(new RewardChangedEvent());
        }

        public void Leave()
        {
            var snapshot = new Dictionary<string, RewardCacheData.Entry>(_rewardData.Entries);
            GameEventBus.Publish(new LeaveConfirmedEvent(snapshot));
            ResetProgress();
        }

        private void OnDestroy()
        {
            _rewardData.Reset();
        }
    }
}