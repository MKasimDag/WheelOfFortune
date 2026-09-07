using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Reward
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Reward/PersistentInventoryData")]
    public class PersistentInventoryData : ScriptableObject
    {
        private const string GoldId = "UI_icon_gold";

        private readonly Dictionary<string, RewardCacheData.Entry> _entries = new();

        public IReadOnlyDictionary<string, RewardCacheData.Entry> Entries => _entries;

        public void Add(string id, int amount, Sprite icon)
        {
            _entries.TryGetValue(id, out var existing);
            _entries[id] = new RewardCacheData.Entry(existing.Count + amount, icon ?? existing.Icon);
        }

        public void MergeFrom(IReadOnlyDictionary<string, RewardCacheData.Entry> source)
        {
            foreach (var pair in source)
                Add(pair.Key, pair.Value.Count, pair.Value.Icon);
        }

        public int GetCount(string id) => _entries.TryGetValue(id, out var entry) ? entry.Count : 0;

        public bool TrySpendGold(int amount)
        {
            if (!_entries.TryGetValue(GoldId, out var entry) || entry.Count < amount)
                return false;

            _entries[GoldId] = new RewardCacheData.Entry(entry.Count - amount, entry.Icon);
            return true;
        }

        public void Clear() => _entries.Clear();

        public void LoadFromSave(string id, int count, Sprite icon)
        {
            _entries[id] = new RewardCacheData.Entry(count, icon);
        }
    }
}