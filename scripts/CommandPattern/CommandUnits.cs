using Godot;
using System;

public partial class CommandUnits : Node2D
{
	[Signal]
	public delegate void CommandsChangeEventHandler();

	private List<Command> _commandQueue = new List<Command>();
	private List<Command> _undoList = new List<Command>();
	private bool _awaitingExecution = false;

	public override void _UnhandledInput(InputEvent @event)
	{
		return;
	}

	private void AddCommand(Command command)
	{
		return;
	}

	private void ExecuteNextCommand()
	{
		return;
	}

	private void UndoLastCommand()
	{
		return;
	}

}
