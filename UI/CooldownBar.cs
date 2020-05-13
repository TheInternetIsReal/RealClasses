using Terraria.UI;
using RealClasses.UI.BuildingBlocks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RealClasses.UI
{
    public class CooldownBar : UIState
    {
        //We inherit from UIState. A UISTate is like a palette that many UI elements should be placed on. It's the top level for a UI.

        DragableUIPanel Bar = new DragableUIPanel();
        private float counter;
        private static float padding = 6;
        private float offset = padding / 2;
        private float textureWidth = 64;
        private float width = 0;
        private float height = 0;

        //This should be a list later
        public override void OnInitialize()
        {
            counter = 1;

            // Here we define our container UIElement. In DragableUIPanel.cs, you can see that DragableUIPanel is a UIPanel with a couple added features.
            Bar = new DragableUIPanel();
            Bar.SetPadding(6f);
            // We need to place this UIElement in relation to its Parent. Later we will be calling `base.Append(coinCounterPanel);`. 
            // This means that this class, ExampleUI, will be our Parent. Since ExampleUI is a UIState, the Left and Top are relative to the top left of the screen.
            Bar.Left.Set(800f, 0f);
            Bar.Top.Set(800f, 0f);
            //Bar.Width.Set(346f, 0f);
            Bar.Width.Set(width, 0f);
            //Bar.Height.Set(76f, 0f);
            Bar.Height.Set(height, 0f);
            Bar.BackgroundColor = new Color(73, 94, 171);

            Append(Bar);
        }

        public void Cleanup()
        {
            Bar.RemoveAllChildren();
        }

        
        public void SetButtons(List<UIElement> buttons)
        {
            foreach (UIElement button in buttons)
            {
                //64 + 3 = 67 pixels for each button
                button.Left.Set(((textureWidth + offset) * counter) - textureWidth, 0);
                Bar.Append(button);
                counter++;
            }

            Bar.Width.Set((textureWidth + padding) * (counter - 1), 0f);
            Bar.Height.Set(76f, 0f);
            counter = 1;
            Bar.Recalculate();
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
