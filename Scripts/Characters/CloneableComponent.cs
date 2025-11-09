using Godot;

namespace CrossDimensions.Characters;

public sealed partial class CloneableComponent : Node
{
    public Character Character => GetParent<Character>();

    public Character Original { get; set; }

    public Character Clone { get; set; }

    public Character Mirror => Original ?? Clone;

    public bool IsClone => Original is not null;

    [Export]
    public float SplitForce { get; set; } = 768f;

    /// <summary>
    /// Splits the character, creating a mirrored clone. The character can
    /// only be split if there is no existing clone.
    /// </summary>
    /// <returns>The cloned character, or null if split failed.</returns>
    public Character Split()
    {
        if (Mirror is not null)
        {
            // can't split if there is already a clone
            return null;
        }

        var clone = (Character)Character.Duplicate();

        var clonesComponent = clone
            .GetNode<CloneableComponent>("%CloneableComponent");
        clonesComponent.Original = Character;
        clone.Controller.XScale *= -1;

        Clone = clone;

        // set clone state to split state
        clone.MovementStateMachine.InitialState = clone
            .MovementStateMachine
            .GetNode<States.Characters.CharacterSplitState>("Split State");

        Character.GetParent().AddChild(clone);

        return clone;
    }

    /// <summary>
    /// Merges the clone back into the original character.
    /// </summary>
    public void Merge()
    {
        if (Mirror is null)
        {
            return;
        }

        if (IsClone)
        {
            Original.Cloneable.Clone = null;
            QueueFree();
        }
        else
        {
            Clone.QueueFree();
            Clone = null;
        }
    }
}
