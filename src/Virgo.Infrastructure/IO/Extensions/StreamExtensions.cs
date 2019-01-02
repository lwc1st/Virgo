﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Virgo.Infrastructure.IO.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] GetAllBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
