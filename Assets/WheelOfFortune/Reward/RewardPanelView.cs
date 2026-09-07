using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.Reward
{
    public class RewardPanelView : MonoBehaviour
    {
        [SerializeField] private RewardCacheData _rewardCacheData;
        [SerializeField] private RewardListView _listView;

        private void OnEnable()
        {
            GameEventBus.Subscribe<RewardChangedEvent>(OnRewardChanged);
            Refresh();
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<RewardChangedEvent>(OnRewardChanged);
        }

        private void OnRewardChanged(RewardChangedEvent e) => Refresh();

        private void Refresh() => _listView.Populate(_rewardCacheData.Entries);
    }
}