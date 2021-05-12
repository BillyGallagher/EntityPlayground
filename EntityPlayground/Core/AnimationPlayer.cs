using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityPlayground.Core
{
    public class AnimationPlayer
    {
        private Animation _animation;
        private int _frameIndex;
        private float _timer;

        private bool _leftToRight = true;

        public AnimationPlayer() { }

        public void PlayAnimation(Animation animation)
        {
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
