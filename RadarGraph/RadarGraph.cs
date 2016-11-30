using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;

namespace RadarGraphProject
{
    public class RadarGraph : UIView
    {
        private readonly IRadarGraphSource source;

        private readonly int numberOfNetSteps;

        private readonly int numberOfDataPoints;

        private readonly int height;

        private readonly int width;

        private readonly int maxGraphWidth;

        public RadarGraph(CGRect rect, IRadarGraphSource source) : base(rect)
        {
            this.height = (int)rect.Height;
            this.width = (int)rect.Width;

            this.source = source;
            this.numberOfDataPoints = source.GetNumberOfRaysInGraph();
            this.numberOfNetSteps = source.GetNumberOfNetStepsInGraph();
            this.maxGraphWidth = (this.width < this.height) ? this.width - source.GetPaddingOfGraphInBounds() : this.height - source.GetPaddingOfGraphInBounds();
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            var currentContext = UIGraphics.GetCurrentContext();

            DrawBackground(currentContext);
            DrawNet(currentContext);
            DrawRays(currentContext);
            DrawData(currentContext);

            DrawLabels();
        }

        public void DrawBackground(CGContext drawingContext)
        {
            source.GetColorForGraphBackground().SetFill();
            drawingContext.FillRect(this.Bounds);
        }


        private void DrawNet(CGContext drawingContext)
        {
            // set line parameter for drawing
            drawingContext.SetLineWidth(1);

            // set color parameters for drawing
            UIColor.Clear.SetFill();
            source.GetColorForNet().SetStroke();
        
            for (int j = 1; j < numberOfNetSteps + 2; j++)
            {
                var radius = (j * (maxGraphWidth / (numberOfNetSteps + 1))) / 2;

                // calculate points for polygon current net step
                var points = new List<CGPoint>();
                for (int i = 0; i < numberOfDataPoints + 1; i++)
                {
                    var x = radius * Math.Cos(2 * Math.PI * i / numberOfDataPoints) + (width / 2);
                    var y = radius * Math.Sin(2 * Math.PI * i / numberOfDataPoints) + (height / 2);
                    points.Add(new CGPoint(x, y));
                }

                // generate path to draw net step
                var path = new CGPath();
                path.AddLines(points.ToArray());

                //draw path
                drawingContext.AddPath(path);
                drawingContext.DrawPath(CGPathDrawingMode.Stroke);
            }
        }

        private void DrawRays(CGContext drawingContext)
        {
            var radius = maxGraphWidth / 2;

            for (int i = 0; i < numberOfDataPoints; i++)
            { 
                var x = radius * Math.Cos(2 * Math.PI * i / (numberOfDataPoints)) + width / 2;
                var y = radius * Math.Sin(2 * Math.PI * i / (numberOfDataPoints)) + height / 2;

                // generate path to draw net step
                var path = new CGPath();
                path.AddLines(new CGPoint[] { new CGPoint(width / 2, height / 2), new CGPoint(x, y) });

                //draw path
                drawingContext.AddPath(path);
                drawingContext.DrawPath(CGPathDrawingMode.Stroke);
            }
        }

        private void DrawData(CGContext drawingContext)
        {
            for (int j = 0; j < source.GetNumberOfLayersInGraph(); j++)
            {
                var points = new List<CGPoint>();

                source.GetColorForLayer(j).SetStroke();
                source.GetColorForLayer(j).ColorWithAlpha(0.4f).SetFill();
                drawingContext.SetLineWidth((nfloat) source.GetLineWidthForLayers());

                // generate path to draw net step
                var path = new CGPath();
                for (int i = 0; i < numberOfDataPoints + 1; i++)
                {
                    var currentRay = (i == numberOfDataPoints) ? 0 : i;

                    var radius = (double)(source.GetDatavalueForRayInLayer(currentRay, j) / (double)source.GetMaxDatavalueForRays()) * ((double)this.maxGraphWidth / 2);

                    var x = radius * Math.Cos(2 * Math.PI * i / (this.numberOfDataPoints)) + this.width / 2;
                    var y = radius * Math.Sin(2 * Math.PI * i / (this.numberOfDataPoints)) + this.height / 2;

                    points.Add(new CGPoint(x, y));
                }
                path.AddLines(points.ToArray());

                //draw path
                drawingContext.AddPath(path);
                drawingContext.DrawPath(CGPathDrawingMode.FillStroke);
            }

            
        }

        private void DrawLabels()
        {
            var radius = (maxGraphWidth / 2) + source.GetLabelDistance();

            for (int i = 0; i < numberOfDataPoints; i++)
            {
                var x = radius * Math.Cos(2 * Math.PI * i / (numberOfDataPoints)) + width / 2;
                var y = radius * Math.Sin(2 * Math.PI * i / (numberOfDataPoints)) + height / 2;

                // generate path to draw net step
                var path = new CGPath();
                path.AddLines(new CGPoint[] { new CGPoint(width / 2, height / 2), new CGPoint(x, y) });

                var rayLabel = new UILabel();
                rayLabel.Text = source.GetNameForRay(i);
                rayLabel.TextAlignment = UITextAlignment.Center;
                rayLabel.SizeToFit();
                rayLabel.Center = new CGPoint(x, y);

                rayLabel.BackgroundColor = source.GetLabelBackgroundColor();
                rayLabel.TextColor = source.GetColorForNet();

                AddSubview(rayLabel);
            }
        }
    }
}
