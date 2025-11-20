using Godot;
namespace CrossDimensions.Environment.Puzzles;
public partial class SwitchButton : Node
{
    public bool SwitchPressed { get; set; } = false;
    private void OnArea2DBodyEntered(PhysicsBody2D body)
    {
        SwitchPressed = true;
        SwitchDoor door = GetParentOrNull<SwitchDoor>();
        if (door != null)
        {
            door.Activate();
        }
    }
    private void OnArea2DBodyExited(PhysicsBody2D body)
    {
        SwitchPressed = false;
        SwitchDoor door = GetParentOrNull<SwitchDoor>();
        if (door != null)
        {
            door.Activate();
        }
    }
}