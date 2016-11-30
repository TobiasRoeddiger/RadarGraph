using UIKit;

namespace RadarGraphProject
{
    public abstract class IRadarGraphSource
    {
        public abstract int GetNumberOfRaysInGraph();

        public abstract int GetNumberOfLayersInGraph();

        public abstract string GetNameForRay(int ray);

        public abstract UIColor GetColorForLayer(int layer);

        public abstract UIColor GetColorForNet();

        public abstract UIColor GetColorForGraphBackground();

        public abstract int GetMaxDatavalueForRays();

        public abstract int GetDatavalueForRayInLayer(int ray, int layer);

        public int GetPaddingOfGraphInBounds()
        {
            return 80;
        }

        public int GetNumberOfNetStepsInGraph()
        {
            return 4;
        }

        public int GetLineWidthForLayers()
        {
            return 3;
        }

        public int GetLabelDistance()
        {
            return 20;
        }

        public UIColor GetLabelBackgroundColor()
        {
            return UIColor.Clear;
        }
    }
}
