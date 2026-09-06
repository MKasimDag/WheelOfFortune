using UnityEngine;
using UnityEngine.UI;
using WheelOfFortune.Events;
using WheelOfFortune.Reward;

namespace WheelOfFortune.UI
{
    public class CollectedPopup : PopupBase
    {
        //[SerializeField] private RewardItemView _itemPrefab;
        //[SerializeField] private Transform _itemContainer;
        [SerializeField] private Button ui_button_ok;

        private readonly System.Collections.Generic.List<RewardItemView> _spawnedItems = new();

        private void Start()
        {
            ui_button_ok.onClick.AddListener(Hide);
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<LeaveConfirmedEvent>(OnLeaveConfirmed);
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<LeaveConfirmedEvent>(OnLeaveConfirmed);
        }

        private void OnLeaveConfirmed(LeaveConfirmedEvent e)
        {
            /*ClearItems();
            foreach (var pair in e.CollectedRewards)
            {
                var item = Instantiate(_itemPrefab, _itemContainer);
                item.SetData(pair.Value.Icon, pair.Value.Count);
                _spawnedItems.Add(item);
            }*/
            Show();
        }

        private void ClearItems()
        {
            foreach (var item in _spawnedItems) Destroy(item.gameObject);
            _spawnedItems.Clear();
        }
    }
}