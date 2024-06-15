using Godot;
using System;
using System.Diagnostics;

public partial class EditorControl : Control
{
	private const int BRICK_COUNT = 17;
	private Button[] mBrickSelectionButtons = new Button[BRICK_COUNT];
	private ImageTexture[] mBrickImageTextures = new ImageTexture[BRICK_COUNT];
	private ImageTexture mBrickNoneImageTexture;
	private int mSelectedBrick = -1;
	private bool mDrawing = true;
	private GridContainer mBrickSelectionContainer;
	private Button mDrawButton;
	private Button mEraseButton;
	private Button mCloseButton;

	public int SelectedBrick
	{
		get
		{
			return mSelectedBrick;
		}
	}

	public bool Drawing
	{
		get
		{
			return mDrawing;
		}
	}

	public ImageTexture BrickNoneTexture
	{
		get
		{
			return mBrickNoneImageTexture;
		}
	}

	public ImageTexture GetBrickTexture(int index)
	{
		return mBrickImageTextures[index];
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mBrickNoneImageTexture = ImageTexture.CreateFromImage(Image.LoadFromFile("artworks/bricks/brick-none.png"));
		mDrawButton = GetNode<Button>("VBoxContainer/HBoxContainer2/Draw");
		mEraseButton = GetNode<Button>("VBoxContainer/HBoxContainer2/Erase");
		mBrickSelectionContainer = GetNode<GridContainer>("VBoxContainer/ScrollContainer/MarginContainer/GridContainer");
		mCloseButton = GetNode<Button>("Close");
		mDrawButton.Pressed += () =>
		{
			mDrawing = true;

			for(int i = 0; i < BRICK_COUNT; ++i)
			{
				mBrickSelectionButtons[i].Disabled = false;
			}

			mDrawButton.Disabled = true;
			mEraseButton.Disabled = false;
		};
		mEraseButton.Pressed += () =>
		{
			mDrawing = false;

			for(int i = 0; i < BRICK_COUNT; ++i)
			{
				mBrickSelectionButtons[i].Disabled = true;
			}

			mSelectedBrick = -1;
			mDrawButton.Disabled = false;
			mEraseButton.Disabled = true;
		};
		mCloseButton.Pressed += () =>
		{
			GetTree().ChangeSceneToFile("scenes/home.tscn");
		};

		for(int i = 0; i < BRICK_COUNT; ++i)
		{
			mBrickImageTextures[i] = ImageTexture.CreateFromImage(Image.LoadFromFile("artworks/bricks/brick-" + i + ".png"));
            mBrickSelectionButtons[i] = new Button
            {
                Icon = mBrickImageTextures[i]
            };
			int index = i;
			mBrickSelectionButtons[i].Pressed += () =>
			{				
				if(mSelectedBrick != -1)
				{
					mBrickSelectionButtons[mSelectedBrick].Disabled = false;
				}

				mBrickSelectionButtons[index].Disabled = true;
				mSelectedBrick = index;
			};

			mBrickSelectionContainer.AddChild(mBrickSelectionButtons[i]);
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
