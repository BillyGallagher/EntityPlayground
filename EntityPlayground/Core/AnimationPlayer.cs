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
        /// <summary>
        /// Return the origin of the texture at the bottom center of the frame
        /// </summary>
        public Vector2 Origin
        {
            get { return new Vector2(_animation.FrameWidth / 2.0f, _animation.FrameHeight); }
        }
        private Animation _animation;
        private int _frameIndex;
        private float _timer;

        private bool _leftToRight = true;

        public AnimationPlayer() { }

        public void PlayAnimation(Animation animation)
        {
            if (_animation == animation) { return; } // Don't restart animation if it's already playing
            _animation = animation;
            _frameIndex = 0;
            _timer = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            while (_timer > _animation.FrameTime)
            {
                _timer -= _animation.FrameTime;

                _leftToRight = _frameIndex == 0 ? true : false;
                _frameIndex = _leftToRight ? _frameIndex + 1 : _frameIndex - 1;
            }

            Rectangle source = new Rectangle(_frameIndex * _animation.FrameWidth, 0, _animation.FrameWidth, _animation.FrameHeight);
            spriteBatch.Draw(_animation.Texture, position, source, Color.White);
        }
    }
}
