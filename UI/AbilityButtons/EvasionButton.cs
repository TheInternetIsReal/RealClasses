using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;

namespace RealClasses.UI.AbilityButtons
{
    class EvasionButton : AbilityButton
    {
        public EvasionButton()
        {
            normalColor = new Color(255, 255, 255, 255);
            opaqueColor = new Color(128, 128, 128, 10);
            _texture = ModContent.GetTexture("RealClasses/UI/AbilityButtons/EvasionButton");
            ImageScale = 1f;
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            description = "Evasion:\nMove like the wind.\nEvade all attacks and move faster for 3 seconds";
        }
    }
}

