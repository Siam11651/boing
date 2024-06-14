using Godot;
using System;
using System.Diagnostics;

public partial class PaddleNormal : Node2D
{
	enum BallStuckState
	{
		UNSTUCK, INIT, STUCK
	}

	[Export]
	private float mBallInitSpeed;
	private BallStuckState mBallStuckState = BallStuckState.INIT;
	private int mWindowWidth;
	private double mDeltaTime = 0.0;
	private AnimatedSprite2D mAnimatedSprite2D;
	private Node2D mBallInit;
	private RigidBody2D mBall;

	public void ReleaseBall()
	{
		if(mBallStuckState != BallStuckState.UNSTUCK)
		{
			mBallStuckState = BallStuckState.UNSTUCK;
			float velocityComponentY = -0.5f - (float)new Random().NextDouble() / 2.0f;
			float velocityComponentX = MathF.Sqrt(1.0f - velocityComponentY * velocityComponentY);
			mBall.Freeze = false;
			mBall.LinearVelocity = new Vector2(velocityComponentX, velocityComponentY) * mBallInitSpeed;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mWindowWidth = GetWindow().Size.X;
		mBall = GetParent().GetNode<RigidBody2D>("Ball");
		mBallInit = GetNode<Node2D>("BallInit");

		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("default");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		mDeltaTime = delta;
		Position = new Vector2(GetViewport().GetMousePosition().X - mWindowWidth / 2, Position.Y);

		if(mBallStuckState == BallStuckState.INIT)
		{
			mBall.Position = mBallInit.GlobalPosition;
		}
		else if(mBallStuckState == BallStuckState.STUCK)
		{

		}
	}

    public override void _Input(InputEvent @event)
    {
        if(@event.GetType() == typeof(InputEventMouseButton))
		{
			InputEventMouseButton mouseButtonEvent = (InputEventMouseButton)@event;

			if(!mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Left)
			{
				ReleaseBall();
			}
		}
    }
}
