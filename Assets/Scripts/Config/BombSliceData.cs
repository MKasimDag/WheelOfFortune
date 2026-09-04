using UnityEngine;

namespace WheelOfFortune.Config
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Slice/Bomb")]
    public class BombSliceData : SliceData
    {
        public override bool IsBomb => true;
    }
}