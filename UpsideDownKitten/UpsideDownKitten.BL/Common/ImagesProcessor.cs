using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace UpsideDownKitten.BL.Common
{
    public static class ImagesProcessor
    {
        public static byte[] Rotate(byte[] data) => Modify(data, x => x.RotateFlip(RotateMode.Rotate180, FlipMode.None));
        public static byte[] BlackWhite(byte[] data) => Modify(data, x => x.BlackWhite());
        public static byte[] Blur(byte[] data) => Modify(data, x => x.GaussianBlur());

        private static byte[] Modify(byte[] data, Action<IImageProcessingContext> operation)
        {
            using (var image = Image.Load(data, out var imageFormat))
            {
                image.Mutate(operation);
                using (var ms = new MemoryStream())
                {
                    image.Save(ms, imageFormat);
                    return ms.ToArray();
                }
            }
        }
    }
}
