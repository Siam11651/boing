using Godot;

public partial class EditorControl : Control
{
	private Button[] mBrickSelectionButtons = new Button[Globals.BRICK_COUNT];
	private ImageTexture mBrickNoneImageTexture;
	private int mSelectedBrick = -1;
	private bool mDrawing = true;
	private GridContainer mBrickSelectionContainer;
	private Button mDrawButton;
	private Button mEraseButton;
	private Button mPlayButton;
	private Button mCloseButton;
	private EditorBrick[,] mEditorBricks;
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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mBrickNoneImageTexture = ImageTexture.CreateFromImage(Image.LoadFromFile("artworks/bricks/brick-none.png"));
		mEditorBricks = GetNode<EditorGrid>("../EditorGrid").EditorBricks;
		mDrawButton = GetNode<Button>("VBoxContainer/HBoxContainer2/Draw");
		mEraseButton = GetNode<Button>("VBoxContainer/HBoxContainer2/Erase");
		mBrickSelectionContainer = GetNode<GridContainer>("VBoxContainer/ScrollContainer/MarginContainer/GridContainer");
		mPlayButton = GetNode<Button>("HBoxContainer/Play");
		mCloseButton = GetNode<Button>("HBoxContainer/Close");
		mDrawButton.Pressed += () =>
		{
			mDrawing = true;

			for(int i = 0; i < Globals.BRICK_COUNT; ++i)
			{
				mBrickSelectionButtons[i].Disabled = false;
			}

			mDrawButton.Disabled = true;
			mEraseButton.Disabled = false;
		};
		mEraseButton.Pressed += () =>
		{
			mDrawing = false;

			for(int i = 0; i < Globals.BRICK_COUNT; ++i)
			{
				mBrickSelectionButtons[i].Disabled = true;
			}

			mSelectedBrick = -1;
			mDrawButton.Disabled = false;
			mEraseButton.Disabled = true;
		};
		mPlayButton.Pressed += () =>
		{
			Globals.BrickSelections = new int[Globals.ROW_COUNT, Globals.COLUMN_COUNT];

			for(int i = 0; i < Globals.ROW_COUNT; ++i)
			{
				for(int j = 0;j < Globals.COLUMN_COUNT; ++j)
				{
					Globals.BrickSelections[i, j] = mEditorBricks[i, j].SelectedBrick;
				}
			}

			GetTree().ChangeSceneToFile("scenes/game.tscn");
		};
		mCloseButton.Pressed += () =>
		{
			GetTree().ChangeSceneToFile("scenes/home.tscn");
		};

		for(int i = 0; i < Globals.BRICK_COUNT; ++i)
		{
			Globals.BrickImageTextures[i] = ImageTexture.CreateFromImage(Image.LoadFromFile("artworks/bricks/brick-" + i + ".png"));
            mBrickSelectionButtons[i] = new Button
            {
                Icon = Globals.BrickImageTextures[i]
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
