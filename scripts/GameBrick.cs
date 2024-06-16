using Godot;

public partial class GameBrick : StaticBody2D
{
	private Area2D mArea2D;
	private Game mGame;
	private GamePanel mGamePanel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mGame = GetNode<Game>("../../..");
		mGamePanel = mGame.GetNode<GamePanel>("Control/Panel");
		mArea2D = GetNode<Area2D>("Area2D");
		mArea2D.BodyEntered += (Node2D body) =>
		{
			if(body.Name == "Ball")
			{
				QueueFree();

				--mGame.TotalBricks;

				if(mGame.TotalBricks == 0)
				{
					mGamePanel.Done();
				}
			}
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
