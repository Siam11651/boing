using Godot;
using System;

public partial class EditorGrid : Node2D
{
	private const int ROW_COUNT = 25;
	private const int COLUMN_COUNT = 25;
	private const float BRICK_WIDTH = 30.0f;
	private const float BRICK_HEIGHT = 15.0f;
	private const float GAP = 1.0f;
	private PackedScene mEditorBrickScene;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mEditorBrickScene = GD.Load<PackedScene>("prefabs/editor-brick.tscn");
		float posY = BRICK_HEIGHT / 2.0f;

		for(int i = 0; i < ROW_COUNT; ++i)
		{
			float posX = BRICK_WIDTH / 2.0f;

			for(int j = 0; j < COLUMN_COUNT; ++j)
			{
				Area2D newEditorBrick = mEditorBrickScene.Instantiate<Area2D>();
				newEditorBrick.Position = new Vector2(posX, posY);

				AddChild(newEditorBrick);

				posX += BRICK_WIDTH + GAP;
			}

			posY += BRICK_HEIGHT + GAP;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
