using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;

namespace RealClasses.UI.AbilityButtons
{
    public class LifeRegenButton : AbilityButton
    {
        public LifeRegenButton()
        {
            normalColor = new Color(255, 255, 255, 255);
            opaqueColor = new Color(128, 128, 128, 1);
            ImageScale = 1f;
            _texture = ModContent.GetTexture("RealClasses/UI/AbilityButtons/LifeRegenButton");
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            description = "Regeneration:\nPASSIVE\nHeal faster when out of combat.\nHealing rate lowers the higher your HP is";
            hotKey = "";
        }
    }
}
