using UnityEngine;

namespace WheelOfFortune.Config
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Config/Bomb")]
    public class BombSliceData : SliceData
    {
        public override bool IsBomb => true;
    }
}