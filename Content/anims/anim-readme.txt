Each spritesheet requires a corresponding .anim file in order for the game to parse it

FORMAT:
	line 1: (x,y) cell dimensions for each frame of animation (true pixel size without scaling).
	line 2: floating point x representing animation speed in seconds/frame
	line 3-n: {AnimationState: [List of cells in animation]}



EXAMPLE:
	"knight animation"
	
32,32 (sprite is considered 32,32 pixels. x doesnt need to equal y)
0.4 (animates every 0.4 seconds)
Default:0 (the rest are potential animations)
MoveLeft:4,5
MoveRight:2,3


CURRENT ANIMATION STATES PARSED IN GAME:
	Default, Idle, Attack, MoveUp, MoveDown, MoveLeft, MoveRight, Dying, Hurt, Charging, Discharging