using UnityEngine;
using WheelOfFortune.Events;
using WheelOfFortune.Zone;

namespace WheelOfFortune.Spin
{
    public class SpinController : MonoBehaviour
    {
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private ZoneData _zoneData;
        [SerializeField] private int _sliceCount = 8;
        [SerializeField] private int _bombSliceIndex = 0;

        private ISliceSelector _selector;

        private void Awake()
        {
            _selector = new WeightedSliceSelector(_sliceCount, _bombSliceIndex);
        }

        public void Spin()
        {
            if (_wheelView.IsSpinning) return;

            bool includesBomb = _zoneData.CurrentZoneType == ZoneType.Normal;
            var result = _selector.Select(includesBomb);

            _wheelView.SpinTo(result.SliceIndex, () => OnSpinComplete(result));
        }

        private void OnSpinComplete(SpinResult result)
        {
            GameEventBus.Publish(new SpinCompletedEvent(result));
        }
    }
}