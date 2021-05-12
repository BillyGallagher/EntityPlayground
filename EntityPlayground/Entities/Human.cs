using EntityPlayground.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EntityPlayground.Entities
{
    public class Human : Entity
    {
        private AnimationPlayer _animationPlayer;
        private Animation _downAnimation;
        private Animation _upAnimation;
        private Animation _leftAnimation;
        private Animation _rightAnimation;

        private Animation[] _animationOrder;
        private int _animationIndex = 0;
        private float _timer;

        public Human(Vector2 position) : base(position) { }

        public override void LoadContent(ContentManager contentManager)
        {
            _animationPlayer = new AnimationPlayer();

            Color[] data;
            _texture = contentManager.Load<Texture2D>("charaset");

            var upTexture = new Texture2D(_texture.GraphicsDevice, 144, 64);
            data = new Color[144 * 64];
            _texture.GetData(0, new Rectangle(0, 0, 144, 64), data, 0, data.Length);
            upTexture.SetData(data);
            _upAnimation = new Animation(upTexture, 64, 48, 250, true);

            var rightTexture = new Texture2D(_texture.GraphicsDevice, 144, 64);
            data = new Color[144 * 64];
            _texture.GetData(0, new Rectangle(0, 64, 144, 64), data, 0, data.Length);
            rightTexture.SetData(data);
            _rightAnimation = new Animation(rightTexture, 64, 48, 250, true);

            var downTexture = new Texture2D(_texture.GraphicsDevice, 144, 64);
            data = new Color[144 * 64];
            _texture.GetData(0, new Rectangle(0, 128, 144, 64), data, 0, data.Length);
            downTexture.SetData(data);
            _downAnimation = new Animation(downTexture, 64, 48, 250, true);

            var leftTexture = new Texture2D(_texture.GraphicsDevice, 144, 64);
            data = new Color[144 * 64];
            _texture.GetData(0, new Rectangle(0, 192, 144, 64), data, 0, data.Length);
            leftTexture.SetData(data);
            _leftAnimation = new Animation(leftTexture, 64, 48, 250, true);

            _animationOrder = new[] { _upAnimation, _rightAnimation, _downAnimation, _leftAnimation };
            _animationPlayer.PlayAnimation(_animationOrder[_animationIndex]);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationPlayer.Draw(gameTime, spriteBatch, _position);
        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timer > 1000)
            {
                _animationIndex++;
                if (_animationIndex == _animationOrder.Length)
                {
                    _animationIndex = 0;
                }
                _animationPlayer.PlayAnimation(_animationOrder[_animationIndex]);
                _timer = 0;
            }
        }
    }
}
