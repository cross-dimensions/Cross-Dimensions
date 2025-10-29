using Godot;

namespace CrossDimensions.StateCharts;

public sealed class CharacterStateChart : BaseStateChart
{
    private CharacterStateChart(Node wrapped) : base(wrapped) { }

    public static class EventName
    {
        public static readonly StringName Move = "move";

        public static readonly StringName Idle = "idle";

        public static readonly StringName Jump = "jump";
    }

    public new static CharacterStateChart Of(Node node)
    {
        return new(node);
    }
}
