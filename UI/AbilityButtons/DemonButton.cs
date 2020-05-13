using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using RealClasses.Abilities;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.UI.AbilityButtons
{
    class DemonButton
: AbilityButton
    {
        public DemonButton()
        {
            normalColor = new Color(255, 255, 255, 255);
            opaqueColor = new Color(128, 128, 128, 10);
            _texture = ModContent.GetTexture("RealClasses/UI/AbilityButtons/DemonButton");
            ImageScale = 1f;
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            description = "Demon Imp:\nLet your Imp do your bidding.\nThe Imp will attack enemies within line of sight for 50 damage every second";
        }

        //This essentially draws itself, but not until it's in a UIState that is drawn in Mod
        public override void Draw(SpriteBatch spriteBatch)
        {

            //Gets the top left point of whatever this UIElement is appended to. Screen by default but could be the top left of a panel etc
            CalculatedStyle dimensions = base.GetDimensions();
            //Draw it the the top left of its parent

            if (opaque == true)
            {
                spriteBatch.Draw(this._texture, dimensions.Position(), null, opaqueColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(this._texture, dimensions.Position(), null, normalColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);
            }

            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, hotKey, dimensions.X, dimensions.Y, Color.White, Color.Black, new Vector2(0f), ImageScale * 0.5f);

            if (IsMouseHovering)
            {
                Main.hoverItemName = description;
            }
        }
    }
}
