using System;
using UIKit;

namespace RadarGraphProject
{
    public abstract class RadarGraphSource
    {
        public abstract int GetNumberOfRaysInGraph();

        public abstract int GetNumberOfNetStepsInGraph();

        public abstract int GetNumberOfLayersInGraph();

        public abstract string GetNameForRay(int ray);

        public abstract UIColor GetColorForLayer(int layer);

        public abstract UIColor GetColorForNet();

        public abstract UIColor GetColorForGraphBackground();

        public abstract int GetMaxDatavalueForRays();

        public abstract int GetDatavalueForRayInLayer(int ray, int layer);

        public abstract int GetPaddingOfGraphInBounds();

        public int GetLineWidthForLayers()
        {
            return 3;
        }

        public int GetLabelDistance()
        {
            return 20;
        }

        public UIColor GetLablBackgroundColor()
        {
            return UIColor.Clear;
        }
    }
}
