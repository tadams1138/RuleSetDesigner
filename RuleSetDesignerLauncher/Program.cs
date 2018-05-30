﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RuleSetDesignerLauncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(CreateLauncher(args));
        }

        private static Launcher CreateLauncher(IReadOnlyList<string> args)
        {
            var launcher = args.Count == 2 ? new Launcher(args[0], args[1]) : new Launcher();
            return launcher;
        }
    }
}