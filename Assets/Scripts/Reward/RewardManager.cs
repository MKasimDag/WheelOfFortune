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
            GameEventBus.Subscribe<SpinCompletedEvent>(OnSpinCompleted);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<SpinCompletedEvent>(OnSpinCompleted);
        }

        private void OnSpinCompleted(SpinCompletedEvent e)
        {
            if (e.Result.IsBomb)
                ResetProgress();
            else
                Add(e.Result.RewardId, e.Result.RewardAmount, e.Result.RewardIcon);
        }

        public void Add(string id, int amount, Sprite icon)
        {
            _rewardData.Add(id, amount, icon);
            Publish();
        }

        public void ResetProgress()
        {
            _rewardData.Reset();
            Publish();
        }

        private void Publish()
        {
            GameEventBus.Publish(new RewardChangedEvent());
        }

        private void OnDestroy()
        {
            ResetProgress();
        }
    }
}