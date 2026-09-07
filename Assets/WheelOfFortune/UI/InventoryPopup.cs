using UnityEngine;
using WheelOfFortune.Reward;

namespace WheelOfFortune.UI
{
    public class InventoryPopup : PopupBase
    {
        [SerializeField] private RewardListView _listView;
        [SerializeField] private PersistentInventoryData _persistentInventory;
        [SerializeField] private UnityEngine.UI.Button ui_button_close;

        private void Start()
        {
            ui_button_close.onClick.AddListener(Hide);
        }

        public void Open()
        {
            _listView.Populate(_persistentInventory.Entries);
            Show();
        }
    }
}