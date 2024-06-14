using Godot;
using System;
using System.Diagnostics;
using System.Drawing;

public partial class EditorBrick : Area2D
{
	private Node2D mHoverLines;
	private float mLeft;
	private float mRight;
	private float mTop;
	private float mBottom;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mHoverLines = GetNode<Node2D>("HoverLines");
		RectangleShape2D collisionRect = (RectangleShape2D)GetNode<CollisionShape2D>("CollisionShape2D").Shape;
		mLeft = GlobalPosition.X - collisionRect.Size.X / 2.0f;
		mRight = GlobalPosition.X + collisionRect.Size.X / 2.0f;
		mTop = GlobalPosition.Y - collisionRect.Size.Y / 2.0f;
		mBottom = GlobalPosition.Y + collisionRect.Size.Y / 2.0f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public override void _Input(InputEvent @event)
    {
        if(@event.GetType() == typeof(InputEventMouseMotion))
		{
			InputEventMouseMotion mouseMotionEvent = (InputEventMouseMotion)@event;
			Vector2 mousePos = mouseMotionEvent.Position;

			if(mLeft <= mousePos.X && mousePos.X <= mRight && mTop <= mousePos.Y && mousePos.Y <= mBottom)
			{
				mHoverLines.Visible = true;
			}
			else
			{
				mHoverLines.Visible = false;
			}
		}
    }
}
