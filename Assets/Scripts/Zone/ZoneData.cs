using UnityEngine;

namespace WheelOfFortune.Zone
{
    [CreateAssetMenu(menuName = "WheelOfFortune/Zone/ZoneData")]
    public class ZoneData : ScriptableObject
    {
        public int CurrentZone;
        public ZoneType CurrentZoneType;
    }
}