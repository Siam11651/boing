using Godot;
using System;

public partial class Walls : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StaticBody2D wallTop = GetNode<StaticBody2D>("WallTop");
		StaticBody2D wallLeft = GetNode<StaticBody2D>("WallLeft");
		StaticBody2D wallRight = GetNode<StaticBody2D>("WallRight");
		int halfWindowWidth = GetWindow().Size.X / 2;
		wallLeft.Position = new Vector2(-halfWindowWidth, wallLeft.Position.Y);
		wallRight.Position = new Vector2(halfWindowWidth, wallLeft.Position.Y);	
		wallTop.Position = new Vector2(wallTop.Position.X, -GetWindow().Size.Y / 2);
		Sprite2D wallSprite = wallLeft.GetNode<Sprite2D>("Sprite2D");
		float heightRatio = (float)GetWindow().Size.Y / wallSprite.Texture.GetHeight();
		float widthRatio = (float)GetWindow().Size.X / wallSprite.Texture.GetHeight();
		wallLeft.Scale = new Vector2(wallLeft.Scale.X, wallLeft.Scale.Y * heightRatio);
		wallRight.Scale = new Vector2(wallRight.Scale.X, wallRight.Scale.Y * heightRatio);
		wallTop.Scale = new Vector2(wallTop.Scale.X * widthRatio, wallTop.Scale.Y);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
