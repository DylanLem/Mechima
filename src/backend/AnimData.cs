using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mechima
{
    public enum AnimationState
    {
        Default, Idle, Attack, MoveUp, MoveDown, MoveLeft, MoveRight, Dying, Hurt
    }

    
    public class AnimData
    {
        //index location on the sheet
        protected int TextureIndex { get; set; }

        //Name of animation and list of texture indices for frames
        protected Dictionary<AnimationState, List<int>> Animations { get; set; }

        //name and index of current frame in animation (the index corresponds to which frame it is on within the animation, not its spritesheet location)
        protected KeyValuePair<AnimationState, int> CurrentAnim
        {
            get { return _currentAnim; }
            set
            {
                _currentAnim = this.Animations.ContainsKey(value.Key) ?
                  value
                  : new KeyValuePair<AnimationState, int>(AnimationState.Default, 0);
            }
        }
        private KeyValuePair<AnimationState, int> _currentAnim { get; set; }

        public int animSpeed { get; set; }
        private double animTimer { get; set; }

        



        //Call this to proceed along to the next animation frame
        public void Animate()
        {
            animTimer += GameManager.lastTick;


            if (animTimer < animSpeed) return;
            animTimer = 0;

            
            TextureIndex = Animations[CurrentAnim.Key][CurrentAnim.Value];
            int nextIndex = CurrentAnim.Value + 1 == Animations[CurrentAnim.Key].Count ? 0 : CurrentAnim.Value + 1;


            CurrentAnim = new KeyValuePair<AnimationState, int>(CurrentAnim.Key, nextIndex);
        }



        public void AddAnimState(AnimationState state, List<int> cells)
        {
            if (Animations.ContainsKey(state)) System.Diagnostics.Debug.WriteLine("This animation already exists. Overwriting. anim: " + state);
            Animations[state] = cells;
        }
    }
}
