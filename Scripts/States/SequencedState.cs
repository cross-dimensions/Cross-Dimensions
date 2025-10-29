using Godot;

namespace CrossDimensions.States;

/// <summary>
/// A state that can contain multiple child states that are processed in
/// sequence. This allows for more modular behavior compositions as well as
/// different states being written in GDScript.
/// </summary>
[GlobalClass]
public partial class SequencedState : State
{
    public override Node Context
    {
        get => base.Context;
        set
        {
            base.Context = value;
            foreach (var node in GetChildren())
            {
                if (node is State state)
                {
                    state.Context = value;
                }
                else
                {
                    node.Set("context", value);
                }
            }
        }
    }

    private TReturn CallScriptMethod<TReturn>(GodotObject script, string name, params Variant[] args)
        where TReturn : GodotObject
    {
        if (script.HasMethod(name))
        {
            var ret = script.Call(name, args);
            if (ret.AsGodotObject() is TReturn typedRet)
            {
                return typedRet;
            }
        }

        return null;
    }

    public override State Enter(State previousState)
    {
        foreach (var node in GetChildren())
        {
            State nextState;
            if (node is State state)
            {
                nextState = state.Enter(previousState);
            }
            else
            {
                nextState = CallScriptMethod<State>(node, "enter", previousState);
            }

            if (nextState is not null)
            {
                return nextState;
            }
        }

        return null;
    }

    public override void Exit(State nextState)
    {
        foreach (var node in GetChildren())
        {
            if (node is State state)
            {
                state.Exit(nextState);
            }
            else
            {
                CallScriptMethod<GodotObject>(node, "exit", nextState);
            }
        }
    }

    public override State Process(double delta)
    {
        foreach (var node in GetChildren())
        {
            State nextState;
            if (node is State state)
            {
                nextState = state.Process(delta);
            }
            else
            {
                nextState = CallScriptMethod<State>(node, "process", delta);
            }

            if (nextState is not null)
            {
                return nextState;
            }
        }

        return null;
    }

    public override State PhysicsProcess(double delta)
    {
        foreach (var node in GetChildren())
        {
            State nextState;
            if (node is State state)
            {
                nextState = state.PhysicsProcess(delta);
            }
            else
            {
                nextState = CallScriptMethod<State>(node, "physics_process", delta);
            }

            if (nextState is not null)
            {
                return nextState;
            }
        }

        return null;
    }

    public override State Input(InputEvent @event)
    {
        foreach (var node in GetChildren())
        {
            State nextState;
            if (node is State state)
            {
                nextState = state.Input(@event);
            }
            else
            {
                nextState = CallScriptMethod<State>(node, "input", @event);
            }

            if (nextState is not null)
            {
                return nextState;
            }
        }

        return null;
    }
}
