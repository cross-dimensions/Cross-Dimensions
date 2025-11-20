using Godot;

namespace CrossDimensions.BoundingBoxes;

public partial class Hitbox : BoundingBox
{
    [Export]
    public Components.DamageComponent DamageComponent { get; set; }

    public override void _Ready()
    {
        AreaEntered += OnHitboxAreaEntered;
        base._Ready();
    }

    public void OnHitboxAreaEntered(Area2D area)
    {
        if (area is Hurtbox hurtbox)
        {
            hurtbox.Hit(this);
        }
    }
}
