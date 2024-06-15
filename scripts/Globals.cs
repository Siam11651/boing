using Godot;

public partial class Globals
{
	public const int BRICK_COUNT = 17;
	public const int ROW_COUNT = 25;
	public const int COLUMN_COUNT = 25;
	public const float BRICK_WIDTH = 30.0f;
	public const float BRICK_HEIGHT = 15.0f;
	public const float BRICK_GAP = 1.0f;
	public static ImageTexture[] BrickImageTextures = new ImageTexture[BRICK_COUNT];
	public static int[,] BrickSelections;
}
