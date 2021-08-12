using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DemoUI {
    public static class SuspendUpdateExt {
        // https://stackoverflow.com/questions/487661/how-do-i-suspend-painting-for-a-control-and-its-children

        private const int WM_SETREDRAW = 0x000B;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        public static void ResumeDrawing(this Control control, bool redraw = true) {
            SendMessage(control.Handle, WM_SETREDRAW, true, 0);

            if (redraw) {
                control.Refresh();
            }
        }

        public static void SuspendDrawing(this Control control) {
            SendMessage(control.Handle, WM_SETREDRAW, false, 0);
        }
    }
}
