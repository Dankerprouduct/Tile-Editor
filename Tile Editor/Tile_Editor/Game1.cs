using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO; 

namespace Tile_Editor
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboardState;
        KeyboardState oldKeyboardState; 
        MouseState oldMs;
        MouseState ms;
        Tiles tiles = new Tiles();
        int[,] tileMap;
        int tileID = 0;
        string tileString = "Blank"; 
        SpriteFont font;
        Texture2D GuiBack;
        StreamWriter sW;
        Vector2 worldPosition;

        Camera camera; 
        public Texture2D cursor;
        public Vector2 curPos;
        public int height;
        public int width;
        int x; 
        int y;
        Vector2 mousePosition;
        Random random;
        int size = 100;
        bool swStart = false;
        bool largeBrush = false; 
        public Game1()
        {
            width = 1080;
            height = (width / 16) * 9; 
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width; 
        }


        protected override void Initialize()
        {
            IsMouseVisible = true;
            camera = new Camera(GraphicsDevice.Viewport); 
            base.Initialize();
        }


        protected override void LoadContent()
        {
            random = new Random(); 
            sW = new StreamWriter(@"C:\Users\David\Desktop\" + random.Next(0, 100000) + ".txt");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("GUI"); 
            tiles.LoadContent(Content);
            GuiBack = Content.Load<Texture2D>("BackToGUI"); 
            tileMap = new int[size, size];
            cursor = Content.Load<Texture2D>("Cursor");
            curPos = new Vector2(400, 300); 
        }


        protected override void UnloadContent()
        {
            
        }


        protected override void Update(GameTime gameTime)
        {
            camera.Update(gameTime, this);
            // Allows the game to exit
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // TODO: Add your update logic here
            ms = Mouse.GetState();
            mousePosition = new Vector2(ms.X, ms.Y);
            worldPosition = Vector2.Transform(mousePosition, Matrix.Invert(camera.transform));
            if (tileID >= tiles.tiles.Count)
            {
                tileID = 0;
            }
            if(keyboardState.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter))
            {

                
                for(int x = 0; x < size; x++)
                {

                    if (swStart)
                    {
                        sW.WriteLine(" ");
                    }
                    
                    swStart = false; 
                    for (int y = 0; y < size; y++)
                    {
                        if (!swStart)
                        {
                            sW.Write(tileMap[x, y]);
                            swStart = true;
                        }
                        else
                        {
                            sW.Write(", " + tileMap[x, y]);
                        }
                    }
                    
                }


                Console.WriteLine("Saved");
                sW.Flush();
                
            }
            if(keyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space))
            {
                largeBrush = !largeBrush; 
            }
            oldKeyboardState = keyboardState;
            x = Convert.ToInt32(worldPosition.X) / (int)(50);
            y = Convert.ToInt32(worldPosition.Y) / (int)(50);
            if (ms.LeftButton == ButtonState.Pressed)
            {
                

                Console.WriteLine(x + " " + y);
                if (largeBrush)
                {
                    try
                    {
                        tileMap[x, y] = tileID;
                        tileMap[x + 1, y + 1] = tileID;
                        tileMap[x - 1, y - 1] = tileID;
                        tileMap[x + 1, y - 1] = tileID;
                        tileMap[x - 1, y + 1] = tileID;
                        tileMap[x - 0, y - 1] = tileID;
                        tileMap[x - 1, y - 0] = tileID;
                        tileMap[x + 1, y + 0] = tileID;
                        tileMap[x - 0, y + 1] = tileID;
                    }
                    catch (Exception ex)
                    {
                        // Console.WriteLine(ex); 
                    }
                }
                else
                {

                    try
                    {
                        tileMap[x, y] = tileID;
                    }
                    catch(Exception ex)
                    {

                    }
                }
            }   
            if(ms.RightButton == ButtonState.Pressed && oldMs.RightButton == ButtonState.Released)
            {
                tileID++;
                
            }
            oldMs = ms;



            if (keyboardState.IsKeyDown(Keys.W))
            {
                curPos.Y -= 3; 
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                curPos.Y += 3;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                curPos.X += 3;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                curPos.X -= 3;
            }

            switch (tileID)
            {
                case 0:
                    {
                        tileString = "Blank";
                        break;
                    }
                case 1:
                    {
                        tileString = "Tile1(Grey)";
                        break;
                    }
                case 2:
                    {
                        tileString = "Tile2(Green)";
                        break; 
                    }
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null,
                camera.transform);


            for (int x = 0; x < tileMap.GetLength(1); x++)
            {
                for (int y = 0; y < tileMap.GetLength(0); y++)
                {
                    spriteBatch.Draw(tiles.tiles[tileMap[x, y]], new Vector2(50 * x, 50 * y), Color.White); 
                }

            }

            //spriteBatch.Draw(cursor, curPos, Color.White); 

            spriteBatch.DrawString(font, "Large Brush: [" + largeBrush + "]", new Vector2(worldPosition.X, worldPosition.Y - 90), Color.Black);
            spriteBatch.DrawString(font, "Tile: [" + tileString + "]", new Vector2(worldPosition.X, worldPosition.Y - 60), Color.Black); 
            spriteBatch.DrawString(font, "Tile Num: [" + x +","+ y +"]", new Vector2(worldPosition.X, worldPosition.Y - 30), Color.Black);

            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
