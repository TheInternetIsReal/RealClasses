using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using RealClasses.Players;
using Terraria.ModLoader;

namespace RealClasses.UI.MenuButtons
{
    class ClassSwitchButton : MenuButton
    {
        protected string className;

        public ClassSwitchButton(Texture2D texture, string className, string description) : base(texture)
        {
            _texture = texture;
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            this.description = description;
            this.className = className;
        }

        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);
            Main.LocalPlayer.GetModPlayer<MyPlayer>().ChangeClass(className);
        }
    }
}
