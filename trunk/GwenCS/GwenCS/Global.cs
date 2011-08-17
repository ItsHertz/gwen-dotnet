﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Gwen.Controls;

namespace Gwen
{
    public static class Global
    {
        public static Base HoveredControl;
        public static Base KeyboardFocus;
        public static Base MouseFocus;

        public const int MaxCoord = 4096; // added here from various places in code

        public static int Round(double x)
        {
            return (int)Math.Round(x, MidpointRounding.AwayFromZero);
        }

        public static int Trunc(double x)
        {
            return (int)Math.Truncate(x);
        }

        public static int Ceil(double x)
        {
            return (int)Math.Ceiling(x);
        }

        public static Rectangle FloatRect(double x, double y, double w, double h)
        {
            return new Rectangle(Trunc(x), Trunc(y), Trunc(w), Trunc(h));
        }

        public static int Clamp(int x, int min, int max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }

        public static float Clamp(float x, float min, float max)
        {
            if (x < min)
                return min;
            if (x > max)
                return max;
            return x;
        }

        public static Rectangle ClampRectToRect(Rectangle inside, Rectangle outside, bool clampSize = false)
        {
            if (inside.X < outside.X)
                inside.X = outside.X;

            if (inside.Y < outside.Y)
                inside.Y = outside.Y;

            if (inside.Right > outside.Right)
            {
                if (clampSize)
                    inside.Width = outside.Width;
                else
                    inside.X = outside.Right - inside.Width;
            }
            if (inside.Bottom > outside.Bottom)
            {
                if (clampSize)
                    inside.Height = outside.Height;
                else
                    inside.Y = outside.Bottom - inside.Height;
            }

            return inside;
        }

        // from http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv
        public static HSV ToHSV(this Color color)
        {
            HSV hsv = new HSV();
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hsv.h = color.GetHue();
            hsv.s = (max == 0) ? 0 : 1f - (1f * min / max);
            hsv.v = max / 255f;

            return hsv;
        }

        public static Color HSVToColor(float h, float s, float v)
        {
            int hi = Convert.ToInt32(Math.Floor(h / 60)) % 6;
            double f = h / 60 - Math.Floor(h / 60);

            v = v * 255;
            int va= Convert.ToInt32(v);
            int p = Convert.ToInt32(v * (1 - s));
            int q = Convert.ToInt32(v * (1 - f * s));
            int t = Convert.ToInt32(v * (1 - (1 - f) * s));

            if (hi == 0)
                return Color.FromArgb(255, va, t, p);
            if (hi == 1)
                return Color.FromArgb(255, q, va, p);
            if (hi == 2)
                return Color.FromArgb(255, p, va, t);
            if (hi == 3)
                return Color.FromArgb(255, p, q, va);
            if (hi == 4)
                return Color.FromArgb(255, t, p, va);
            return Color.FromArgb(255, va, p, q);
        }

        // can't create extension operators
        public static Color Subtract(this Color color, Color other)
        {
            return Color.FromArgb(color.A - other.A, color.R - other.R, color.G - other.G, color.B - other.B);
        }

        public static Color Add(this Color color, Color other)
        {
            return Color.FromArgb(color.A + other.A, color.R + other.R, color.G + other.G, color.B + other.B);
        }

        public static Color Multiply(this Color color, float amount)
        {
            return Color.FromArgb(color.A, Trunc(color.R*amount), Trunc(color.G*amount), Trunc(color.B*amount));
        }
    }
}