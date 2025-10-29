using Godot;

namespace CrossDimensions.States;

public partial class State : Node
{
    public virtual Node Context { get; set; }

    public virtual State Enter(State previousState)
    {
        return null;
    }

    public virtual void Exit(State nextState)
    {

    }

    public virtual State Process(double delta)
    {
        return null;
    }

    public virtual State PhysicsProcess(double delta)
    {
        return null;
    }

    public virtual State Input(InputEvent @event)
    {
        return null;
    }
}
