using Godot;
using CrossDimensions.Characters;

namespace CrossDimensions.States.Characters;

public partial class CharacterState : State
{
    public Character CharacterContext { get; private set; }

    public override Node Context
    {
        get => base.Context;
        set
        {
            base.Context = value;
            CharacterContext = value as Character;
        }
    }
}
