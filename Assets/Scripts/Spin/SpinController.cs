using UnityEngine;
using WheelOfFortune.Config;
using WheelOfFortune.Events;

namespace WheelOfFortune.Spin
{
    public class SpinController : MonoBehaviour
    {
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private Zone.ZoneData _zoneData;
        [SerializeField] private WheelConfigSet _configSet;

        private ISliceSelector _selector;
        private IWheelPopulator _populator;

        private void Awake()
        {
            _selector = new WeightedSliceSelector();
            _populator = new RandomWheelPopulator();
        }

        public void Spin()
        {
            if (_wheelView.IsSpinning) return;

            var config = _configSet.Get(_zoneData.CurrentZoneType);
            var currentSlices = _populator.Populate(config);

            _wheelView.SetSlices(currentSlices);
            var result = _selector.Select(currentSlices, _zoneData.CurrentZone);

            _wheelView.SpinTo(result.SliceIndex, currentSlices.Count, () => OnSpinComplete(result));
        }

        private void OnSpinComplete(SpinResult result)
        {
            GameEventBus.Publish(new SpinCompletedEvent(result));
        }
    }
}