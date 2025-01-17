﻿using System.IO;
using System.Text;

public static class MLT
{
    public static void Extract(string inFile, string outDir, in OSBExtract.OSBExtract.Options opts)
    {

        using (FileStream mlt_file = new FileStream(inFile, FileMode.Open))
        {
            bool isMLT = false;
            long startPos = 0;

            using (BinaryReader reader = new BinaryReader(mlt_file))
            {
                // Make sure this is an MLT
                //We fall through and properly close the FileStream and BinaryReader if it's not an MLT
                if (Encoding.UTF8.GetString(reader.ReadBytes(4), 0, 4) == "SMLT")
                {
                    isMLT = true;
                    //Read the header info and store it for analysis when debugging
                    byte[] unknown_flag1 = reader.ReadBytes(4);
                    byte[] unknown_flag2 = reader.ReadBytes(4);
                    byte[] padding = reader.ReadBytes(20);
                    startPos = reader.BaseStream.Position;
                }
            }

            if(isMLT){
                OSB.Extract(startPos, inFile, outDir, in opts);
            }
        }
    }
}
