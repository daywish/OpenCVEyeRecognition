using Emgu.CV.Face;
using Emgu.CV.Structure;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnguCVTest.FaceRecognitionTest
{
    public class CascadeMethod
    {
        private static string xmlPath = "E:\\test\\EnguCVTest\\EnguCVTest\\CascadeUtil\\Haarcascades\\haarcascade_frontalface_default.xml";
        private static string xmlPath2 = "E:\\test\\opencv2\\data\\lbpcascades\\lbpcascade_frontalface.xml";
        public static void UseCascadeClassifier(string source)
        {
            using (CascadeClassifier faceCascade = new CascadeClassifier(xmlPath2))
            {
                // Load the image containing faces
                using (Mat image = new Mat(source))
                {
                    Rect[] faces = FindFaceRect(image, faceCascade);

                    DrawRects(image, faces);

                    using (new Window("Result", image))
                    {
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

                using (Window window = new Window("Face Recognition"))
                {
                    while (true)
                    {
                        using (Mat frame = new Mat())
                        {
                            capture.Read(frame); // Read a frame from the camera

                            // Detect faces in the frame using Haar Cascade
                            using (CascadeClassifier faceCascade = new CascadeClassifier(xmlPath2))
                            {
                                Rect[] faces = faceCascade.DetectMultiScale(frame, scaleFactor: 1.1, minNeighbors: 5, minSize: new Size(30, 30));

                                foreach (Rect face in faces)
                                {
                                    // Draw a rectangle around each detected face
                                    Cv2.Rectangle(frame, face, Scalar.Red, 2);

                                    // Crop the detected face region
                                    Mat detectedFace = new Mat(frame, face);

                                    // You can now process the detected face for recognition
                                    // Implement recognition logic here
                                }
                            }

                            // Display the frame with detected faces
                            window.ShowImage(frame);

                            // Check for user input to capture a photo (e.g., press 'q' key)
                            if (Cv2.WaitKey(1) == 'q')
                            {
                                // Capture a photo
                                Mat capturedPhoto = frame.Clone();

                                // Save the captured photo to a file
                                capturedPhoto.SaveImage("captured_photo.jpg");

                                Console.WriteLine("Photo captured.");
                            }
                        }

                        // Check for user input to exit the loop (e.g., press 'Esc' key)
                        if (Cv2.WaitKey(1) == 27)
                            break;
                    }
                }
            }

        }

        private static Rect[] FindFaceRect(Mat frame, CascadeClassifier faceCascade)
        {
            using (Mat grey = new Mat())
            {
                Cv2.CvtColor(frame, grey, ColorConversionCodes.BGR2GRAY);

                Rect[] faces = faceCascade.DetectMultiScale(grey, scaleFactor: 1.1, minNeighbors: 5, minSize: new Size(30, 30));

                return faces;
            }
        }

        private static void DrawRects(in Mat frame, Rect[] rects)
        {
            foreach (var rect in rects)
            {
                Cv2.Rectangle(frame, rect, Scalar.Red, 2);
            }
        }
    }
}
