using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;

namespace RealClasses.UI.AbilityButtons
{
    public class AbilityButton : UIElement
    {
        protected bool opaque;
        protected string hotKey = "Empty";
        protected string hoverText = "";
        protected string description;
        protected float ImageScale;
        protected Color normalColor;
        protected Color opaqueColor;
        protected Texture2D _texture;
        protected int stackCount;
        protected int cooldown;
        protected CalculatedStyle dimensions;

        public AbilityButton()
        {

        }

        //By default, every ability will draw a texture, key binding, hover and cooldown text
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
            if (cooldown > 0)
            {
                cooldown--;
                spriteBatch.Draw(this._texture, dimensions.Position(), null, opaqueColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, (cooldown / 60 + 1).ToString(), dimensions.X + 20, dimensions.Y + 20, Color.White, Color.Black, new Vector2(0f), ImageScale);
            }
            else spriteBatch.Draw(this._texture, dimensions.Position(), null, normalColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);

            //Draw the keybinding on top
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, hotKey, dimensions.X, dimensions.Y, Color.White, Color.Black, new Vector2(0f), ImageScale * 0.5f);
        }

        //Make a constructor that accepts a cooldown later? I kind of like hwo the ability pushes it now. It will always be in sync...

        public virtual void SetStack(int stack)
        {
            stackCount = stack;
        }

        public virtual void SetCooldown(int cooldown)
        {
            this.cooldown = cooldown;
        }

        public virtual void SetHotKey(string hotKey)
        {
            this.hotKey = hotKey;
        }

        public virtual void SetOpaque(bool opaque)
        {
            this.opaque = opaque;
        }

        //These next two don't need to be here. It's just to remind me how UIElement and MouseEvents work.
        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);
        }

        //Overriding this to nothing prevents the bar from dragging when an ability is clicked
        public override void MouseDown(UIMouseEvent evt)
        {
            //base.MouseOut(evt);
        }

        //public virtual void Draw(SpriteBatch spritebatch) { }

    }
}
