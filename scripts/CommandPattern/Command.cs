using Godot;
using System;
using System.Threading.Tasks;


public partial class Command : Node
{
    [Signal]
    public delegate void CommandCompletedEventHandler();

    public String CommandName = "Command";    
    public CommandUnits Unit;

    // What the command does upon execution
    public virtual async Task<bool> execute()
    {
        return true;
    }

    // How to undo this command
    public virtual async Task<bool> undo()
    {
        return true;
    }
}
