using OpenCvSharp;

namespace EnguCVTest.CameraTest
{
    public class VideoCaptureTest
    {
        public static void Capture()
        {
            VideoCapture capture = new VideoCapture(0);
            using (Window window = new Window("Camera"))
            {
                using (Mat image = new Mat()) // Frame image buffer
                {
                    // When the movie playback reaches end, Mat.data becomes NULL.
                    while (true)
                    {
                        capture.Read(image); // same as cvQueryFrame
                        if (image.Empty()) break;
                        window.ShowImage(image);
                        Cv2.WaitKey(30);
                    }
                }
            }
        }
    }
}
