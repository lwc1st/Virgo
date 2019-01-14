﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Virgo.Extensions
{
    /// <summary>
    /// <see cref="Stream"/>的扩展方法
    /// </summary>
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
