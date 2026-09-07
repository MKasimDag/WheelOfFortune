using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Reward
{
    public class RewardListView : MonoBehaviour
    {
        [SerializeField] private RewardItemView _itemPrefab;
        [SerializeField] private Transform _itemContainer;

        private readonly List<RewardItemView> _spawnedItems = new();

        public void Populate(IReadOnlyDictionary<string, RewardCacheData.Entry> entries)
        {
            ClearItems();

            foreach (var pair in entries)
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