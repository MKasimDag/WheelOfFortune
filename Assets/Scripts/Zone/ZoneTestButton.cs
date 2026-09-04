using UnityEngine;
using UnityEngine.UI;

namespace WheelOfFortune.Zone
{
    public class ZoneTestButton : MonoBehaviour
    {
        [SerializeField] private Button ui_button_increment;
        [SerializeField] private ZoneManager _zoneManager;

        private void OnValidate()
        {
            if (ui_button_increment == null)
                ui_button_increment = GetComponent<Button>();
        }

        private void Start()
        {
            ui_button_increment.onClick.AddListener(() => _zoneManager.IncrementZone());
        }
    }
}