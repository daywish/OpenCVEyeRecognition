namespace EnguCVTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            string source = "E:\\test\\EnguCVTest\\EnguCVTest\\img\\senjumaru.jpg";
            string faceFrontal = "E:\\test\\EnguCVTest\\EnguCVTest\\img\\ryan.jpg";
            ImageTest.ImageReader.ImageDisplay(source);
            //CameraTest.VideoCaptureTest.Capture();
            //FaceRecognitionTest.CascadeMethod.UseCascadeClassifier(faceFrontal);
            //FaceRecognitionTest.CascadeMethod.CameraCascadeClassifier();
            //EyeRecognitionTest.CascadeMethod.UseCascadeClassifier(faceFrontal);
            //EyeRecognitionTest.CascadeMethod.CameraCascadeClassifier();
            //CombinatedRecognitionTest.CascadeMethod.UseCascadeClassifier(faceFrontal);
            //CombinatedRecognitionTest.CascadeMethod.CameraCascadeClassifier();
        }
    }
}
