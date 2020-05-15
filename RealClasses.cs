using Terraria.ModLoader;
using Terraria.UI;
using RealClasses.UI;
using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;
using RealClasses.Classes;
using RealClasses.Players;

namespace RealClasses
{
    public class RealClasses : Mod
    {
        public static Mod mod;

        //Key bindings
        public static ModHotKey ability1;
        public static ModHotKey ability2;
        public static ModHotKey ability3;
        public static ModHotKey ability4;
        public static ModHotKey ability5;
        public static List<ModHotKey> hotKeys;

        ////UI
        //CooldownBar
        internal CooldownBar CooldownBar;
        private UserInterface _cooldownBar;

        public RealClasses()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
            };
        }

        //Load settings
        public override void Load()
        {
            mod = this;

            //These show up in the Controls -> Key Bind section of the menu in game
            hotKeys = new List<ModHotKey>();
            ability1 = RegisterHotKey("Ability #1", "R");
            hotKeys.Add(ability1);
            ability2 = RegisterHotKey("Ability #2", "F");
            hotKeys.Add(ability2);
            ability3 = RegisterHotKey("Ability #3", "C");
            hotKeys.Add(ability3);
            ability4 = RegisterHotKey("Ability #4", "G");
            hotKeys.Add(ability4);
            ability5 = RegisterHotKey("Ability #5", "E");
            hotKeys.Add(ability5);

            //Cooldown bar setup
            CooldownBar = new CooldownBar();
            CooldownBar.Activate();
            _cooldownBar = new UserInterface();
            _cooldownBar.SetState(CooldownBar);
        }

        //Updates whatever gametime is
        public override void UpdateUI(GameTime gameTime)
        {
            _cooldownBar?.Update(gameTime);
        }

        //Draw the UI
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "RealClasses UI",
                    delegate
                    {
                        _cooldownBar.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}