using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Reward
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Reward/RewardCacheData")]
    public class RewardCacheData : ScriptableObject
    {
        public readonly struct Entry
        {
            public readonly int Count;
            public readonly Sprite Icon;

            public Entry(int count, Sprite icon)
            {
                Count = count;
                Icon = icon;
            }
        }

        private readonly Dictionary<string, Entry> _entries = new();

        public IReadOnlyDictionary<string, Entry> Entries => _entries;

        public void Add(string id, int amount, Sprite icon)
        {
            _entries.TryGetValue(id, out var existing);
            _entries[id] = new Entry(existing.Count + amount, icon);
        }

        public void Reset()
        {
            _entries.Clear();
        }
    }
}