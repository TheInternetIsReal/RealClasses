using Microsoft.Xna.Framework.Graphics;

namespace RealClasses.UI
{
    public interface IUIAbilityButton
    {
        float ImageScale { get; }
        Texture2D _texture { get; }
        void Draw(SpriteBatch spriteBatch);
        int Cooldown { get; set; }
    }
}
