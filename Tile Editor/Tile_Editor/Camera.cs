using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Tile_Editor
{
    class Camera
    {
        Viewport view;
        Vector2 center;
        public Matrix transform;
        float scale = .3f;
        KeyboardState keyboardState;

        public Camera(Viewport vPort)
        {
            view = vPort; 
        }
        public void Update(GameTime gameTime, Game1 game)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.E))
            {
                scale += .001f;
            }
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                scale -= .001f;
            }

            center = new Vector2((game.curPos.X + (game.cursor.Width / 2)) - (game.width / 2), (game.curPos.Y + (game.cursor.Height / 2)) - (game.height / 2));

            transform = Matrix.CreateScale(new Vector3(scale, scale, 1)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)); 
        }
    }
}
