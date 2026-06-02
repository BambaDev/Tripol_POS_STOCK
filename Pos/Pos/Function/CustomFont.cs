using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class CustomFont
    {
        public static Font customFont;
        public static PrivateFontCollection fontCollection = new PrivateFontCollection();

        public static void LoadCustomFont(float fontSize = 10.0F, bool isBold = false)
        {
            // Load font from embedded resource
            byte[] fontData = Properties.Resources.WorkSans_Regular; // Ensure this matches the resource name
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            fontCollection.AddMemoryFont(fontPtr, fontData.Length);
            Marshal.FreeCoTaskMem(fontPtr);

            FontStyle fontStyle = isBold ? FontStyle.Bold : FontStyle.Regular;
            customFont = new Font(fontCollection.Families[0], fontSize, fontStyle);
        }
    }
}
