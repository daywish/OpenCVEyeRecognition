using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnguCVTest.CascadeUtil
{
    public class CascadeFinder
    {
        public static Rect[] FindRect(Mat frame, CascadeClassifier cascadeClassifier)
        {
            using (Mat grey = new Mat())
            {
                Cv2.CvtColor(frame, grey, ColorConversionCodes.BGR2GRAY);

                Rect[] rects = cascadeClassifier.DetectMultiScale(grey, scaleFactor: 1.1, minNeighbors: 5, minSize: new Size(30, 30));

                return rects;
            }
        }

        public static void DrawRects(in Mat frame, Rect[] rects)
        {
            foreach (var rect in rects)
            {
                Cv2.Rectangle(frame, rect, Scalar.Red, 2);
            }
        }

        public static void DrawRect(in Mat frame, Rect rect)
        {
            Cv2.Rectangle(frame, rect, Scalar.Red, 2);
        }
    }
}
