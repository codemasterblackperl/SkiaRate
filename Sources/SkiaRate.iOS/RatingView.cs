﻿using System;
using SkiaSharp;
using SkiaSharp.Views.iOS;
using UIKit;
using System.Linq;
using CoreGraphics;
using Foundation;
namespace SkiaRate
{
    public partial class RatingView : SKCanvasView
    {
        public event EventHandler<EventArgs> ValueChanged;

        public RatingView()
        {
            this.BackgroundColor = UIColor.Clear;
            this.PaintSurface += Handle_PaintSurface;
        }


        #region fields

        private nfloat scale = UIScreen.MainScreen.Scale;
        private double value = 0;
        private string path = PathConstants.Star;
        private int count = 5;
        private RatingType ratingType = RatingType.Floating;

        #endregion

        #region properties

        public double Value
        {
            get { return this.value; }
            set
            {
                if (value != this.value)
                {
                    this.value = this.ClampValue(value);
                    this.SetNeedsDisplayInRect(this.Bounds);
                    this.ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public string Path
        {
            get { return this.path; }
            set
            {
                if (value != this.path)
                {
                    this.path = value;
                    this.SetNeedsDisplayInRect(this.Bounds);
                }
            }
        }
        public int Count
        {
            get { return this.count; }
            set
            {
                if (value != this.count)
                {
                    this.count = value;
                    this.SetNeedsDisplayInRect(this.Bounds);
                }
            }
        }

        public UIColor ColorOn
        {
            get { return this.SKColorOn.ToUIColor(); }
            set 
            { 
                if (value.ToSKColor() != this.SKColorOn)
                {
                    this.SKColorOn = value.ToSKColor();
                    this.SetNeedsDisplayInRect(this.Bounds);
                }
            }
        }

        public UIColor OutlineOnColor
        {
            get { return this.SKOutlineOnColor.ToUIColor(); }
            set 
            { 
                if (value.ToSKColor() != this.SKOutlineOnColor)
                {
                    this.SKOutlineOnColor = value.ToSKColor();
                    this.SetNeedsDisplayInRect(this.Bounds);
                }
            }
        }

        public UIColor OutlineOffColor
        {
            get { return this.SKOutlineOffColor.ToUIColor(); }
            set 
            {
                if (value.ToSKColor() != this.SKOutlineOffColor)
                {
                    this.SKOutlineOffColor = value.ToSKColor();
                    this.SetNeedsDisplayInRect(this.Bounds);
                }
            }
        }

        public RatingType RatingType
        {
            get { return this.ratingType; }
            set 
            { 
                if (value != this.ratingType)
                {
                    this.ratingType = value;
                    this.SetNeedsDisplayInRect(this.Bounds);
                } 
            }
        }

        #endregion

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            this.HandleTouches(touches, evt);
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            this.HandleTouches(touches, evt);
        }

        private void HandleTouches(NSSet touches, UIEvent evt)
        {
            var touch = touches.FirstOrDefault() as UITouch;
            CGPoint point = touch.LocationInView(this);

            this.SetValue(point.X * this.scale, point.Y * this.scale);
            this.SetNeedsDisplayInRect(this.Bounds);
        }

        private void Handle_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            this.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
        }
    }
}
