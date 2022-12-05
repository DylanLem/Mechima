using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    

    //Holds metadata attached to a given spritesheet wrt its various animations
    //Prone to rework
    public class AnimData: ICloneable
    {
        //index location on the sheet
        protected int TextureIndex { get; set; }

        //Name of animation and list of texture indices for frames
        public Dictionary<AnimationState, List<int>> Animations { get; set; } = new Dictionary<AnimationState, List<int>>();


        //name and index of current frame in animation (the index corresponds to which frame it is on within the animation, not its spritesheet location)
        public KeyValuePair<AnimationState, int> CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                _currentFrame = this.Animations.ContainsKey(value.Key) ?
                  value
                  : new KeyValuePair<AnimationState, int>(AnimationState.Default, 0);
            }
        } 
        private KeyValuePair<AnimationState, int> _currentFrame { get; set; }

        public float animSpeed { get; set; }
        public float defaultAnimSpeed { get; set; }
        public double animTimer { get; set; }

        public Vector2 CellSize { get; set; } //this value can change depending on the resolution of each spritesheet. Must be set externally

        public Vector2 TextureSize;

        public Rectangle SpriteCell { get => new Rectangle((int)CellSize.X * (TextureIndex % (int)(TextureSize.X / CellSize.X)), (int)CellSize.Y * (int)(CellSize.X * TextureIndex / TextureSize.X), (int)CellSize.X, (int)CellSize.Y); }
        




        //Call this to proceed along to the next animation frame
        public void Animate()
        {
            animTimer += GameManager.lastTick;


            if (animTimer < animSpeed) return;
            animTimer = 0;

            
            TextureIndex = Animations[CurrentFrame.Key][CurrentFrame.Value];
            int nextIndex = CurrentFrame.Value + 1 == Animations[CurrentFrame.Key].Count ? 0 : CurrentFrame.Value + 1;


            CurrentFrame = new KeyValuePair<AnimationState, int>(CurrentFrame.Key, nextIndex);
        }



        public void AddAnimState(AnimationState state, List<int> cells)
        {
            if (Animations.ContainsKey(state)) System.Diagnostics.Debug.WriteLine("This animation already exists. Overwriting. anim: " + state);
            Animations[state] = cells;
        }

        public void SetAnim(AnimationState state)
        {
            if (!Animations.ContainsKey(state) || _currentFrame.Key == state)
                return;
            this.CurrentFrame = new KeyValuePair<AnimationState, int>(state,0);
        }

        public object Clone()
        {
            AnimData data = new AnimData();
            data.Animations = this.Animations;
            data.TextureSize = this.TextureSize;
            data.animSpeed = this.animSpeed;
            data.CellSize = this.CellSize;

            return data;
        }
    }
}
