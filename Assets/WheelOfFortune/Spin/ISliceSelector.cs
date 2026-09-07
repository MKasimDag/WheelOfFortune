using System.Collections.Generic;
using WheelOfFortune.Config;

namespace WheelOfFortune.Spin
{
    public interface ISliceSelector
    {
        SpinResult Select(IReadOnlyList<SliceData> slices, int zone);
    }
}