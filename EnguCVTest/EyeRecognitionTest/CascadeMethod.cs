using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using OpenCvSharp.ML;

namespace EnguCVTest.EyeRecognitionTest
{
    public class CascadeMethod
    {
        private static string xmlPathDefaultEyes = "E:\test\\EnguCVTest\\EnguCVTest\\CascadeUtil\\Haarcascades\\haarcascade_eye.xml";
        private static string xmlPathEyesWithGlasses = "E:\\test\\EnguCVTest\\EnguCVTest\\CascadeUtil\\Haarcascades\\haarcascade_eye_tree_eyeglasses.xml";

        public static void UseCascadeClassifier(string source)
        {
            using (CascadeClassifier eyeCascade = new CascadeClassifier(xmlPathEyesWithGlasses))
            {
                using (Mat image = new Mat(source))
                {
                    Rect[] eyes = eyeCascade.DetectMultiScale(image, scaleFactor: 1.1, minNeighbors: 5, minSize: new Size(30, 30));

                    foreach (Rect eye in eyes)
                    {
                        // Draw a rectangle around each detected eye
                        Cv2.Rectangle(image, eye, Scalar.Red, 2);
                    }

                    using (new Window("Result", image))
                    {
                        // Check for user input to exit the loop (e.g., press 'Esc' key)
                        Cv2.WaitKey();
                    }
                }
            }
        }

        public static void CameraCascadeClassifier()
        {
            using (VideoCapture capture = new VideoCapture(0))
            {
                if (!capture.IsOpened())
                {
                    Console.WriteLine("Error: Camera not found or could not be opened.");
                    return;
                }

                using (Window window = new Window("Eye Detection"))
                {
                    // Load the Haar Cascade classifier for eye detection
                    using (CascadeClassifier eyeCascade = new CascadeClassifier(xmlPathEyesWithGlasses))
                    {
                        while (true)
                        {
                            using (Mat frame = new Mat())
                            {
                                capture.Read(frame); // Read a frame from the camera

                                // Detect eyes in the frame using the Haar Cascade classifier
                                Rect[] eyes = eyeCascade.DetectMultiScale(frame, scaleFactor: 1.1, minNeighbors: 5, minSize: new Size(30, 30));

                                foreach (Rect eye in eyes)
                                {
                                    // Draw a rectangle around each detected eye
                                    Cv2.Rectangle(frame, eye, Scalar.Red, 2);
                                }

                                // Display the frame with detected eyes
                                window.ShowImage(frame);

                                // Check for user input to exit the loop (e.g., press 'Esc' key)
                                if (Cv2.WaitKey(1) == 27)
                                    break;
                            }
                        }
                    }
                }
            }
        }

    }
}
