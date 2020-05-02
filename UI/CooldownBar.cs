using Terraria.UI;

namespace RealClasses.UI
{
    class CooldownBar : UIState
    {
        public Playground cooldownBar;

        public override void OnInitialize()
        {
            cooldownBar = new Playground();

            Append(cooldownBar);
        }
    }
}
