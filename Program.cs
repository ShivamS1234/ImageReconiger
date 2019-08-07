using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DescribeImageSample
{
    public class Program
    {

        public static string ImageData;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            try
            {
                Console.WriteLine("Enter your Cognitive Services Vision API Key:");
                var apiKey = "db2c765d316648af88bda0557daa58cb";
                Console.WriteLine();

                Console.WriteLine("Enter the url of the image you want analyze:");



                var imageUrl = "/Users/virtualemployee/Downloads/Cognitive-Vision-DotNetCore-master/Samples/DescribeImageSample/IMG_0042.PNG";
                Console.WriteLine();

                DescribeImage(apiKey, imageUrl).Wait();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        public static async Task DescribeImage(string apiKey, string imageUrl, Stream streamImage = null)
        {
            try
            {
                ImageData = null;
                Console.WriteLine("Run Image Recongize..:");
                var apiRoot = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0";

                if (streamImage == null)
                {
                    streamImage = File.OpenRead(imageUrl);
                }

                var VisionServiceClient = new VisionServiceClient(apiKey, apiRoot);

                var analysisResult = await VisionServiceClient.RecognizeTextAsync(streamImage);
                LogOcrResults(analysisResult);
                Console.WriteLine("Exit Image Recongize..:");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exit Image Recongize..:");
                Console.WriteLine();
            }
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
            ImageData = message;
        }

        private static void LogOcrResults(OcrResults results)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            StringBuilder stringBuilder = new StringBuilder();

            if (results != null && results.Regions != null)
            {
                stringBuilder.Append("Text: ");
                stringBuilder.AppendLine();
                foreach (var item in results.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }

            Log(stringBuilder.ToString());
        }
    }
}