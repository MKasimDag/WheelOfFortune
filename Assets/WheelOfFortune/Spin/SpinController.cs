using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Config;
using WheelOfFortune.Events;
using WheelOfFortune.Zone;

namespace WheelOfFortune.Spin
{
    public class SpinController : MonoBehaviour
    {
        [SerializeField] private WheelView _wheelView;
        [SerializeField] private ZoneData _zoneData;
        [SerializeField] private WheelConfigSet _configSet;

        private ISliceSelector _selector;
        private IWheelPopulator _populator;
        private List<SliceData> _currentSlices;

        private void Awake()
        {
            _selector = new WeightedSliceSelector();
            _populator = new RandomWheelPopulator();
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<ZoneChangedEvent>(OnZoneChanged);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<ZoneChangedEvent>(OnZoneChanged);
        }

        private void Start()
        {
            PopulateWheel();
        }

        private void OnZoneChanged(ZoneChangedEvent e)
        {
            PopulateWheel();
        }

        private void PopulateWheel()
        {
            var config = _configSet.Get(_zoneData.CurrentZoneType);
            _currentSlices = _populator.Populate(config);
            _wheelView.SetSlices(_currentSlices, _zoneData.CurrentZone);
        }

        public void Spin()
        {
            if (_wheelView.IsSpinning) return;

            GameEventBus.Publish(new SpinStartedEvent());

            var result = _selector.Select(_currentSlices, _zoneData.CurrentZone);
            _wheelView.SpinTo(result.SliceIndex, () => OnSpinComplete(result));
        }

        private void OnSpinComplete(SpinResult result)
        {
            GameEventBus.Publish(new SpinResultEvent(result));
            Debug.Log($"Spin bitti — IsBomb: {result.IsBomb}, RewardId: {result.RewardId}");

            if (!result.IsBomb)
                GameEventBus.Publish(new RewardWonEvent(result.RewardId, result.RewardAmount, result.RewardIcon));
        }
    }
}