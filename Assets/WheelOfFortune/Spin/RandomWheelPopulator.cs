using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Config;

namespace WheelOfFortune.Spin
{
    public class RandomWheelPopulator : IWheelPopulator
    {
        public List<SliceData> Populate(WheelVisualConfig config)
        {
            var pool = new List<SliceRewardData>(config.RewardPool.Rewards);
            int rewardSlotCount = config.IncludesBomb ? config.SlotCount - 1 : config.SlotCount;

            var result = new List<SliceData>();
            for (int i = 0; i < rewardSlotCount && pool.Count > 0; i++)
            {
                int index = Random.Range(0, pool.Count);
                result.Add(pool[index]);
                pool.RemoveAt(index);   // aynı ödül iki kere çıkmasın
            }

            if (config.IncludesBomb)
                result.Insert(Random.Range(0, result.Count + 1), config.BombSlice);

            return result;
        }
    }
}