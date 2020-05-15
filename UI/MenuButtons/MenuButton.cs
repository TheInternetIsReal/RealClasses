using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace RealClasses.UI.MenuButtons
{
    //Menu buttons are pretty simple. They draw their texture, have hover text and make a sound when clicked
    class MenuButton : UIElement
    {
        protected string hoverText = "";
        protected string description;
        protected Texture2D _texture;
        protected CalculatedStyle dimensions;


        public MenuButton(Texture2D texture)
        {
            _texture = texture;
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            description = "Default";
        }

        //By default, every menu button will draw a texture and hover text
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Check if mouse is hovering and draw skill description if so. MouseEvents rely on width and height of the UIElement to trigger
            if (IsMouseHovering)
            {
                Main.hoverItemName = description;
            }

            //Gets the top left point of whatever this UIElement is appended to. Screen view by default but usually the top left of a panel etc
            dimensions = base.GetDimensions();

            //Draw the texture and cooldown from the the top left of its parent
            spriteBatch.Draw(this._texture, dimensions.Position(), Color.White);

        }

        //Should activate a UI panel later
        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseOver(evt);
            Main.PlaySound(12, -1, -1, 1);
        }
    }
}
