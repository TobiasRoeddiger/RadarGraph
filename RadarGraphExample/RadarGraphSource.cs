using RadarGraphProject;
using UIKit;

namespace RadarGraphTest
{
    public class RadarGraphSource : IRadarGraphSource
    {
        private readonly string[] dataLables = { "C#", "JS", "Objective-C", "Python", "Go", "C++", "Haskell", "Prolog" };
        private readonly int[,] dataValues = { { 5, 75, 60, 40, 90, 20, 35, 50 }, { 20, 15, 40, 90, 20, 75, 30, 40 } };
        private readonly UIColor[] colorValues = { UIColor.Red, UIColor.Green };

        public override int GetNumberOfRaysInGraph()
        {
            return 8;
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

        public override int GetMaxDatavalueForRays()
        {
            return 100;
        }
    }
}
