using Terraria.UI;
using RealClasses.UI.BuildingBlocks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using RealClasses.UI.MenuButtons;
using Terraria.ModLoader;

namespace RealClasses.UI
{
    public class CooldownBar : UIState
    {
        //We inherit from UIState. A UISTate is like a palette that many UI elements should be placed on. It's the top level for a UI.

        //I instantiate instnaces here because these will ALWAYS be there in the UI. This helps because code below references these directly later on and assumes they are there
        DragableUIPanel Bar = new DragableUIPanel();
        DragableUIPanel ClassPane = new DragableUIPanel();
        MenuButton classSelect = new ClassPaneButton(ModContent.GetTexture("Terraria/UI/ButtonPlay"));
        List<MenuButton> menuButtons = new List<MenuButton>(); //used for menu buttons on cooldown bar later
        ClassSwitchButton testClassButton = new ClassSwitchButton(ModContent.GetTexture("RealClasses/Projectiles/Testprojectile"), "Test", "Test class for testing");
        ClassSwitchButton warriorClassButton = new ClassSwitchButton(ModContent.GetTexture("RealClasses/UI/AbilityButtons/BerserkButton"), "Warrior", "Warrior class");
        List<ClassSwitchButton> classButtons = new List<ClassSwitchButton>(); //used for ClassPane class selection buttons later
        private float abilityCounter;
        private float menuCounter;
        private static float padding = 6;
        private float offset = padding / 2;
        private float abilityWidth = 64;
        private float menuWidth = 32;

        //Default look
        public override void OnInitialize()
        {
            abilityCounter = 1;
            menuCounter = 1;

            //Cooldown bar. We need to place this UIElement in relation to its Parent. Since this is in a UIState, the Left and Top are relative to the top left of the screen.
            Bar.SetPadding(6f);
            Bar.Width.Set(32f, 0f);
            Bar.Height.Set(32f, 0f);
            Bar.Left.Set(800f, 0f);
            Bar.Top.Set(800f, 0f);
            Bar.BackgroundColor = new Color(73, 94, 171);
            //Bar.visible = true;
            Append(Bar);
            //Add menu buttons
            menuButtons.Add(classSelect);
            foreach (MenuButton menuButton in menuButtons)
            {
                Bar.Append(menuButton);
            }

            //Class choice pane. It doesn't get appended and show until the class pane menu button is clicked
            ClassPane.SetPadding(6f);
            ClassPane.Width.Set(400f, 0f);
            ClassPane.Height.Set(400f, 0f);
            ClassPane.Left.Set(Bar.Left.Pixels, 0f);
            ClassPane.Top.Set(Bar.Top.Pixels + Bar.Height.Pixels - ClassPane.Height.Pixels, 0f);
            ClassPane.BackgroundColor = new Color(73, 94, 171);
            //Give it some random buttons for now
            ClassPane.Append(testClassButton);
            warriorClassButton.Top.Set(128f, 0f);
            ClassPane.Append(warriorClassButton);
        }

        public void Cleanup()
        {
            Bar.RemoveAllChildren();
        }

        //Make CLass Pane UIElement visible or not
        public void ShowClassPane(bool show)
        {
            ClassPane.Left.Set(Bar.Left.Pixels, 0f);
            //This differs from above because in initialize because the bar doesn't have a height up there, this does
            ClassPane.Top.Set(Bar.Top.Pixels  - ClassPane.Height.Pixels, 0f);

            //Append or pop to completely disable the element. Disabling Draw() still left the element and therefore the mouse events active
            if (show == true)
            {
                Append(ClassPane);
            }
            else RemoveChild(ClassPane);
        }

        //Dynamically make cooldownbar
        public void SetButtons(List<UIElement> abilityButtons)
        {
            //Dynamically place Ability buttons on bar
            foreach (UIElement abilityButton in abilityButtons)
            {
                //64 + 3 = 67 pixels for each button
                abilityButton.Left.Set(((abilityWidth + offset) * abilityCounter) - abilityWidth, 0);
                Bar.Append(abilityButton);
                abilityCounter++;
            }

            //Dynamically place Menu buttons on bar
            foreach (MenuButton menuButton in menuButtons)
            {
                //Add menu buttons on the right now that we setup the ability buttons
                //This should probably be stored better, it's the same as below. Also, it's wrong and only works for one menubutton
                menuButton.Left.Set((abilityWidth + padding) * (abilityCounter - 1), 0f);
                Bar.Append(menuButton);
                menuCounter++;
            }

            //Dynamically resize the bar to fit all of the buttons
            //Fuck this math. Need to clean thisup later. It's essentially the ability buttons weidth, plus padding, plus the menu button's width, plus padding
            Bar.Width.Set(((abilityWidth + padding) * (abilityCounter - 1)) + menuWidth * (menuCounter -1) + offset , 0f);
            Bar.Height.Set(76f, 0f);
            abilityCounter = 1;
            menuCounter = 1;
            Bar.Recalculate();
        }

        //Legacy
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
