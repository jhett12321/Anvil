using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Anvil.API
{
  /// <summary>
  /// A dynamic NUI element with style support.
  /// </summary>
  public abstract class NuiElement
  {
    /// <summary>
    /// The aspect ratio (x/y) for this element.
    /// </summary>
    [JsonPropertyName("aspect")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public float? Aspect { get; set; }

    /// <summary>
    /// Toggles if this element is active/interactable, or disabled/greyed out.
    /// </summary>
    [JsonPropertyName("enabled")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public NuiProperty<bool>? Enabled { get; set; }

    /// <summary>
    /// Style the foreground color of this widget.<br/>
    /// This is dependent on the widget in question and only supports solid/full colors right now (no texture skinning).<br/>
    /// For example, labels would style their text color; progress bars would style the bar.
    /// </summary>
    [JsonPropertyName("foreground_color")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public NuiProperty<Color>? ForegroundColor { get; set; }

    /// <summary>
    /// The height of this element, in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public float? Height { get; set; }

    /// <summary>
    /// A unique identifier for this element.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Id { get; set; }

    /// <summary>
    /// The margin on the widget. The margin is the spacing outside of the widget.
    /// </summary>
    [JsonPropertyName("margin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public float? Margin { get; set; }

    /// <summary>
    /// The padding on the widget. The padding is the spacing inside of the widget.
    /// </summary>
    [JsonPropertyName("padding")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public float? Padding { get; set; }

    /// <summary>
    /// A tooltip to show when hovering over this element.
    /// </summary>
    [JsonPropertyName("tooltip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public NuiProperty<string>? Tooltip { get; set; }

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public abstract string Type { get; }

    /// <summary>
    /// Toggles if this element should/should not be rendered. Invisible elements still take up layout space, and cannot be clicked through.
    /// </summary>
    [JsonPropertyName("visible")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public NuiProperty<bool>? Visible { get; set; }

    /// <summary>
    /// The width of this element, in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public float? Width { get; set; }

    [JsonPropertyName("draw_list")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<NuiDrawListItem>? DrawList { get; set; }

    [JsonPropertyName("draw_list_scissor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public NuiProperty<bool>? Scissor { get; set; }
  }
}
