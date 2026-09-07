using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Config;

namespace WheelOfFortune.Reward
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Reward/RewardCatalog")]
    public class RewardCatalog : ScriptableObject
    {
        [SerializeField] private SliceRewardData[] _allRewards;

        private Dictionary<string, Sprite> _lookup;

        public Sprite GetIcon(string id)
        {
            EnsureLookup();
            return _lookup.TryGetValue(id, out var icon) ? icon : null;
        }

        private void EnsureLookup()
        {
            if (_lookup != null) return;

            _lookup = new Dictionary<string, Sprite>();
            foreach (var reward in _allRewards)
                _lookup[reward.Id] = reward.Icon;
        }
    }
}