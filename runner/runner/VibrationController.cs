using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Devices;

namespace runner
{
    /// <summary>
    /// Static class controlling the vibrations, based on the settings
    /// </summary>
    static class VibrationController
    {
        public static void Start(int seconds)
        {
            if (Settings.vibrate)
                VibrateController.Default.Start(TimeSpan.FromSeconds(3));
        }

        public static void Stop()
        {
            VibrateController.Default.Stop();
        }
    }
}
