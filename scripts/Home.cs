using Godot;
using System;

public partial class Home : Control
{
	private Button mCreateButton;
	private Button mExitButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mCreateButton = GetNode<Button>("VBoxContainer/Create");
		mExitButton = GetNode<Button>("VBoxContainer/Exit");
		mCreateButton.Pressed += () =>
		{
			GetTree().ChangeSceneToFile("scenes/editor.tscn");
		};
		mExitButton.Pressed += () =>
		{
			GetTree().Quit();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
