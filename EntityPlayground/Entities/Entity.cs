using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityPlayground.Entities
{
    public abstract class Entity
    {
        protected Texture2D _texture;
        protected Vector2 _position;

        public Entity(Vector2 position) { _position = position; }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void LoadContent(ContentManager contentManager);
    }
}
