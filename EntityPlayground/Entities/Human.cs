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

        private float _timer;

        private Random rng = new Random();

        private Vector2 _movement;
        private Vector2 _velocity;
        private Rectangle _localBounds;

        // Physics constants
        private const float _maxMoveSpeed = 100.0f;

        public Human(World world, Vector2 position) : base(world, position) { }

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

            // Find local bounds within the texture size
            int width = (int)(_downAnimation.FrameWidth * 0.6);
            int left = (_downAnimation.FrameWidth - width) / 2;
            int height = (int)(_downAnimation.FrameHeight * 0.8);
            int top = _downAnimation.FrameHeight - height;
            _localBounds = new Rectangle(left, top, width, height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _animationPlayer.Draw(gameTime, spriteBatch, _position);
        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_timer > 5000)
            {
                MoveInRandomDirection();
                _velocity = new Vector2(0, 0);
                _timer = 0;
            }

            Animation animation;
            if (_velocity.X > 0) { animation = _rightAnimation; }
            else if (_velocity.X < 0) { animation = _leftAnimation; }
            else if (_velocity.Y < 0) { animation = _upAnimation; }
            else { animation = _downAnimation; } // TODO: Add idle animation, for now use "down" animation
            _animationPlayer.PlayAnimation(animation);

            ApplyPhysics(gameTime);
        }

        private void MoveInRandomDirection()
        {
            var random = rng.Next(0, 4);
            var direction = (EntityDirection)random;

            switch (direction)
            {
                case EntityDirection.Up:
                    _movement.X = 0;
                    _movement.Y = -1;
                    break;
                case EntityDirection.Right:
                    _movement.X = 1;
                    _movement.Y = 0;
                    break;
                case EntityDirection.Down:
                    _movement.X = 0;
                    _movement.Y = 1;
                    break;
                case EntityDirection.Left:
                    _movement.X = -1;
                    _movement.Y = 0;
                    break;
            }
        }

        public override void ApplyPhysics(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _velocity.X += _movement.X * _maxMoveSpeed;
            _velocity.Y += _movement.Y * _maxMoveSpeed;
            _velocity.X = MathHelper.Clamp(_velocity.X, -_maxMoveSpeed, _maxMoveSpeed);
            _velocity.Y = MathHelper.Clamp(_velocity.Y, -_maxMoveSpeed, _maxMoveSpeed);

            var newPosition = _position + _velocity * elapsedTime;
            newPosition = UpdatePositionForCollisions(newPosition);

            _position = newPosition;
        }

        private Vector2 UpdatePositionForCollisions(Vector2 potentialPosition) 
        {
            var newBounds = new Rectangle(
                (int)potentialPosition.X + _localBounds.Left,
                (int)potentialPosition.Y + _localBounds.Top,
                _localBounds.Width,
                _localBounds.Height);

            if (!_world.Bounds.Contains(newBounds))
            {
                if (newBounds.Left < _world.Bounds.Left)
                {
                    potentialPosition.X = _world.Bounds.Left - _localBounds.Left;
                }
                if (newBounds.Right >= _world.Bounds.Right)
                {
                    potentialPosition.X = _world.Bounds.Right - _localBounds.Right;
                }
                if (potentialPosition.Y <= _world.Bounds.Top)
                {
                    potentialPosition.Y = _world.Bounds.Top - _localBounds.Top;
                }
                if (newBounds.Bottom >= _world.Bounds.Bottom)
                {
                    potentialPosition.Y = _world.Bounds.Bottom - _localBounds.Bottom;
                }
            }

            return potentialPosition;
        }
    }
}
