using UnityEngine;

namespace WheelOfFortune.Config
{
    public abstract class SliceData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _weight = 1f;

        public Sprite Icon => _icon;
        public float Weight => _weight;

        public abstract bool IsBomb { get; }
    }
}