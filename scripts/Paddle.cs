using Godot;
using System;

public partial class Paddle : Area2D
{
	enum BallStuckState
	{
		UNSTUCK, INIT, STUCK
	}

	private const float BALL_INIT_SPEED = 250.0f;
	private BallStuckState mBallStuckState = BallStuckState.INIT;
	private int mWindowWidth;
	private float mPaddleLimit;

	private double mDeltaTime = 0.0;
	private AnimatedSprite2D mAnimatedSprite2D;
	private Node2D mBallInit;
	private RigidBody2D mBall;

	public void ReleaseBall()
	{
		if(mBallStuckState != BallStuckState.UNSTUCK)
		{
			Random random = new Random();
			float directionMultiplier = 0.0f;

			if(random.NextDouble() >= 0.5f)
			{
				directionMultiplier = 1.0f;
			}

			mBallStuckState = BallStuckState.UNSTUCK;
			float velocityComponentY = -0.5f - (float)random.NextDouble() / 2.0f;
			float velocityComponentX = MathF.Sqrt(1.0f - velocityComponentY * velocityComponentY) * (1.0f - 2.0f * directionMultiplier);
			mBall.Freeze = false;
			mBall.LinearVelocity = new Vector2(velocityComponentX, velocityComponentY) * BALL_INIT_SPEED;
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mWindowWidth = GetWindow().Size.X;
		mBall = GetParent().GetNode<RigidBody2D>("Ball");
		mBallInit = GetNode<Node2D>("BallInit");
		AnimatedSprite2D paddleSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		Vector2 paddleSize = paddleSprite.SpriteFrames.GetFrameTexture("default", 0).GetSize();
		float ballRadius = mBall.GetNode<Sprite2D>("Sprite2D").Texture.GetWidth() / 2.0f;
		mPaddleLimit = mWindowWidth / 2.0f - GetNode<Sprite2D>("../Walls/WallLeft/Sprite2D").Texture.GetWidth() - paddleSize.X / 2.0f - 1.0f;

		paddleSprite.Play("default");

		BodyEntered += (Node2D body) =>
		{
			if(mBallStuckState == BallStuckState.UNSTUCK)
			{
				float distDiffX = body.Position.X - Position.X;
				float componentX = distDiffX * 2.0f / (paddleSize.X + ballRadius);

				if(componentX > 0.9f)
				{
					componentX = 0.9f;
				}

				float componentY = MathF.Sqrt(1.0f - componentX * componentX);

				if(body.Position.Y < Position.Y)
				{
					componentY *= -1.0f;
				}

				mBall.LinearVelocity = new Vector2(componentX, componentY) * BALL_INIT_SPEED;
			}
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		mDeltaTime = delta;
		float newPositionX = Math.Clamp(GetViewport().GetMousePosition().X - mWindowWidth / 2.0f, -mPaddleLimit, mPaddleLimit);
		Position = new Vector2(newPositionX + mWindowWidth / 2.0f, Position.Y);

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
