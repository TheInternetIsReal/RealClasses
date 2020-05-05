using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace RealClasses.UI.AbilityButtons
{
    public class AbilityButton : UIElement
    {
        float ImageScale;
        Texture2D _texture;
        public int cooldown;
        public int stackCount;
        public bool opaque;
        public string hotKey = "Empty";
    }
}
