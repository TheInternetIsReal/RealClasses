using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace RealClasses.UI.AbilityButtons
{
    public class AbilityButton : UIElement
    {
        protected bool opaque;
        protected string hotKey = "Empty";
        protected float ImageScale;
        protected Color normalColor;
        protected Color opaqueColor;
        protected Texture2D _texture;
        protected int stackCount;
        protected int cooldown;

        public AbilityButton()
        {

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

        //public virtual void Draw(SpriteBatch spritebatch) { }

    }
}
