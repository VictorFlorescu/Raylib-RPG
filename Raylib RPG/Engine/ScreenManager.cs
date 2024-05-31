using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rectangle = Raylib_CsLo.Rectangle;

namespace Engine
{
    internal static class ScreenManager
    {
        // For different screens
        public enum Screen { LOGO = 0, MAIN_MENU, SETTINGS, GAME, PAUSE_MENU, END }

        public static Screen currentScreen;

        // Variables for different screens
        // Logo Screen objects and variables
        public static int logoPositionX = Engine.screenWidth / 2 - 128;
        public static int logoPositionY = Engine.screenHeight / 2 - 128;
        public static int lettersCount = 0;
        public static int frameCounter = 0;
        public static int topSideRecWidth = 16;
        public static int leftSideRecHeight = 16;
        public static int bottomSideRecWidth = 16;
        public static int rightSideRecHeight = 16;
        public static int state = 0;
        public static float alpha = 1.0f;
        
        // Set a screen to be used / rendered
        public static void SetScreen(Screen screen)
        {
            currentScreen = screen;
        }

        // Update logic for the current screen
        public static void UpdateScreen()
        {
            switch (currentScreen)
            {
                case Screen.LOGO:
                    {
                        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                        {
                            //skip the loading screen
                            currentScreen = Screen.MAIN_MENU;
                        }

                        else if (state == 0)
                        {
                            frameCounter++;

                            if (frameCounter == 120)
                            {
                                state = 1;
                                frameCounter = 0;
                            }
                        }
                        else if (state == 1)
                        {
                            topSideRecWidth += 4;
                            leftSideRecHeight += 4;

                            if (topSideRecWidth == 256)
                            {
                                state = 2;
                            }
                        }
                        else if (state == 2)
                        {
                            bottomSideRecWidth += 4;
                            rightSideRecHeight += 4;

                            if (bottomSideRecWidth == 256)
                            {
                                state = 3;
                            }
                        }

                        else if (state == 3)
                        {
                            frameCounter++;

                            if ((frameCounter % 12) == 0)
                            {
                                lettersCount++;
                                frameCounter = 0;
                            }

                            if (lettersCount >= 10)
                            {
                                alpha -= 0.02f;
                                if (alpha <= 0.0f)
                                {
                                    alpha = 0.0f;
                                    state = 4;
                                }
                            }

                        }
                        else if (state == 4)
                        {
                            SetScreen(Screen.MAIN_MENU);
                        }
                        break;
                    }

                case Screen.MAIN_MENU:
                    {
                        break;
                    }
            }
        }

        // Draw the current screen
        public static void DrawScreen()
        {
            switch (currentScreen)
            {
                case Screen.LOGO:
                    {

                        Raylib.DrawText("Press enter to skip logo", Raylib.GetScreenWidth() - 200, Raylib.GetScreenHeight() - 20, 15, Raylib.BLACK);


                        if (state == 0)
                        {
                            Raylib.DrawText("Powered by", Raylib.GetScreenWidth() / 2 - 60, Raylib.GetScreenHeight() / 2 - 200, 20, Raylib.BLACK);
                            if (((frameCounter / 15) % 2) != 0)
                            {
                                Raylib.DrawRectangle(logoPositionX, logoPositionY, 16, 16, Raylib.BLACK);
                            }
                        }

                        else if (state == 1)
                        {
                            Raylib.DrawText("Powered by", Raylib.GetScreenWidth() / 2 - 60, Raylib.GetScreenHeight() / 2 - 200, 20, Raylib.BLACK);
                            Raylib.DrawRectangle(logoPositionX, logoPositionY, topSideRecWidth, 16, Raylib.BLACK);
                            Raylib.DrawRectangle(logoPositionX, logoPositionY, 16, leftSideRecHeight, Raylib.BLACK);
                        }

                        else if (state == 2)
                        {
                            Raylib.DrawText("Powered by", Raylib.GetScreenWidth() / 2 - 60, Raylib.GetScreenHeight() / 2 - 200, 20, Raylib.BLACK);
                            Raylib.DrawRectangle(logoPositionX, logoPositionY, topSideRecWidth, 16, Raylib.BLACK);
                            Raylib.DrawRectangle(logoPositionX, logoPositionY, 16, leftSideRecHeight, Raylib.BLACK);

                            Raylib.DrawRectangle(logoPositionX + 240, logoPositionY, 16, rightSideRecHeight, Raylib.BLACK);
                            Raylib.DrawRectangle(logoPositionX, logoPositionY + 240, bottomSideRecWidth, 16, Raylib.BLACK);
                        }

                        else if (state == 3)
                        {
                            Raylib.DrawText("Powered by", Raylib.GetScreenWidth() / 2 - 60, Raylib.GetScreenHeight() / 2 - 200, 20, Raylib.Fade(Raylib.BLACK, alpha));
                            Raylib.DrawRectangle(logoPositionX, logoPositionY, topSideRecWidth, 16, Raylib.Fade(Raylib.BLACK, alpha));
                            Raylib.DrawRectangle(logoPositionX, logoPositionY + 16, 16, leftSideRecHeight - 32, Raylib.Fade(Raylib.BLACK, alpha));

                            Raylib.DrawRectangle(logoPositionX + 240, logoPositionY + 16, 16, rightSideRecHeight - 32, Raylib.Fade(Raylib.BLACK, alpha));
                            Raylib.DrawRectangle(logoPositionX, logoPositionY + 240, bottomSideRecWidth, 16, Raylib.Fade(Raylib.BLACK, alpha));

                            Raylib.DrawRectangle(Raylib.GetScreenWidth() / 2 - 112, Raylib.GetScreenHeight() / 2 - 112, 224, 224, Raylib.Fade(Raylib.WHITE, alpha));

                            Raylib.DrawText(Raylib.TextSubtext("raylib", 0, lettersCount), Raylib.GetScreenWidth() / 2 - 44, Raylib.GetScreenHeight() / 2 + 48, 50, Raylib.Fade(Raylib.BLACK, alpha));

                        }

                        // Show the middle of the screen
                        //Raylib.DrawRectangle(0, Raylib.GetScreenHeight() / 2, 1280, 1, Raylib.RED);
                        //Raylib.DrawRectangle(Raylib.GetScreenWidth() / 2, 0, 1, 800, Raylib.RED);


                        break;
                    }

                case Screen.MAIN_MENU:
                    {

                        Raylib.DrawText("Main menu", Raylib.GetScreenWidth() / 2 - Raylib.MeasureText("Main menu", 60) / 2, Raylib.GetScreenHeight() / 2 - 250, 60, Raylib.BLACK);

                        Raylib.DrawText("Test", Raylib.GetScreenWidth() / 2 - Raylib.MeasureText("Test", 15) / 2, Raylib.GetScreenHeight() / 2 - 7, 15, Raylib.BLACK);


                        // -------------------------------

                        // Show the middle of the screen
                        //Raylib.DrawRectangle(0, Raylib.GetScreenHeight() / 2, 1280, 1, Raylib.RED);
                        //Raylib.DrawRectangle(Raylib.GetScreenWidth() / 2, 0, 1, 800, Raylib.RED);

                        break;
                    }
            }

        }

    }

}