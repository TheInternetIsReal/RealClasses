using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI.Elements;

namespace RealClasses.UI.AbilityButtons
{
    class LeapButton : AbilityButton
    {
        public LeapButton()
        {
            normalColor = new Color(255, 255, 255, 255);
            opaqueColor = new Color(128, 128, 128, 10);
            _texture = ModContent.GetTexture("RealClasses/UI/AbilityButtons/LeapButton");         
            ImageScale = 1f;
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            description = "Leap Slam:\nLeap into the air and slam down on enemies.\nDeals 50 damage to enemies you fall on and 50 additional damage to enemies near your landing\n1 second of invulnerability when landing";
        }
    }
}