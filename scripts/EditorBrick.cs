using Godot;
using System.Diagnostics;

public partial class EditorBrick : Area2D
{
	private Node2D mHoverLines;
	private float mLeft;
	private float mRight;
	private float mTop;
	private float mBottom;
	private Vector2 mMousePos;
	private Sprite2D mSprite;
	private EditorControl mEditorControlScript;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mEditorControlScript = (EditorControl)GetNode<Control>("../../Control");
		mSprite = GetNode<Sprite2D>("Sprite2D");
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
		if(mLeft <= mMousePos.X && mMousePos.X <= mRight && mTop <= mMousePos.Y && mMousePos.Y <= mBottom)
		{
			if(Input.IsMouseButtonPressed(MouseButton.Left))
			{
				if(mEditorControlScript.Drawing)
				{
					if(mEditorControlScript.SelectedBrick != -1)
					{
						mSprite.Texture = mEditorControlScript.GetBrickTexture(mEditorControlScript.SelectedBrick);
					}
				}
				else
				{
					mSprite.Texture = mEditorControlScript.BrickNoneTexture;
				}
			}
		}
	}

    public override void _Input(InputEvent @event)
    {
        if(@event.GetType() == typeof(InputEventMouseMotion))
		{
			InputEventMouseMotion mouseMotionEvent = (InputEventMouseMotion)@event;
			mMousePos = mouseMotionEvent.Position;

			if(mLeft <= mMousePos.X && mMousePos.X <= mRight && mTop <= mMousePos.Y && mMousePos.Y <= mBottom)
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
