using UnityEngine;

namespace WheelOfFortune.Config
{
    [CreateAssetMenu(menuName = "WheelOfFortune/RewardPool")]
    public class RewardPool : ScriptableObject
    {
        [SerializeField] private RewardSliceData[] _rewards;
        public RewardSliceData[] Rewards => _rewards;
    }
}