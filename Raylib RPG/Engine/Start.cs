using Raylib_CsLo;

namespace Engine
{
    internal static class Engine
    {
        public static bool devMode = true;
        public static string version = "0.1";


        public static int screenWidth = 1280;
        public static int screenHeight = 720;

        public static void Main()
        {
            /* Update check - TODO: Redesign

            string updatePromptMessage = "Check for updates?";
            string updatePromptCaption = "Update";
            MessageBoxButtons updatePromptButtons= MessageBoxButtons.YesNo;
            DialogResult updatePromptResult; 

            updatePromptResult = MessageBox.Show(updatePromptMessage, updatePromptCaption, updatePromptButtons);
            if(updatePromptResult == System.Windows.Forms.DialogResult.Yes)
            {
                await AppUpdater.Update();
            }

            */

            EngineLoop();
        }

        public static void EngineLoop()
        {
            Raylib.InitWindow(screenWidth, screenHeight, "RPG Alpha Version " + version);

            Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor())); // Sets max FPS using the refresh rate of the current display


            // Screen Manager

            // Engine Loop
            while (!Raylib.WindowShouldClose())
            {
                // Update 
                ScreenManager.UpdateScreen();

                // ----------------------------------------

                // Draw

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.WHITE);

                if (devMode)
                {
                    Raylib.DrawFPS(10, 10);
                }
                ScreenManager.DrawScreen();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}