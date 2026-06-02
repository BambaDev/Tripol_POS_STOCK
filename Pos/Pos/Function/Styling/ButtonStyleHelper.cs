using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos.Function.Styling
{
    internal class ButtonStyleHelper
    {
        // Apply the easing effect to a button
        public void ApplyEasingEffectToButton(ButtonEffectConfig config)
        {
            Timer hoverTimer = new Timer();
            float hoverProgress = 0f;
            bool isHovered = false;

            // Set up Paint event to draw the button with the given properties
            config.Button.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                Rectangle rect = config.Button.ClientRectangle;

                // Interpolate between default and hover state based on hoverProgress
                Color currentBackColor = InterpolateColor(config.DefaultBackColor, config.HoverBackColor, hoverProgress);
                Color currentTextColor = InterpolateColor(config.DefaultTextColor, config.HoverTextColor, hoverProgress);
                Color currentBorderColor = InterpolateColor(config.DefaultBorderColor, config.HoverBorderColor, hoverProgress);

                // Fill the background
                using (SolidBrush brush = new SolidBrush(currentBackColor))
                {
                    g.FillRectangle(brush, rect);
                }

                // Draw the border with or without rounded corners based on configuration
                using (Pen pen = new Pen(currentBorderColor, config.BorderWidth))
                {
                    if (config.EnableBorderRadius)
                    {
                        // Draw with rounded corners
                        using (GraphicsPath path = RoundedRect(rect, config.BorderRadius))
                        {
                            g.DrawPath(pen, path);
                        }
                    }
                    else
                    {
                        // Draw without rounded corners (normal rectangle)
                        g.DrawRectangle(pen, 1, 1, rect.Width - 2, rect.Height - 2);
                    }
                }

                // Draw the button text
                TextRenderer.DrawText(g, config.Button.Text, config.Button.Font, rect, currentTextColor,
                                      TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            // Set up MouseEnter and MouseLeave events to start the hover animation
            config.Button.MouseEnter += (s, e) =>
            {
                isHovered = true;
                hoverTimer.Start();
            };

            config.Button.MouseLeave += (s, e) =>
            {
                isHovered = false;
                hoverTimer.Start();
            };

            // Timer event to handle the easing effect
            hoverTimer.Interval = 15;
            hoverTimer.Tick += (s, e) =>
            {
                if (isHovered && hoverProgress < 1f)
                {
                    hoverProgress += config.EasingSpeed; // Use the provided easing speed
                    if (hoverProgress > 1f) hoverProgress = 1f;
                }
                else if (!isHovered && hoverProgress > 0f)
                {
                    hoverProgress -= config.EasingSpeed;
                    if (hoverProgress < 0f) hoverProgress = 0f;
                }

                if (hoverProgress == 0f || hoverProgress == 1f)
                {
                    hoverTimer.Stop();
                }

                config.Button.Invalidate(); // Redraw the button with updated progress
            };
        }


        // Helper method to interpolate between two colors
        private Color InterpolateColor(Color startColor, Color endColor, float progress)
        {
            int r = (int)(startColor.R + (endColor.R - startColor.R) * progress);
            int g = (int)(startColor.G + (endColor.G - startColor.G) * progress);
            int b = (int)(startColor.B + (endColor.B - startColor.B) * progress);
            return Color.FromArgb(r, g, b);
        }

        // Helper function to create a rounded rectangle path
        private GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r = radius * 2F;
            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.X + rect.Width - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.X + rect.Width - r, rect.Y + rect.Height - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - r, r, r, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}

