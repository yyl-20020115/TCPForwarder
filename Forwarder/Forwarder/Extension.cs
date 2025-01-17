﻿using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;

//Copyright(c) 2018 Iván Dominguez (XWolf Override)
//
//This software is provided 'as-is', without any express or implied
//warranty.In no event will the authors be held liable for any damages
//arising from the use of this software.
//
//Permission is granted to anyone to use this software for any purpose,
//including commercial applications, and to alter it and redistribute it
//freely, subject to the following restrictions:
//
//1. The origin of this software must not be misrepresented; you must not
//   claim that you wrote the original software.If you use this software
//   in a product, an acknowledgment in the product documentation would be
//   appreciated but is not required.
//2. Altered source versions must be plainly marked as such, and must not be
//   misrepresented as being the original software.
//3. This notice may not be removed or altered from any source distribution.

namespace Forwarder;

static class Utils
{
    private const int MAX_DIALOG_PREVIEW = 256;
    public static bool IsConnected(this Socket socket)
    {
        try
        {
            return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
        }
        catch (SocketException) { 
            return false; 
        }
    }

    public static string FBytes(int bytes)
    {
        if (bytes < 1024)
            return bytes.ToString()+"b";
        string tail = null;
        double b = bytes;
        if (b > 1024)
        {
            tail = "Kb";
            b /= 1024;
        }
        if (b > 1024)
        {
            tail = "Mb";
            b /= 1024;
        }
        if (b > 1024)
        {
            tail = "Gb";
            b /= 1024;
        }
        return b.ToString("#0.00") + tail;
    }

    public static string FTime(TimeSpan ts)
    {
        var result = new StringBuilder();
        if (ts.Days > 0)
        {
            result.Append(ts.Days);
            result.Append("d ");
        }
        if (ts.Hours > 0)
        {
            result.Append(ts.Hours);
            result.Append("h ");
        }
        if (ts.Minutes > 0)
        {
            result.Append(ts.Minutes);
            result.Append("m ");
        }
        if (ts.Seconds > 0)
        {
            result.Append(ts.Seconds);
            result.Append("s ");
        }
        if (ts.Milliseconds > 0 || result.Length == 0)
        {
            result.Append(ts.Milliseconds);
            result.Append("ms ");
        }
        return result.ToString();
    }

    public static string BinToTextSample(byte[] data) =>
        (data == null || data.Length == 0)
            ? ""
            : string.Join(" ", data.Select(d => $"{d:X2}").ToArray());
}
