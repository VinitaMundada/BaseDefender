﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseDefender
{
    public enum ButtonStatus
    {
        Normal,
        MouseOver,
        Pressed,
    }

    public class Button : Sprite
    {
        private MouseState previousState;
        private Texture2D hoverTexture;
        private Texture2D pressedTexture;
        public event EventHandler Clicked;
        private Rectangle bounds;
        private Texture2D buttonTexture;

        private ButtonStatus state = ButtonStatus.Normal;

        public Button(Texture2D texture, Vector2 position):base(texture,position)
        {
            this.buttonTexture = texture;
            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Button(Texture2D texture, Texture2D hoverTexture, Texture2D pressedTexture, Vector2 position)
            : base(texture, position)
        {
            this.hoverTexture = hoverTexture;
            this.pressedTexture = pressedTexture;

            this.bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            int mouseX = mouseState.X;
            int mouseY = mouseState.Y;
            bool isMouseOver = bounds.Contains(mouseX, mouseY);

            if (isMouseOver && state != ButtonStatus.Pressed)
            {
                state = ButtonStatus.MouseOver;
            }

            else if (isMouseOver == false && state != ButtonStatus.Pressed)
            {
                state = ButtonStatus.Normal;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released)
            {
                if (isMouseOver == true)
                {
                    state = ButtonStatus.Pressed;
                }

            }

            if (mouseState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    state = ButtonStatus.MouseOver;

                    if (Clicked != null)
                    {
                        Clicked(this, EventArgs.Empty);
                    }
                }

                else if (state == ButtonStatus.Pressed)
                {
                    state = ButtonStatus.Normal;
                }
            }

            previousState = mouseState;

            //base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (state)
            {
                case ButtonStatus.Normal:
                    spriteBatch.Draw(texture, bounds, Color.White);
                    break;
                case ButtonStatus.MouseOver:
                    spriteBatch.Draw(hoverTexture, bounds, Color.White);
                    break;
                case ButtonStatus.Pressed:
                    spriteBatch.Draw(pressedTexture, bounds, Color.White);
                    break;
                default:
                    spriteBatch.Draw(texture, bounds, Color.White);
                    break;
            }

            //base.Draw(spriteBatch);
        }
    }
}
