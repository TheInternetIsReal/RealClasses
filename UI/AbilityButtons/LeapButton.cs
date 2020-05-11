using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;

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
        }

        //This essentially draws itself, but not until it's in a UIState that is drawn in Mod
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Gets the top left point of whatever this UIElement is appended to. Screen by default but could be the top left of a panel etc
            CalculatedStyle dimensions = base.GetDimensions();
            //Draw it the the top left of its parent

            if (cooldown > 0)
            {
                cooldown--;
                spriteBatch.Draw(this._texture, dimensions.Position(), null, opaqueColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, (cooldown / 60 + 1).ToString(), dimensions.X + 20, dimensions.Y + 20, Color.White, Color.Black, new Vector2(0f), ImageScale);
                //Utils.DrawBorderString(spriteBatch, cooldown.ToString(), dimensions.Position() /2, Color.White);
            }
            else spriteBatch.Draw(this._texture, dimensions.Position(), null, normalColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);

            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, hotKey, dimensions.X, dimensions.Y, Color.White, Color.Black, new Vector2(0f), ImageScale * 0.5f);

        }
    }
}