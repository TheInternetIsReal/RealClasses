using Terraria.ModLoader;
using Terraria.UI;
using RealClasses.UI;
using Microsoft.Xna.Framework;
using Terraria;
using System.Collections.Generic;

namespace RealClasses
{
    public class RealClasses : Mod
    {
        public static Mod mod;

        //Key bindings
        public static ModHotKey ability1;

        ////UI
        //Get an instance of our UI class
        internal ExampleUI ExampleUI;
        internal CooldownBar CooldownBar;
        //Get a UserInterface object to place it in later
        private UserInterface _exampleUserInterface;
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
            ability1 = RegisterHotKey("Ability #1", "g");

            //Get our ExampleUI and activate it (I guess)
            ExampleUI = new ExampleUI();
            ExampleUI.Activate();
            CooldownBar = new CooldownBar();
            CooldownBar.Activate();
            //Make a new UI interface object and set the state to our states
            _exampleUserInterface = new UserInterface();
            _exampleUserInterface.SetState(ExampleUI);
            _cooldownBar = new UserInterface();
            _cooldownBar.SetState(CooldownBar);
        }

        //Updates whatever gametime is
        public override void UpdateUI(GameTime gameTime)
        {
            //if (ExampleUI.Visible)
            //{
            _exampleUserInterface?.Update(gameTime);
            _cooldownBar?.Update(gameTime);
            //}

            //ExamplePersonUserInterface?.Update(gameTime);
        }

        //Draw the UI
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Coins Per Minute",
                    delegate
                    {
                        //if (ExampleUI.Visible)
                        //{
                            _exampleUserInterface.Draw(Main.spriteBatch, new GameTime());
                            _cooldownBar.Draw(Main.spriteBatch, new GameTime());
                        //}
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}