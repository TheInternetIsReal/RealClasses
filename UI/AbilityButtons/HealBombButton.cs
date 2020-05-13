using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.UI.AbilityButtons
{
    class HealBombButton : AbilityButton
    {
        public HealBombButton()
        {
            normalColor = new Color(255, 255, 255, 255);
            opaqueColor = new Color(128, 128, 128, 10);
             _texture = ModContent.GetTexture("RealClasses/UI/AbilityButtons/HealBombButton");
            ImageScale = 1f;
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            description = "Heal Bomb:\nSend a bomb to your friends or foes.\nHit hotkey again to detonate the bomb and do 100 aoe damage and healing in the area";
        }
    }

}
