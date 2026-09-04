using UnityEngine;
using WheelOfFortune.Zone;

namespace WheelOfFortune.Config
{
    [CreateAssetMenu(menuName = "WheelOfFortune/WheelConfigSet")]
    public class WheelConfigSet : ScriptableObject
    {
        [SerializeField] private WheelVisualConfig _normal;
        [SerializeField] private WheelVisualConfig _safe;
        [SerializeField] private WheelVisualConfig _super;

        public WheelVisualConfig Get(ZoneType type) => type switch
        {
            ZoneType.Safe => _safe,
            ZoneType.Super => _super,
            _ => _normal
        };
    }
}