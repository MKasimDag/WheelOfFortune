using UnityEngine;

namespace WheelOfFortune.Config
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Config/RewardPool")]
    public class RewardPool : ScriptableObject
    {
        [SerializeField] private SliceRewardData[] _rewards;
        public SliceRewardData[] Rewards => _rewards;
    }
}