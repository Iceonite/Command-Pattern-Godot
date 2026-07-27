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
			GD.Print("EHY");
			Command command = new CommandSpin();
			AddCommand(command);
		}
	}

	private void AddCommand(Command command)
	{
		_commandQueue.Add(command);
		AddChild(command); 
		command.Unit = this;
		ExecuteNextCommand();
		return;
	}

	private async void ExecuteNextCommand()
	{
		EmitSignal(SignalName.CommandsChange);

		if (_awaitingExecution == true || _commandQueue.Count == 0)
		{
			return;
		}

		_awaitingExecution = true;

		Command command = _commandQueue[0];

		await command.execute();

		_undoList.Insert(0, _commandQueue[0]);
		_commandQueue.RemoveAt(0);

		_awaitingExecution = false;

		ExecuteNextCommand();

		return;
	}

	private void UndoLastCommand()
	{
		return;
	}

}
