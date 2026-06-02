using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function.Styling
{
    internal class ButtonEffectConfig
    {
        public SimpleButton Button { get; set; }
        public Color DefaultBackColor { get; set; } = Color.White;
        public Color HoverBackColor { get; set; } = Color.RoyalBlue;
        public Color DefaultTextColor { get; set; } = Color.Red;
        public Color HoverTextColor { get; set; } = Color.White;
        public Color DefaultBorderColor { get; set; } = Color.Red; // Default border color
        public Color HoverBorderColor { get; set; } = Color.White; // New hover border color
        public float EasingSpeed { get; set; } = 0.04f; // Default easing speed
        public int BorderRadius { get; set; } = 3;
        public bool EnableBorderRadius { get; set; } = true; // Enable or disable rounded corners
        public int BorderWidth { get; set; } = 3;
    }
}
