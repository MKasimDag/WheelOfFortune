using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Config;
using WheelOfFortune.Events;
using WheelOfFortune.Zone;

namespace WheelOfFortune.Spin
{
    public class WheelSpriteSwitcher : MonoBehaviour
    {
        [SerializeField] private Image _wheelImage;
        [SerializeField] private Image _wheelWinIndicatorImage;
        [SerializeField] private ZoneData _zoneData;
        [SerializeField] private WheelConfigSet _configSet;

        private void OnEnable()
        {
            GameEventBus.Subscribe<ZoneChangedEvent>(OnZoneChanged);
            ApplySprite(_zoneData.CurrentZoneType);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<ZoneChangedEvent>(OnZoneChanged);
        }

        private void OnZoneChanged(ZoneChangedEvent e) => ApplySprite(e.Type);

        private void ApplySprite(ZoneType type)
        {
            _wheelImage.sprite = _configSet.Get(type).WheelSprite;
            _wheelWinIndicatorImage.sprite = _configSet.Get(type).WheelWinIndicatorSprite;
        }
    }
}