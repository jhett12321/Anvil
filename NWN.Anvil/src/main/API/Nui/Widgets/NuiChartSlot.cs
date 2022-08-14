using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A chart element/data set for use in <see cref="NuiChart"/>.
  /// </summary>
  public sealed class NuiChartSlot
  {
    [JsonConstructor]
    public NuiChartSlot(NuiChartType chartType, NuiProperty<string> legend, NuiProperty<Color> color, NuiProperty<List<float>> data)
    {
      ChartType = chartType;
      Legend = legend;
      Color = color;
      Data = data;
    }

    [JsonPropertyName("type")]
    public NuiChartType ChartType { get; set; }

    [JsonPropertyName("color")]
    public NuiProperty<Color> Color { get; set; }

    [JsonPropertyName("data")]
    public NuiProperty<List<float>> Data { get; set; }

    [JsonPropertyName("legend")]
    public NuiProperty<string> Legend { get; set; }
  }
}
