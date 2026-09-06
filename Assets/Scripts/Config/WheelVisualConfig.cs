using UnityEngine;

namespace WheelOfFortune.Config
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Config/WheelVisualConfig")]
    public class WheelVisualConfig : ScriptableObject
    {
        [SerializeField] private Sprite _wheelSprite;
        [SerializeField] private RewardPool _rewardPool;
        [SerializeField] private bool _includesBomb;
        [SerializeField] private BombSliceData _bombSlice;
        [SerializeField] private int _slotCount = 8;

        public Sprite WheelSprite => _wheelSprite;
        public RewardPool RewardPool => _rewardPool;
        public bool IncludesBomb => _includesBomb;
        public BombSliceData BombSlice => _bombSlice;
        public int SlotCount => _slotCount;
    }
}