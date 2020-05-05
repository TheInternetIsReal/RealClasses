using RealClasses.UI.AbilityButtons;
using Terraria;
using Terraria.UI;

namespace RealClasses.Passives
{
    public interface IPassive
    {
        void DoPassive(Player player);
        AbilityButton passiveButton { get; set; }
    }
}
