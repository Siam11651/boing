using Godot;
using System;

public partial class Game : Node2D
{
	private const float BRICK_GAP = 0.1f;
	private const float LEVEL_WIDTH = Globals.BRICK_WIDTH * Globals.COLUMN_COUNT + BRICK_GAP * (Globals.COLUMN_COUNT - 1);
	private const float START_X = (-LEVEL_WIDTH + Globals.BRICK_WIDTH) / 2.0f;
	private const float START_Y = 50.0f;
	private PackedScene mBrickScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mBrickScene = GD.Load<PackedScene>("prefabs/game-brick.tscn");
		int[,] brickSelections = Globals.BrickSelections;
		float posY = START_Y;

		for(int i = 0; i < Globals.ROW_COUNT; ++i)
		{
			float posX = START_X + GetWindow().Size.X / 2.0f;

			for(int j = 0; j < Globals.COLUMN_COUNT; ++j)
			{
				if(brickSelections[i, j] != -1)
				{
					StaticBody2D newBrick = mBrickScene.Instantiate<StaticBody2D>();
					newBrick.Position = new Vector2(posX, posY);
					Sprite2D sprite2D = newBrick.GetNode<Sprite2D>("Sprite2D");
					sprite2D.Texture = Globals.BrickImageTextures[brickSelections[i, j]];

					AddChild(newBrick);
				}

				posX += Globals.BRICK_WIDTH + BRICK_GAP;
			}

			posY += Globals.BRICK_HEIGHT + BRICK_GAP;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
