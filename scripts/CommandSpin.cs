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
        //helperFunction();
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty( _Sprite, "rotation_degrees", 360f, 0.5f);
        await ToSignal(tween, Tween.SignalName.Finished);
        GD.Print("Finished Tween");
        _Sprite.RotationDegrees = 0f;   
        return true;
    }


    // public async void helperFunction()
    // {
    //     Tween tween = GetTree().CreateTween();
    //     tween.TweenProperty( _Sprite, "rotation_degrees", 360f, 0.5f);
    //     await ToSignal(tween, Tween.SignalName.Finished);
    //     _Sprite.RotationDegrees = 0f;
    // }

    public override async Task<bool> undo()
    {
        return true;
    }





}
