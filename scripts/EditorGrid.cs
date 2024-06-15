using Godot;
using System;

public partial class EditorGrid : Node2D
{
	private EditorBrick[,] mEditorBricks = new EditorBrick[Globals.ROW_COUNT, Globals.COLUMN_COUNT];
	private PackedScene mEditorBrickScene;
	public EditorBrick[,] EditorBricks
	{
		get
		{
			return mEditorBricks;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mEditorBrickScene = GD.Load<PackedScene>("prefabs/editor-brick.tscn");
		float posY = Globals.BRICK_HEIGHT / 2.0f;

		for(int i = 0; i < Globals.ROW_COUNT; ++i)
		{
			float posX = Globals.BRICK_WIDTH / 2.0f;

			for(int j = 0; j < Globals.COLUMN_COUNT; ++j)
			{
				Area2D newEditorBrick = mEditorBrickScene.Instantiate<Area2D>();
				newEditorBrick.Position = new Vector2(posX, posY);

				AddChild(newEditorBrick);

				mEditorBricks[i, j] = (EditorBrick)newEditorBrick;
				posX += Globals.BRICK_WIDTH + Globals.BRICK_GAP;
			}

			posY += Globals.BRICK_HEIGHT + Globals.BRICK_GAP;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
