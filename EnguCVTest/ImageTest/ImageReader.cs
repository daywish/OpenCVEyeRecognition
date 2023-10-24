using OpenCvSharp;

namespace EnguCVTest.ImageTest
{
    public class ImageReader
    {
        public static void ImageDisplay(string source)
        {
            using (var img = new Mat(source, ImreadModes.Unchanged))
            {
                using (var newImg = new Mat())
                {
                    Cv2.Canny(img, newImg, 50, 200);
                    using (new Window("Source", img))
                    {
                        using (new Window("result", newImg))
                        {
                            Cv2.WaitKey();
                        }
                    }
                }
            }
        }
    }
}
