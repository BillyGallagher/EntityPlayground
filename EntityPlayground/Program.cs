﻿using System;

namespace EntityPlayground
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new EntityPlayground())
                game.Run();
        }
    }
}
