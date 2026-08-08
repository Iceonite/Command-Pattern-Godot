using Godot;
using System;
using System.Threading.Tasks;

public partial class CommandSpin : Command
{

    private Sprite2D _Sprite;

    public override void _Ready()
    {
        CommandName = "Spin";
        _Sprite = GetNode<Sprite2D>("../Sprite");
        return;
    }

    public override async Task<bool> execute()
    {
        // Create a tween and spin 360
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty( _Sprite, "rotation_degrees", 360f, 0.5f);
        await ToSignal(tween, Tween.SignalName.Finished);
        GD.Print("Finished Tween");

        // Reset the position for future use
        _Sprite.RotationDegrees = 0f;   
        return true;
    }

    public override async Task<bool> undo()
    {
        // As we're undoing, start at 360 and spin to 0
        _Sprite.RotationDegrees = 360f;   
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty( _Sprite, "rotation_degrees", 0f, 0.5f);
        await ToSignal(tween, Tween.SignalName.Finished);
        GD.Print("Finished Tween");
        return true;
    }
}