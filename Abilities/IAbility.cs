using Terraria;
using RealClasses.UI;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.Abilities
{
    //TURN THIS INTO A CLASS INSTEAD. Variables are so annoying with interfaces
    public interface IAbility
    {
        void UseAbility(Player player);
        void GiveHotKey(string hotKey);
        void DoCooldown();
        AbilityButton abilityButton { get; set; }
        int Cooldown { get; set; }
    }

}
