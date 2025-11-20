using Godot;

namespace CrossDimensions.Components;

public partial class DamageComponent : Node
{
    [Export]
    public int DamageAmount { get; set; } = 10;
}
