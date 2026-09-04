using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Config;

namespace WheelOfFortune.Spin
{
    public class WeightedSliceSelector : ISliceSelector
    {
        public SpinResult Select(IReadOnlyList<SliceData> slices, int zone)
        {
            float totalWeight = 0f;
            for (int i = 0; i < slices.Count; i++)
                totalWeight += slices[i].Weight;

            float roll = Random.Range(0f, totalWeight);
            float cumulative = 0f;
            int selectedIndex = slices.Count - 1;

            for (int i = 0; i < slices.Count; i++)
            {
                cumulative += slices[i].Weight;
                if (roll <= cumulative)
                {
                    selectedIndex = i;
                    break;
                }
            }

            var slice = slices[selectedIndex];
            int rewardAmount = slice is RewardSliceData reward ? reward.GetAmount(zone) : 0;

            return new SpinResult(selectedIndex, slice.IsBomb, rewardAmount);
        }
    }
}