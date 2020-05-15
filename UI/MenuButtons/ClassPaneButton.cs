using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace RealClasses.UI.MenuButtons
{
    class ClassPaneButton : MenuButton
    {
        protected bool show;

        public ClassPaneButton(Texture2D texture) : base(texture)
        {
            _texture = texture;
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            description = "Default";
            show = false;
        }

        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);
            if (show == false)
            {
                show = true;
            }
            else show = false;
            
            //The bool fed here effects if ClassPane UIElement will show on screen or not (Append/Remove)
            ModContent.GetInstance<RealClasses>().CooldownBar.ShowClassPane(show);
        }
    }
}
