using EntityPlayground.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityPlayground
{
    public class World
    {
        private ContentManager _contentManager;
        private List<Entity> _entities = new List<Entity>();

        public World(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public void LoadWorld()
        {
            _entities.Add(new Human(new Vector2(100, 100)));

            _entities.ForEach(x => x.LoadContent(_contentManager));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Draw background

            _entities.ForEach(x => x.Draw(gameTime, spriteBatch));
        }

        public void Update(GameTime gameTime)
        {
            _entities.ForEach(x => x.Update(gameTime));
        }
    }
}
