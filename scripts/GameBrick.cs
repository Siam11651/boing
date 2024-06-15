using Godot;

public partial class GameBrick : StaticBody2D
{
	private Area2D mArea2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mArea2D = GetNode<Area2D>("Area2D");
		mArea2D.BodyEntered += (Node2D body) =>
		{

		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
