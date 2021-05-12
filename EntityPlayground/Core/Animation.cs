using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityPlayground.Core
{
    public class Animation
    {
        public Texture2D Texture { get; }
        public int FrameHeight { get; }
        public int FrameWidth { get; }
        public float FrameTime { get; }
        public bool IsLooping { get; }

        public int FrameCount
        {
            get { return Texture.Width / FrameWidth; }
        }

        public Animation(Texture2D texture, int frameHeight, int frameWidth, float frameTime, bool isLooping)
        {
            Texture = texture;
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
            FrameTime = frameTime;
            IsLooping = isLooping;
        }
    }
}
