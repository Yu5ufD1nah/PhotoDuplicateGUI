using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using Size = OpenCvSharp.Size;
namespace PhotoDuplicateGUI;

public class ImageHasher
{
    public static ulong ComputePHash(string filePath)
    {
       using var src = Cv2.ImRead(filePath, ImreadModes.Grayscale);

    // Step 1: Resize the image to 32x32
    using var resized = new Mat();
    Cv2.Resize(src, resized, new Size(32, 32));

    // Step 2: Convert to CV_32F (32-bit floating-point)
    using var resizedFloat = new Mat();
    resized.ConvertTo(resizedFloat, MatType.CV_32F);

    // Step 3: Compute the DCT (Discrete Cosine Transform)
    using var dct = new Mat();
    Cv2.Dct(resizedFloat, dct);

    // Step 4: Extract the top-left 8x8 region of the DCT result
    var dctSmall = dct.RowRange(0, 8).ColRange(0, 8);

    // Step 5: Convert the 8x8 DCT block into a float array
    float[] dctValues = new float[8 * 8];
    dctSmall.GetArray(out dctValues);

    // Step 6: Compute the mean value of the 8x8 DCT block
    double mean = dctValues.Average();

    // Step 7: Generate the hash by comparing each DCT value with the mean
    ulong hash = 0;
    for (int i = 0; i < dctValues.Length; i++)
    {
        if (dctValues[i] > mean)
            hash |= (1UL << i);
    }

    return hash;
}


    public static double CompareHashes(ulong hash1, ulong hash2)
    {
        // Compute Hamming distance
        ulong xor = hash1 ^ hash2;
        int hammingDistance = 0;

        while (xor > 0)
        {
            hammingDistance += (int)(xor & 1);
            xor >>= 1;
        }

        // Convert Hamming distance to a similarity score (1.0 = identical, 0.0 = completely different)
        return 1.0 - (hammingDistance / 64.0);
    }
}

