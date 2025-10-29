using Godot;

namespace CrossDimensions.States.Characters;

public partial class CharacterIdleState : CharacterState
{
    [Export]
    public State MoveState { get; set; }

    public override State Enter(State previousState)
    {
        if (CharacterContext.Controller.IsMoving)
        {
            return MoveState;
        }

        return null;
    }

    public override State Process(double delta)
    {
        if (CharacterContext.Controller.IsMoving)
        {
            return MoveState;
        }

        return null;
    }

    public override State PhysicsProcess(double delta)
    {
        // get the default gravity from project settings
        Vector2 gravity = ProjectSettings
            .GetSetting("physics/2d/default_gravity_vector")
            .AsVector2();
        gravity *= ProjectSettings
            .GetSetting("physics/2d/default_gravity")
            .AsSingle();

        // apply gravity to the character
        CharacterContext.Velocity += gravity * (float)delta;
        CharacterContext.MoveAndSlide();
        return null;
    }
}
