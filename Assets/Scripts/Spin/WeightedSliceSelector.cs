using UnityEngine;

namespace WheelOfFortune.Spin
{
    public class WeightedSliceSelector : ISliceSelector
    {
        private readonly int _sliceCount;
        private readonly int _bombSliceIndex;

        public WeightedSliceSelector(int sliceCount, int bombSliceIndex)
        {
            _sliceCount = sliceCount;
            _bombSliceIndex = bombSliceIndex;
        }

        public SpinResult Select(bool includesBomb)
        {
            int index;
            do
            {
                index = Random.Range(0, _sliceCount);
            } while (!includesBomb && index == _bombSliceIndex);

            bool isBomb = includesBomb && index == _bombSliceIndex;
            return new SpinResult(index, isBomb);
        }
    }
}