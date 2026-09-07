using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Config;
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
        [SerializeField] private float _slotWidth = 80f;
        [SerializeField] private float _slideDuration = 0.3f;

        [SerializeField] private Image ui_image_current_highlight;
        [SerializeField] private WheelConfigSet _configSet;
        [SerializeField] private Color _normalColor = Color.white;
        [SerializeField] private Color _safeColor = Color.cyan;
        [SerializeField] private Color _superColor = Color.yellow;
        [SerializeField] private Color _pastColor = Color.gray;

        private const int BufferCount = 1;
        private int VisibleCount => _pastCount + 1 + _futureCount;
        private int TotalCount => VisibleCount + BufferCount * 2;

        private readonly List<ZoneBarItemView> _items = new();
        private int _displayedZone = -1;

        private void OnEnable()
        {
            GameEventBus.Subscribe<ZoneChangedEvent>(OnZoneChanged);

            EnsureItemCount(TotalCount);
            _displayedZone = _zoneData.CurrentZone;
            LayoutItems(animate: false);
            UpdateHighlightSprite();
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<ZoneChangedEvent>(OnZoneChanged);
        }

        private void OnZoneChanged(ZoneChangedEvent e)
        {
            bool slide = _displayedZone >= 0 && e.Zone == _displayedZone + 1;
            _displayedZone = e.Zone;

            if (slide)
                RecycleLeftItem();

            LayoutItems(animate: slide);
            UpdateHighlightSprite();
        }

        private void RecycleLeftItem()
        {
            var recycled = _items[0];
            _items.RemoveAt(0);
            _items.Add(recycled);

            recycled.RectTransform.DOKill();
            recycled.RectTransform.anchoredPosition = SlotPosition(TotalCount);
        }

        private void LayoutItems(bool animate)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                item.RectTransform.DOKill();

                if (animate)
                    item.RectTransform.DOAnchorPos(SlotPosition(i), _slideDuration).SetEase(Ease.OutQuad);
                else
                    item.RectTransform.anchoredPosition = SlotPosition(i);

                ApplyZone(item, ZoneAt(i));
            }
        }

        private Vector2 SlotPosition(int slotIndex) => new Vector2((slotIndex - BufferCount) * _slotWidth, 0f);

        private int ZoneAt(int slotIndex) => _displayedZone - _pastCount - BufferCount + slotIndex;

        private void ApplyZone(ZoneBarItemView item, int zone)
        {
            if (zone < 1)
            {
                item.SetEmpty();
                return;
            }

            var type = zone == _zoneData.CurrentZone ? _zoneData.CurrentZoneType : _zoneData.CalculateZoneType(zone);
            item.SetData(zone, GetColorFor(zone, type));
        }

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

        private void UpdateHighlightSprite()
        {
            if (ui_image_current_highlight != null)
                ui_image_current_highlight.sprite = _configSet.Get(_zoneData.CurrentZoneType).ZoneHighlightSprite;
        }

        private void EnsureItemCount(int count)
        {
            while (_items.Count < count)
                _items.Add(Instantiate(_itemPrefab, _itemContainer));
        }
    }
}