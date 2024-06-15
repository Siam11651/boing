using System;
using Godot;

public partial class GamePanel : Panel
{
	private bool mDone;
	private bool mPaused;
	private Node2D mLevel;
	private VBoxContainer mPauseMenu;
	private VBoxContainer mDoneMenu;
	private Button mResumeButton;
	private Button mRetryButton;
	private Button mMainMenuButtonPause;
	private Button mMainMenuButtonDone;
	private Label mLevelDoneLabel;

	public void Pause()
	{
		if(!mDone)
		{
			mPaused = true;
			GetTree().Paused = true;
			Visible = true;
			mPauseMenu.Visible = true;
			mDoneMenu.Visible = false;
		}
	}

	public void Resume()
	{
		if(!mDone)
		{
			mPaused = false;
			GetTree().Paused = false;
			Visible = false;
		}
	}

	public void Done(bool died = false)
	{
		mDone = true;
		GetTree().Paused = true;
		Visible = true;
		mPauseMenu.Visible = false;
		mDoneMenu.Visible = true;

		if(died)
		{
			mLevelDoneLabel.Text = "Ball Lost";
		}
		else
		{
			mLevelDoneLabel.Text = "Level Complete";
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mDone = false;
		mPaused = false;
		mLevel = GetNode<Node2D>("../../Level");
		mPauseMenu = GetNode<VBoxContainer>("PauseMenu");
		mDoneMenu = GetNode<VBoxContainer>("DoneMenu");
		mResumeButton = mPauseMenu.GetNode<Button>("Resume");
		mRetryButton = mDoneMenu.GetNode<Button>("Retry");
		mMainMenuButtonPause = mPauseMenu.GetNode<Button>("MainMenu");
		mMainMenuButtonDone = mDoneMenu.GetNode<Button>("MainMenu");
		mLevelDoneLabel = mDoneMenu.GetNode<Label>("Label");
		Action mainMenuButtonHandler = () =>
		{
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile("scenes/home.tscn");
		};
		mResumeButton.Pressed += () =>
		{
			Resume();
		};
		mRetryButton.Pressed += () =>
		{
			GetTree().Paused = false;
			GetTree().ReloadCurrentScene();
		};
		mMainMenuButtonPause.Pressed += mainMenuButtonHandler;
		mMainMenuButtonDone.Pressed += mainMenuButtonHandler;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventKey inputEventKey)
		{
			if(inputEventKey.Pressed && inputEventKey.Keycode == Key.Escape)
			{
				if(!mDone)
				{
					if(mPaused)
					{
						Resume();
					}
					else
					{
						Pause();
					}
				}
			}
		}
    }
}
