using Godot;

namespace CrossDimensions.Entities;

public partial class Projectile : Node2D
{
    /// <summary>
    /// The direction the projectile is moving in. If set in the editor,
    /// it should be a normalized vector.
    /// </summary>
    [Export]
    public Vector2 Direction { get; set; }

    /// <summary>
    /// The speed of the projectile in pixels per second.
    /// </summary>
    [Export]
    public float Speed { get; set; }

    /// <summary>
    /// The hitbox associated with the projectile.
    /// </summary>
    [Export]
    public BoundingBoxes.Hitbox Hitbox { get; set; }

    /// <summary>
    /// A custom handler for handling the projectile. If not set, the
    /// projectile will be freed on hit.
    /// </summary>
    [Export]
    public Node ProjectileHitHandler { get; set; }

    /// <summary>
    /// A timer that determines the lifetime of the projectile. If null,
    /// the projectile will not expire. Note that the timer should have
    /// auto-start enabled in the editor.
    /// </summary>
    [Export]
    public Timer LifetimeTimer { get; set; }

    /// <summary>
    /// The character that owns this projectile, if any.
    /// </summary>
    public Characters.Character OwnerCharacter { get; set; }

    /// <summary>
    /// The weapon that owns this projectile, if any.
    /// </summary>
    public Items.Weapon OwnerWeapon { get; set; }

    public override void _Ready()
    {
        if (LifetimeTimer is not null)
        {
            LifetimeTimer.Timeout += () => QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction.Rotated(Rotation) * Speed * (float)delta;

        Hitbox.AreaEntered += OnHitboxAreaEntered;
    }

    public void OnHitboxAreaEntered(Area2D area)
    {
        if (ProjectileHitHandler is IProjectileHitHandlerComponent handler)
        {
            handler.OnProjectileHit(this);
        }
        else if (ProjectileHitHandler.HasMethod("on_projectile_hit"))
        {
            ProjectileHitHandler.Call("on_projectile_hit", this);
        }
        else
        {
            QueueFree();
        }
    }
}
