using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityPlayground.Entities
{
    public class Human : Entity
    {
        // Animations (abstract this?)
        private float _timer;
        private int _msPerFrame;
        private bool _leftToRight = true;
        private Rectangle[] _forwardSprites;
        private byte _animationIndex;

        public Human(Vector2 position) : base(position) { }

        public override void LoadContent(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>("charaset");
            
            _timer = 0;
            _msPerFrame = 250;
            _forwardSprites = new[] { new Rectangle(0, 128, 48, 64), new Rectangle(48, 128, 48, 64), new Rectangle(96, 128, 48, 64)};
            _animationIndex = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _forwardSprites[_animationIndex], Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (_timer > _msPerFrame)
            {
                if (_animationIndex == 0)
                {
                    _leftToRight = true;
                }
                else if (_animationIndex == 2)
                {
                    _leftToRight = false;
                }

                _animationIndex = (byte)(_leftToRight ? _animationIndex + 1 : _animationIndex - 1);
                _timer = 0;
            }
            else
            {
                _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}
