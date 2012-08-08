using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EvilDicom.Helper
{
    public static class Extensions
    {
        public static unsafe long IndexOf(this byte[] Haystack, byte[] Needle)
        {
            fixed (byte* H = Haystack) fixed (byte* N = Needle)
            {
                long i = 0;
                for (byte* hNext = H, hEnd = H + Haystack.LongLength; hNext < hEnd; i++, hNext++)
                {
                    bool Found = true;
                    for (byte* hInc = hNext, nInc = N, nEnd = N + Needle.LongLength; Found && nInc < nEnd; Found = *nInc == *hInc, nInc++, hInc++) ;
                    if (Found) return i;
                }
                return -1;
            }
        }

        public static unsafe List<long> IndexesOf(this byte[] Haystack, byte[] Needle)
        {
            List<long> Indexes = new List<long>();
            fixed (byte* H = Haystack) fixed (byte* N = Needle)
            {
                long i = 0;
                for (byte* hNext = H, hEnd = H + Haystack.LongLength; hNext < hEnd; i++, hNext++)
                {
                    bool Found = true;
                    for (byte* hInc = hNext, nInc = N, nEnd = N + Needle.LongLength; Found && nInc < nEnd; Found = *nInc == *hInc, nInc++, hInc++) ;
                    if (Found) Indexes.Add(i);
                }
                return Indexes;
            }
        }

    }
}


//Copyright © 2012 Rex Cardan, Ph.D


