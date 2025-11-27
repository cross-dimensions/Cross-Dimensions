using Godot;

namespace CrossDimensions.States.Weapons.Behaviors;

public partial class FireProjectileBehavior : WeaponState
{
    [Export]
    public PackedScene ProjectileScene { get; set; }

    public override State Enter(State previousState)
    {
        var projectile = ProjectileScene.Instantiate<Entities.Projectile>();
        projectile.OwnerCharacter = Weapon.OwnerCharacter;
        projectile.GlobalPosition = projectile.OwnerCharacter.GlobalPosition;
        projectile.Rotation = Weapon.Target.Angle();

        // TODO: use a World class to add the projectile to the scene
        projectile.OwnerCharacter.GetParent().AddChild(projectile);

        return null;
    }
}
