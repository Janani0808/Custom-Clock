using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
 
namespace CustomClockExample
{
    public partial class MainPage : PhoneApplicationPage
    {
        Point gridCenter;
        Size textSize;
        double scale;
 
        public MainPage()
        {
            InitializeComponent();
 
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(1);
            tmr.Tick += OnTimerTick;
            tmr.Start();
        }
 
        void OnContentPanelSizeChanged(object sender, SizeChangedEventArgs args)
        {
            gridCenter = new Point(args.NewSize.Width / 2,
                                   args.NewSize.Height / 2);
 
            textSize = new Size(referenceText.ActualWidth,
                                referenceText.ActualHeight);
 
            scale = Math.Min(gridCenter.X, gridCenter.Y) / textSize.Width;
 
            UpdateClock();
        }
 
        void OnTimerTick(object sender, EventArgs e)
        {
            UpdateClock();
        }
 
        void UpdateClock()
        {
            DateTime dt = DateTime.Now;
            double angle = 6 * dt.Second;
            SetupHand(secondHand, "SECONDS " + dt.Second, angle);
            angle = 6 * dt.Minute + angle / 60;
            SetupHand(minuteHand, "MINUTE " + dt.Minute, angle);
            angle = 30 * (dt.Hour % 12) + angle / 12;
            SetupHand(hourHand, "HOUR " + (((dt.Hour + 11) % 12) + 1), angle);
        }
 
        void SetupHand(TextBlock txtblk, string text, double angle)
        {
            txtblk.Text = text;
            CompositeTransform xform = txtblk.RenderTransform as CompositeTransform;
            xform.CenterX = textSize.Height / 2;
            xform.CenterY = textSize.Height / 2;
            xform.ScaleX = scale;
            xform.ScaleY = scale;
            xform.Rotation = angle - 90;
            xform.TranslateX = gridCenter.X - textSize.Height / 2;
            xform.TranslateY = gridCenter.Y - textSize.Height / 2;
        }
    }
}
