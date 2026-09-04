using System.Collections.Generic;
using WheelOfFortune.Config;

namespace WheelOfFortune.Spin
{
    public interface IWheelPopulator
    {
        List<SliceData> Populate(WheelVisualConfig config);
    }
}