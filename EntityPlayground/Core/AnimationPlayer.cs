using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityPlayground.Core
{
    // TODO: Fix only showing two of three frames
    public class AnimationPlayer
    {
        private Animation _animation;
        private int _frameIndex;
        private float _timer;

        private bool _leftToRight = true;

        public AnimationPlayer() { }

        public void PlayAnimation(Animation animation)
        {
            if (_animation == animation) { return; } // Don't restart animation if it's already playing
            _animation = animation;
            _frameIndex = 1;
            _timer = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            while (_timer > _animation.FrameTime)
            {
                _timer -= _animation.FrameTime;

                if (_frameIndex == 2)
                {
                    _leftToRight = false;
                }
                else if (_frameIndex == 0)
                {
                    _leftToRight = true;
                }

                if (_leftToRight)
                {
                    _frameIndex++;
                }
                else
                {
                    _frameIndex--;
                }
            }

            Rectangle source = new Rectangle(_frameIndex * _animation.FrameWidth, 0, _animation.FrameWidth, _animation.FrameHeight);
            spriteBatch.Draw(_animation.Texture, position, source, Color.White);
        }
    }
}
