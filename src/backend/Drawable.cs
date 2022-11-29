using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mechima
{
    public enum AnchorPoint { TopLeft, TopCenter, TopRight, CenterLeft, Center, CenterRight, BottomLeft, BottomCenter, BottomRight}
    

    /*
     * Curse ye, all who enter here
     */
    public abstract class Drawable
    {
        public Texture2D Texture { get; private set; }
        protected Color Color { get; set; }

        public Vector2 Scale { get; set; } = Vector2.One;

        protected float Rotation { get; set; }
        protected bool IsDrawn { get; set; } = false;
        public bool IsAnimated { get; set; }
        public AnimData AnimData { get; set; }
        public int animSpeed { get; set; }
        private double animTimer { get; set; }

        //true if the animation cell intersects with the camera anywhere on screen
        public bool IsOnScreen
            { get => (GameManager.MainCam.ScreenDimensions.Intersects(new Rectangle((int)this.ScreenPosition.X, (int)this.ScreenPosition.Y, (int)this.CellSize.X, (int)this.CellSize.Y))); }
        
        public Vector2 ScreenPosition { get; set; }



        //calculates the rectangular slice of the texture in current animation frame
        public Rectangle SpriteCell { get => AnimData != null ? AnimData.SpriteCell : this.Texture.Bounds; }
        public Vector2 CellSize { get; set; } //this value can change depending on the resolution of each spritesheet. Must be set externally



        public Drawable(Texture2D sprite=null)
        {
            DisplayManager.Drawables.Add(this);


            
            IsAnimated = false;
            Color = Color.White;
            
        }



        //Might have to refactor this if somehow the animation cycling is inefficient during draw calls.
        public void Draw(SpriteBatch sb)
        {
            if (!IsDrawn) return;
            
            sb.Draw(this.Texture, this.ScreenPosition, this.SpriteCell, this.Color, Rotation, this.SpriteCell.Size.ToVector2()/2, Scale, SpriteEffects.None, 0);
        }

        public void Update()
        {
            if (!IsDrawn) return;

            if (this.AnimData != null) AnimData.Animate();
        }



        public void SetSprite(string sprite, bool animated = false)
        {
            this.Texture = sprite != null ? DisplayManager.spriteMap[sprite] : DisplayManager.spriteMap["player_new"];
            this.IsAnimated = animated;

            if(animated)
                this.AnimData = DisplayManager.GetAnim(sprite);

            IsDrawn = true;
        }


        public virtual void DetermineScreenPosition()
        {
            System.Diagnostics.Debug.WriteLine("virtual function called in Drawable.cs. this really shouldn't be called.");
        }


    }

}
