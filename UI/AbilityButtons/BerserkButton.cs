using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;
using Terraria.ModLoader;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.UI
{
    public class BerserkButton : AbilityButton
    {
        //Set some variables from UIElement on cosntruction
        public BerserkButton()
        {
            normalColor = new Color(255, 255, 255, 255);
            opaqueColor = new Color(128, 128, 128, 10);
            _texture = ModContent.GetTexture("RealClasses/UI/AbilityButtons/BerserkButton");
            base.Width.Set((float)this._texture.Width, 0f);
            base.Height.Set((float)this._texture.Height, 0f);
            ImageScale = 1f;
            description = "Berserk:\nHurt yourself to hurt them more.\nSacrifice 10% hp to gain 10% extra damage to any attack for 8 seconds.\nStacks 3 times";
        }

        //This essentially draws itself, but not until it's in a UIState that is drawn in Mod
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Gets the top left point of whatever this UIElement is appended to. Screen by default but should be the top left of a panel etc
            CalculatedStyle dimensions = base.GetDimensions();

            if (IsMouseHovering)
            {
                Main.hoverItemName = description;
            }

            //I would GET cooldown here but I made Ability graba copy of this, not vice versa
            if (cooldown > 0)
            {
                cooldown--;
                //Draw at full color
                spriteBatch.Draw(this._texture, dimensions.Position(), null, normalColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);
                //Draw cooldown
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, (cooldown / 60 + 1).ToString(), dimensions.X + 25, dimensions.Y + 20, Color.White, Color.Black, new Vector2(0f), ImageScale);
            }
            //Draw normal as well since this can always be pushed
            else spriteBatch.Draw(this._texture, dimensions.Position(), null, normalColor, 0f, Vector2.Zero, this.ImageScale, SpriteEffects.None, 0f);

            if (stackCount > 0)
            {
                //Draw stackCount
                Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, stackCount.ToString(), dimensions.X, dimensions.Y, Color.Red, Color.Black, new Vector2(0f), ImageScale * 2);
            }

            //Always draw the hotkey
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, hotKey, dimensions.X, dimensions.Y, Color.White, Color.Black, new Vector2(0f), ImageScale * 0.5f);
        }
    }
}
