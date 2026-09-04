using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Events;
using WheelOfFortune.Zone;

namespace WheelOfFortune.Spin
{
    public class WheelSpriteSwitcher : MonoBehaviour
    {
        [SerializeField] private Image _wheelImage;
        [SerializeField] private ZoneData _zoneData;
        [SerializeField] private Sprite _normalSprite;
        [SerializeField] private Sprite _safeSprite;
        [SerializeField] private Sprite _superSprite;

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
            _wheelImage.sprite = type switch
            {
                ZoneType.Safe => _safeSprite,
                ZoneType.Super => _superSprite,
                _ => _normalSprite
            };
        }
    }
}