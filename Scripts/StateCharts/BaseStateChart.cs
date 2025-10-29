using Godot;

namespace CrossDimensions.StateCharts;

/// <summary>
/// This is a base class for specialized state chart wrappers. Note that this
/// class is not a node, but a pure C# class that extends the StateChart class,
/// which is a wrapper around the extension's StateChart node. Only inherit
/// from this class if you need to define a custom API for your state chart
/// that is specific to the entities using it.
/// </summary>
public abstract class BaseStateChart : GodotStateCharts.StateChart
{
    protected internal Node _wrapped;

    protected BaseStateChart(Node wrapped) : base(wrapped)
    {
        _wrapped = wrapped;
    }
}
