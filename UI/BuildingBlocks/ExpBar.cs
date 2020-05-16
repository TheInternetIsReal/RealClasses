using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria;

namespace RealClasses.UI.BuildingBlocks
{
    class ExpBar : DragableUIPanel
    {
        protected int exp = 999;
        protected CalculatedStyle dimensions;

        public void Expbar()
        {
            dimensions = base.GetDimensions();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            //Gets the top left point of whatever this UIElement is appended to. Screen view by default but usually the top left of a panel etc
            dimensions = base.GetDimensions();
            base.DrawSelf(spriteBatch);
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, exp.ToString(), dimensions.X, dimensions.Y, Color.White, Color.Black, new Vector2(0f), 1f);
        }
    }
}
