using UnityEngine;

namespace WheelOfFortune.Config
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Slice/Reward")]
    public class RewardSliceData : SliceData
    {
        [SerializeField] private int _baseAmount = 100;
        [SerializeField] private AnimationCurve _zoneScaling = AnimationCurve.Linear(1, 1, 30, 3);

        public override bool IsBomb => false;

        public int GetAmount(int zone)
        {
            float multiplier = _zoneScaling.Evaluate(zone);
            return Mathf.RoundToInt(_baseAmount * multiplier);
        }
    }
}