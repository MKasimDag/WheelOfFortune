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
        [SerializeField] private int _pastCount = 6;
        [SerializeField] private int _futureCount = 6;

        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _safeColor = Color.cyan;
        [SerializeField] private Color _superColor = Color.yellow;
        [SerializeField] private Color _pastColor = Color.gray;

        private readonly List<ZoneBarItemView> _spawnedItems = new();
        private int TotalCount => _pastCount + 1 + _futureCount;

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

        private Color GetColorFor(int zone, ZoneType type)
        {
            if (zone < _zoneData.CurrentZone) return _pastColor;
            return type switch
            {
                ZoneType.Safe => _safeColor,
                ZoneType.Super => _superColor,
                _ => _normalColor
            };
        }

        private void Refresh()
        {
            EnsureItemCount(TotalCount);

            int currentZone = _zoneData.CurrentZone;
            int startZone = currentZone - _pastCount;

            for (int i = 0; i < TotalCount; i++)
            {
                int zone = startZone + i;
                var item = _spawnedItems[i];

                if (zone < 1)
                {
                    item.SetEmpty();
                    continue;
                }

                var type = zone == currentZone ? _zoneData.CurrentZoneType : _zoneData.CalculateZoneType(zone);
                item.SetData(zone, GetColorFor(zone, type));
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