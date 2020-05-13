using Terraria.UI;
using RealClasses.Abilities;
using RealClasses.UI.BuildingBlocks;
using RealClasses.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using RealClasses.UI.AbilityButtons;

namespace RealClasses.UI
{
    public class CooldownBar : UIState
    {
        //public BerserkButton berserkButton;
        //UIElement button1 = new BerserkButton();
        DragableUIPanel Bar = new DragableUIPanel();
        //BerserkButton button2 = new BerserkButton(5);
        //BerserkButton button3 = new BerserkButton(10);
        //BerserkButton button4 = new BerserkButton();
        //LifeRegenButton button5 = new LifeRegenButton();

        //This should be a list later
        public override void OnInitialize()
        {
            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
            Bar = new DragableUIPanel();
            Bar.SetPadding(6f);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(coinCounterPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            Bar.Left.Set(800f, 0f);
            Bar.Top.Set(800f, 0f);
            Bar.Width.Set(346f, 0f);
            Bar.Height.Set(76f, 0f);
            Bar.BackgroundColor = new Color(73, 94, 171);

            Append(Bar);

        }

        public void Cleanup()
        {
            Bar.RemoveAllChildren();
        }

        public void SetButtons(UIElement button1, UIElement button2, UIElement button3, UIElement button4, UIElement passive1)
        {
            button1.Left.Set(0, 0f);
            button1.Top.Set(0, 0f);
            Bar.Append(button1);

            button2.Left.Set(67, 0f);
            button2.Top.Set(0, 0f);
            Bar.Append(button2);

            button3.Left.Set(134, 0f);
            button3.Top.Set(0, 0f);
            Bar.Append(button3);

            button4.Left.Set(201, 0f);
            button4.Top.Set(0, 0f);
            Bar.Append(button4);

            passive1.Left.Set(270, 0f);
            passive1.Top.Set(0, 0f);
            Bar.Append(passive1);
        }
    }
}
