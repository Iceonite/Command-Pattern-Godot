using Godot;
using System;
using System.Collections.Generic;

// Class to handle for a specific thing, in this case "unit"
// (So for a player, CommandPlayer etc)
public partial class CommandUnits : Node2D
{
	[Signal]
	public delegate void CommandsChangeEventHandler();

	private List<Command> _commandQueue = new List<Command>();
	private List<Command> _undoList = new List<Command>();
	private bool _awaitingExecution = false;

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("Spin"))
		{
			GD.Print("Queue Spin");
			Command command = new CommandSpin();
			AddCommand(command);
		}

		if (@event.IsActionPressed("Unspin"))
		{
			GD.Print("Undo Last Command")
			UndoLastCommand();
		}
	}

	// Adds command to the commandQueue, and executes it
	private void AddCommand(Command command)
	{
		_commandQueue.Add(command);
		AddChild(command); 
		command.Unit = this;
		ExecuteNextCommand();
		return;
	}

	// Recursive function to execute all commands in the list
	private async void ExecuteNextCommand()
	{
		// If you want to use this, this is set up
		EmitSignal(SignalName.CommandsChange);

		if (_awaitingExecution == true || _commandQueue.Count == 0)
		{
			return;
		}

		// Execute command
		_awaitingExecution = true;
		Command command = _commandQueue[0];
		await command.execute();

		// Clean up list and status
		_undoList.Insert(0, _commandQueue[0]);
		_commandQueue.RemoveAt(0);
		_awaitingExecution = false;

		// Recursively call so all commands are executed
		ExecuteNextCommand();
		return;
	}

	// Undo the last command, not recursive
	private async void UndoLastCommand()
	{
		if (_awaitingExecution == true || _undoList.Count == 0)
		{
			return;
		}

		// Queue it up and execute
		_awaitingExecution = true;
		Command command = _undoList[0];
		await command.undo();

		// Clean up and execute any commands not yet executed
		_undoList.RemoveAt(0);
		_awaitingExecution = false;
		ExecuteNextCommand();

		return;
	}
}