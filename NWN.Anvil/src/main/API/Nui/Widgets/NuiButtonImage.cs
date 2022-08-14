using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A clickable button with an image as the label.
  /// </summary>
  public sealed class NuiButtonImage : NuiWidget
  {
    [JsonConstructor]
    public NuiButtonImage(NuiProperty<string> resRef)
    {
      ResRef = resRef;
    }

    [JsonPropertyName("label")]
    public NuiProperty<string> ResRef { get; set; }

    public override string Type => "button_image";
  }
}
