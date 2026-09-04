using System.Collections.Generic;
using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.Zone
{
    public class ZoneBarView : MonoBehaviour
    {
        [SerializeField] private ZoneData _zoneData;
        [SerializeField] private ZoneBarItemView _itemPrefab;
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private int _visibleZoneCount = 7;

        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _safeColor = Color.cyan;
        [SerializeField] private Color _superColor = Color.yellow;

        private readonly List<ZoneBarItemView> _spawnedItems = new();

        private void OnEnable()
        {
            GameEventBus.Subscribe<ZoneChangedEvent>(OnZoneChanged);
            Refresh();
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<ZoneChangedEvent>(OnZoneChanged);
        }

        private void OnZoneChanged(ZoneChangedEvent e) => Refresh();

        private Color GetColorFor(ZoneType type) => type switch
        {
            ZoneType.Safe => _safeColor,
            ZoneType.Super => _superColor,
            _ => _normalColor
        };

        private void Refresh()
        {
            EnsureItemCount(_visibleZoneCount);

            int startZone = _zoneData.CurrentZone;
            for (int i = 0; i < _visibleZoneCount; i++)
            {
                int zone = startZone + i;
                var type = i == 0 ? _zoneData.CurrentZoneType : _zoneData.CalculateZoneType(zone);
                _spawnedItems[i].SetData(zone, GetColorFor(type));
            }
        }

        private void EnsureItemCount(int count)
        {
            while (_spawnedItems.Count < count)
            {
                var item = Instantiate(_itemPrefab, _itemContainer);
                _spawnedItems.Add(item);
            }
        }
    }
}