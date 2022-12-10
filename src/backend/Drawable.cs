using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Mechima
{
    
    

    /// <summary>
    /// Prototype class for ANY object that gets rendered to the screen in game.
    /// Base class for Entity
    /// </summary>
    public abstract class Drawable
    {
        
        public Texture2D Texture { get =>  _texture; set => SetSprite(value); }
        private Texture2D _texture { get; set; }


        public Color Color { get; set; }

        public Vector2 Scale { get; set; } = Vector2.One;

        public float Rotation { get; set; }
        protected bool IsDrawn { get; set; } = false;
       
        public AnimData AnimData { get; set; }


        //true if the animation cell intersects with the camera anywhere on screen. Not yet utilized
        public bool IsOnScreen
            { get => (GameManager.MainCam.ScreenDimensions.Intersects(new Rectangle((int)this.ScreenPosition.X, (int)this.ScreenPosition.Y, (int)this.CellSize.X, (int)this.CellSize.Y))); }
        
        public Vector2 ScreenPosition { get; set; }

        //calculates the rectangular slice of the texture in current animation frame
        public Rectangle SpriteCell { get => AnimData != null ? AnimData.SpriteCell : this.Texture.Bounds; }
        public Vector2 CellSize { get; set; } //this value can change depending on the resolution of each spritesheet. Must be set externally



        public Drawable(Texture2D sprite=null)
        {
            DisplayManager.AddSprite(this);
         
            
            Color = Color.White;
            
        }




        public void Draw(SpriteBatch sb)
        {
            if (!IsDrawn) return;
            
            sb.Draw(this.Texture, this.ScreenPosition, this.SpriteCell, this.Color, Rotation, this.SpriteCell.Size.ToVector2()/2, Scale, SpriteEffects.None, 0);
        }

        public virtual void UpdateSprite()
        {
            if (AnimData != null)
                AnimData.Animate();

            DetermineScreenPosition();
        }



        public void SetSprite(string sprite, bool animated = false)
        {
            this._texture = sprite != null ? DisplayManager.spriteMap[sprite] : DisplayManager.spriteMap["player_new"];
            

            
            this.AnimData = DisplayManager.GetAnim(sprite);

            IsDrawn = true;
        }

        //Use this overload to attach primitives that aren't loaded by the content manager.
        public void SetSprite(Texture2D sprite, bool animated = false)
        {
            this._texture = sprite;

            

            IsDrawn = true;
        }

        public virtual void DetermineScreenPosition() { }


        public void DeleteDrawable()
        {
            DisplayManager.PoppedDrawables.Add(this);
        }


    }

}
