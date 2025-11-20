using Godot;

namespace CrossDimensions.BoundingBoxes;

public partial class Hurtbox : BoundingBox
{
    /// <summary>
    /// The health component of the entity that owns this hurtbox.
    /// </summary>
    [Export]
    public Components.HealthComponent HealthComponent { get; set; }

    /// <summary>
    /// The weapon that owns this hurtbox, if any.
    /// </summary>
    [Export]
    public Items.Weapon OwnerWeapon { get; set; }

    /// <summary>
    /// Signal emitted when this hurtbox is hit by a hitbox.
    /// </summary>
    [Signal]
    public delegate void HurtboxHitEventHandler(Hitbox hitbox);

    /// <summary>
    /// Applies damage to the owning entity based on the given hitbox.
    /// </summary>
    public void Hit(Hitbox hitbox)
    {
        if (HealthComponent != null && hitbox.DamageComponent != null)
        {
            int damage = hitbox.DamageComponent.DamageAmount;
            HealthComponent.CurrentHealth -= damage;
        }
    }
}
