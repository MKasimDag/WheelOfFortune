using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune.UI
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] private Button ui_button_inventory;
        [SerializeField] private InventoryPopup _inventoryPopup;

        private void OnValidate()
        {
            if (ui_button_inventory == null)
                ui_button_inventory = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            ui_button_inventory.onClick.AddListener(_inventoryPopup.Open);
        }
    }
}