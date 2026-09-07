using TMPro;
using UnityEngine;
using WheelOfFortune.Events;

namespace WheelOfFortune.Reward
{
    public class GoldDisplayView : MonoBehaviour
    {
        [SerializeField] private PersistentInventoryData _persistentInventory;
        [SerializeField] private TextMeshProUGUI ui_text_gold_value;

        private void OnValidate()
        {
            if (ui_text_gold_value == null)
                ui_text_gold_value = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            GameEventBus.Subscribe<RewardChangedEvent>(OnRewardChanged);
            Refresh();
        }

        private void OnDisable()
        {
            GameEventBus.Unsubscribe<RewardChangedEvent>(OnRewardChanged);
        }

        private void OnRewardChanged(RewardChangedEvent e) => Refresh();

        private void Refresh()
        {
            ui_text_gold_value.text = _persistentInventory.GetCount("UI_icon_gold").ToString();
        }
    }
}