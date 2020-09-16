using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SikiaSharpTraining
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    { int arcLength = 100;
        SKPath path = new SKPath(); SKPath path2 = new SKPath();
        public MainPage()
        {
            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1 / 60f), () =>
            {
                canvasView.InvalidateSurface();
                return true;
            });
        }

        private SKPaint GetPaintColor(SKPaintStyle style, string hexColor, float strokeWidth = 0, SKStrokeCap cap = SKStrokeCap.Round, bool IsAntialias = true)
        {
            return new SKPaint
            {
                Style = style,
                StrokeWidth = strokeWidth,
                Color = string.IsNullOrWhiteSpace(hexColor) ? SKColors.White : SKColor.Parse(hexColor),
                StrokeCap = cap,
                IsAntialias = IsAntialias
            };
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            SKPaint StrokePaint = GetPaintColor(SKPaintStyle.Stroke, "#ffffff",4);
            SKPaint bgPaint = GetPaintColor(SKPaintStyle.Fill, "#1ab7ea");
            SKPaint hrPaint = GetPaintColor(SKPaintStyle.Stroke, "#000000", 5);
            SKPaint minPaint = GetPaintColor(SKPaintStyle.Stroke, "#000000", 3);
            SKPaint secPaint = GetPaintColor(SKPaintStyle.Stroke, "#ff0084", 1);
            SKPaint dotPaint = GetPaintColor(SKPaintStyle.Fill, "#ff0084");

            canvas.Clear();

            SKRect bgRect = new SKRect(25, 25, info.Width - 25, info.Height - 25);
            SKRect arcRect = new SKRect(10,10,info.Width-10,info.Height-10);

            canvas.DrawOval(bgRect,bgPaint);
            path.ArcTo(arcRect, 0, arcLength, true); path2.ArcTo(arcRect, 180, arcLength, true);

            canvas.DrawPath(path, StrokePaint);
            canvas.DrawPath(path2, StrokePaint);

           
            canvas.Translate(info.Width/2, info.Height/2);
            
            canvas.Scale(info.Width / 200f);
          

            canvas.Save();
            canvas.RotateDegrees(240);
            canvas.DrawCircle(0, -75, 2, dotPaint);
            canvas.Restore();

            canvas.Save();
            //hour
            DateTime date = DateTime.Now;
            canvas.RotateDegrees(30*date.Hour + date.Minute / 2f);
            canvas.DrawLine(0, 0, 0, -50, hrPaint);
            canvas.Restore();


            //min
            canvas.Save();
            canvas.RotateDegrees(date.Minute + date.Second / 10f);
            canvas.DrawLine(0, 0, 0, -70,minPaint);
            canvas.Restore();
            //sec
            canvas.Save();
            canvas.RotateDegrees(6*date.Second);
            canvas.DrawLine(0, 0, 0, -70, secPaint);
            canvas.DrawCircle(0, 0, 4, dotPaint);
            canvas.Restore();

            

        }
    }
}
