using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.Reward
{
    public class RewardPanelView : MonoBehaviour
    {
        [SerializeField] private RewardCacheData _rewardCacheData;
        [SerializeField] private RewardItemView _itemPrefab;
        [SerializeField] private Transform _itemContainer;

        private readonly System.Collections.Generic.List<RewardItemView> _spawnedItems = new();

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

        private void Refresh()
        {
            ClearItems();

            foreach (var pair in _rewardCacheData.Entries)
            {
                var item = Instantiate(_itemPrefab, _itemContainer);
                item.SetData(pair.Value.Icon, pair.Value.Count);
                _spawnedItems.Add(item);
            }
        }

        private void ClearItems()
        {
            foreach (var item in _spawnedItems)
                Destroy(item.gameObject);
            _spawnedItems.Clear();
        }
    }
}