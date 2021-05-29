using NWN.API;

namespace NWN.Services
{
  public sealed class DevastatingCriticalData
  {
    public NwItem Weapon { get; init; }

    public NwGameObject Target { get; init; }

    public int Damage { get; set; }

    public bool Bypass { get; set; }
  }
}
