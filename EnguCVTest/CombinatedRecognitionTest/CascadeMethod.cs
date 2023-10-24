using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using OpenCvSharp;

namespace EnguCVTest.CombinatedRecognitionTest
{
    public class CascadeMethod
    {
        private static string xmlPathFace = "E:\\test\\opencv2\\data\\haarcascades_cuda\\haarcascade_frontalface_default.xml";
        private static string xmlPathEyes = "E:\\test\\opencv2\\data\\haarcascades_cuda\\haarcascade_eye.xml";

        public static void UseCascadeClassifier(string source)
        {
            using (CascadeClassifier faceCascade = new CascadeClassifier(xmlPathFace))
            {
                using (Mat image = new Mat(source))
                {
                    Rect[] faces = CascadeUtil.CascadeFinder.FindRect(image, faceCascade);

                    using (CascadeClassifier eyeCascade = new CascadeClassifier(xmlPathEyes))
                    {
                        foreach (var face in faces)
                        {
                            DrawFaceAndEyes(image, face, eyeCascade);
                        }
                    }
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

                using (Window window = new Window("Eye Detection"))
                {
                    while (true)
                    {
                        using (Mat frame = new Mat())
                        {
                            capture.Read(frame);

                            using (CascadeClassifier faceCascade = new CascadeClassifier(xmlPathFace))
                            {
                                using (CascadeClassifier eyeCascade = new CascadeClassifier(xmlPathEyes))
                                {
                                    Rect[] faces = CascadeUtil.CascadeFinder.FindRect(frame, faceCascade);

                                    for (int i = 0; i < faces.Length; i++)
                                    {
                                        if (i == 0 || i != 0 && !faces[i].IntersectsWith(faces[i - 1]))
                                        {
                                            DrawFaceAndEyes(frame, faces[i], eyeCascade);
                                        }
                                    }

                                }
                            }

                            window.ShowImage(frame);

                            if (Cv2.WaitKey(1) == 27)
                                break;
                        }
                    }
                }
            }
        }

        private static void DrawFaceAndEyes(in Mat frame, Rect face, CascadeClassifier eyeCascade)
        {
            CascadeUtil.CascadeFinder.DrawRect(frame, face);

            using (Mat faceRegion = frame.SubMat(face))
            {
                Rect[] eyes = CascadeUtil.CascadeFinder.FindRect(faceRegion, eyeCascade);

                CascadeUtil.CascadeFinder.DrawRects(in faceRegion, eyes);
            }
        }
    }
}
