using System;
using RadarGraphProject;
using UIKit;

namespace RadarGraphTest
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var radarGraph = new RadarGraph(UIScreen.MainScreen.Bounds, new Source());
            View.AddSubview(radarGraph);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }

    public class Source : RadarGraphSource
    {
        private readonly string[] dataLables = { "C#", "JS", "Objective-C", "Python", "Go", "C++", "Haskell", "Prolog" };
        private readonly int[,] dataValues = { { 5, 75, 60, 40, 90, 20, 35, 50 }, { 20, 15, 40, 90, 20, 75, 30, 40 }};
        private readonly UIColor[] colorValues = { UIColor.Red, UIColor.Green };

        public override int GetNumberOfRaysInGraph()
        {
            return 8;
        }


        public override int GetNumberOfNetStepsInGraph()
        {
            return 4;
        }

        public override int GetNumberOfLayersInGraph()
        {
            return 2;
        }

        public override string GetNameForRay(int ray)
        {
            return dataLables[ray];
        }

        public override UIColor GetColorForLayer(int layer)
        {
            return colorValues[layer];
        }

        public override UIColor GetColorForNet()
        {
            return UIColor.DarkGray;
        }

        public override UIColor GetColorForGraphBackground()
        {
            return UIColor.White;
        }

        public override int GetDatavalueForRayInLayer(int ray, int layer)
        {
            return dataValues[layer, ray];
        }

        public override int GetPaddingOfGraphInBounds()
        {
            return 80;
        }

        public override int GetMaxDatavalueForRays()
        {
            return 100;
        }
    }
}
