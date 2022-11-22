using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mechima
{

    /*
     * Curse ye, all who enter here
     */
    public abstract class Drawable
    {
        public Texture2D Texture { get; private set; }
        protected Color Color { get; set; }

        protected float Rotation { get; set; }
        protected bool Enabled { get; set; }
        public bool Animated { get; set; }
        public AnimData AnimData { get; set; }
        public int animSpeed { get; set; }
        private double animTimer { get; set; }

        //true if the animation cell intersects with the camera anywhere on screen
        public bool IsOnScreen
            { get => (GameManager.MainCam.ScreenDimensions.Intersects(new Rectangle((int)this.ScreenPosition.X, (int)this.ScreenPosition.Y, (int)this.CellSize.X, (int)this.CellSize.Y))); }
        
        public Vector2 ScreenPosition { get; set; }

        //Name of animation and list of texture indices for frames
        protected Dictionary<AnimationState, List<int>> Animations { get; set; }

        //name and index of current frame in animation (the index corresponds to which frame it is on within the animation, not its spritesheet location)
        protected KeyValuePair<AnimationState, int> CurrentAnim { 
            get {return _currentAnim; } 
            set { _currentAnim = this.Animations.ContainsKey(value.Key) ? 
                    value 
                    : new KeyValuePair<AnimationState,int> (AnimationState.Default, 0); 
                }
        } 

        private KeyValuePair<AnimationState, int> _currentAnim { get; set; }

        //index location on the sheet
        protected int TextureIndex { get; set; }

        //calculates the rectangular slice of the texture in current animation frame
        protected Rectangle SpriteCell { get => new Rectangle((int)CellSize.X * (TextureIndex % (int)(Texture.Width / CellSize.X)), (int)CellSize.Y * (int)(CellSize.X * TextureIndex / Texture.Width), (int)CellSize.X, (int)CellSize.Y); }
        public Vector2 CellSize { get; set; } //this value can change depending on the resolution of each spritesheet. Must be set externally



        public Drawable(Texture2D sprite=null)
        {
            DisplayManager.Drawables.Add(this);
            animSpeed = 85;
            animTimer = 0;

            this.Texture = sprite != null? sprite : DisplayManager.spriteMap["player_new"];

            //Defaults to single texture sheet with no animation
            Animations = new Dictionary<AnimationState, List<int>>
            {
                { AnimationState.Default, new List<int>(){0,1,2,3} }
            };

            CellSize = new Vector2(Texture.Width, Texture.Height); 
            CurrentAnim =  new KeyValuePair<AnimationState, int> (AnimationState.Default, 0);
            Animated = false;
            Enabled = true;
            Color = Color.White;
            
        }



        //Might have to refactor this if somehow the animation cycling is inefficient during draw calls.
        public void Draw(SpriteBatch sb)
        {
            if (!Enabled) return;
            
            sb.Draw(this.Texture, this.ScreenPosition, this.SpriteCell, this.Color, Rotation, this.SpriteCell.Size.ToVector2()/2, new Vector2(2,2), SpriteEffects.None, 0);
        }

        public void Update()
        {
            if (!Enabled) return;

            if (this.AnimData != null) AnimData.Animate();
        }
        //Call this to proceed along to the next animation frame
        public void Animate(GameTime gameTime)
        {
            if (!Enabled || !Animated) return;

            animTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (animTimer < animSpeed) return;

            animTimer = 0;

            TextureIndex = Animations[CurrentAnim.Key][CurrentAnim.Value];
            int nextIndex = CurrentAnim.Value + 1 == Animations[CurrentAnim.Key].Count ? 0 : CurrentAnim.Value + 1;

            CurrentAnim = new KeyValuePair<AnimationState, int>(CurrentAnim.Key, nextIndex);
        }


        public void AddAnimState(AnimationState state, List<int> cells)
        {
            if(Animations.ContainsKey(state))   System.Diagnostics.Debug.WriteLine("This animation already exists. Overwriting. anim: " + state);
            Animations[state] = cells;
        }

        public void SetSprite(string spriteName)
        {

        }


        public virtual void DetermineScreenPosition()
        {
            System.Diagnostics.Debug.WriteLine("virtual function called in Drawable.cs. this really shouldn't be called.");
        }

    }

}
