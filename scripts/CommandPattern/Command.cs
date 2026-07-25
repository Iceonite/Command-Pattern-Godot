using Godot;
using System;

public partial class Command : Node
{
    [Signal]
    public delegate void CommandCompletedEventHandler();

    String CommandName = "Command";    

    // What the command does upon execution
    private bool execute()
    {
        return true;
    }

    // How to undo this command
    private bool undo()
    {
        return true;
    }
}
