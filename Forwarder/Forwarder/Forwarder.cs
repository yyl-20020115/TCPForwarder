using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

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

public class Forwarder
{
    private bool stop = false;
    private int currentConections;
    private int totalConnections;
    private IPEndPoint local;
    private IPEndPoint remote;
    private Thread listenerThread;

    #region Management
    public void Activate()
    {
        if (listenerThread != null)
            return;
        lock (this)
        {
            stop = false;
        }
        listenerThread = new Thread(Listen);
        listenerThread.Start();
    }

    public void Deactivate()
    {
        if (listenerThread == null)
            return;
        lock (this)
        {
            stop = true;
        }
        listenerThread.Join();
        Message(ForwarderMessage.FromDeactivation());
        listenerThread = null;
    }

    public void SetLocal(string host, string port)
    {
        Local = new IPEndPoint(GetAddress(host), int.Parse(port));
    }

    public void SetRemote(string host, string port)
    {
        Remote = new IPEndPoint(GetAddress(host), int.Parse(port));
    }

    public static IPAddress GetAddress(string host)
    {
        if (!IPAddress.TryParse(host, out var address))
        {
            var dest = Dns.GetHostEntry(host);
            if (dest.AddressList.Length > 0)
            {
                foreach (var ip in dest.AddressList)
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        address = ip;
                        break;
                    }
                if (address == null)
                    address = dest.AddressList[0];
            }
            else
                throw new Exception("Can't resolve '" + host + "'");
        }
        return address;
    }

    private void Message(ForwarderMessage msg)
    {
        WhenMessage?.Invoke(this, msg);
    }

    #endregion

    #region Listener

    private void Listen()
    {
        Thread.CurrentThread.Name = "Listener on " + local;
        bool running = true;
        var listener = new TcpListener(local);
        try
        {
            listener.Start();
        }
        catch (SocketException ex)
        {
            running = false;
            Message(ForwarderMessage.FromException(ex));
        }
        Message(ForwarderMessage.FromActivation());
        while (running)
        {
            if (!listener.Pending())
                Thread.Sleep(100);
            else
                (new Thread(new ParameterizedThreadStart(ProcessLauncher))).Start(listener.AcceptTcpClient());
            lock (this)
            {
                running = !stop;
            }
        }
        listener.Stop();
        lock (this)
        {
            listenerThread = null;
        }
    }

    private void ProcessLauncher(object o)
    {
        Transmission trans = null;
        lock (this)
        {
            currentConections++;
            totalConnections++;
        }
        try
        {
            using TcpClient c = o as TcpClient;
            if (c != null)
            {
                var prc = new TcpProcess(this, c.Client, remote);
                prc.Process();
                trans = prc.Transmission;
            }
        }
        finally
        {
            lock (this)
            {
                currentConections--;
            }
            if (trans != null)
                Message(ForwarderMessage.FromEndTransmission(trans));
        }
    }

    #endregion

    #region TCP processor

    private class TcpProcess
    {
        private Forwarder fw;
        private Socket scksrc;
        private Socket sckdst;
        private Transmission trans;

        public TcpProcess(Forwarder forwarder, Socket source, IPEndPoint remote)
        {
            try
            {
                fw = forwarder;
                scksrc = source;
                sckdst = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                trans = new Transmission(scksrc.RemoteEndPoint as IPEndPoint, sckdst.RemoteEndPoint as IPEndPoint);
                forwarder.Message(ForwarderMessage.FromNewTransmission(trans));
                sckdst.Connect(remote);
            }
            catch (Exception ex)
            {
                forwarder.Message(ForwarderMessage.FromException(ex));
            }
        }

        public void Process()
        {
            bool alive = true;
            int attl = 0;
            Thread.CurrentThread.Name = "Process " + scksrc.LocalEndPoint + " <-> " + sckdst.RemoteEndPoint + " at " + DateTime.Now;
            try
            {
                while (alive)
                {
                    bool afetch = false;
                    if (scksrc.Available > 0)
                    {
                        afetch = true;
                        trans.AddUpload(Dump(scksrc, sckdst));
                        fw.Message(ForwarderMessage.FromSendTransmission(trans));
                    }
                    if (sckdst.Available > 0)
                    {
                        afetch = true;
                        trans.AddDownload(Dump(sckdst, scksrc));
                        fw.Message(ForwarderMessage.FromReceiveTransmission(trans));
                    }
                    if (!afetch)
                        Thread.Sleep(50);
                    alive = fw.Active;
                    if (attl++ > 20)
                    {
                        alive = alive && scksrc.IsConnected() && sckdst.IsConnected();
                        attl = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                fw.Message(ForwarderMessage.FromException(ex));
            }
        }

        private static byte[] Dump(Socket sfrom, Socket sto)
        {
            int bsize = sfrom.Available;
            if (bsize != 0)
            {
                var buf = new byte[bsize];
                sfrom.Receive(buf);
                sto.Send(buf);
                return buf;
            }

            return null;
        }

        public Transmission Transmission => trans;
    }

    #endregion

    public void ResetCounter()
    {
        totalConnections = 0;
    }

    public override string ToString() => local + " -> " + remote;

    public bool Active { get => listenerThread != null; set { if (value) Activate(); else Deactivate(); } }
    public IPEndPoint Local { get => local; set { if (Active) throw new Exception("Can't set when active"); local = value; } }
    public IPEndPoint Remote { get => remote; set { if (Active) throw new Exception("Can't set when active"); remote = value; } }
    public int CurrentConnections => currentConections;
    public int TotalConnections => totalConnections;
    public ForwarderMessageDelegate WhenMessage { get; set; }
}

public delegate void ForwarderMessageDelegate(Forwarder sender, ForwarderMessage msg);

public enum ForwarderMessageType
{
    ERROR, ACTIVATED, DEACTIVATED, TSTART, TSEND, TRECV, TEND
}

public class ForwarderMessage
{
    private ForwarderMessageType type;
    private string message;
    private Exception exception;
    private DateTime time = DateTime.Now;
    private Transmission trans;

    private ForwarderMessage()
    {

    }

    public static ForwarderMessage FromException(Exception ex) => new ()
    {
        type = ForwarderMessageType.ERROR,
        message = ex.GetType().Name + ": " + ex.Message,
        exception = ex
    };

    public static ForwarderMessage FromActivation() => new()
    {
        type = ForwarderMessageType.ACTIVATED,
        message = "Listener enabled"
    };

    public static ForwarderMessage FromDeactivation()
    => new()
    {
        type = ForwarderMessageType.DEACTIVATED,
        message = "Listener disabled"
    };

    public static ForwarderMessage FromNewTransmission(Transmission trans) => new()
    {
        type = ForwarderMessageType.TSTART,
        trans = trans,
        message = "Transmission from " + trans.SourceIP
    };

    public static ForwarderMessage FromSendTransmission(Transmission trans)
    => new()
    {
        type = ForwarderMessageType.TSEND,
        trans = trans
    };

    public static ForwarderMessage FromReceiveTransmission(Transmission trans) => new()
    {
        type = ForwarderMessageType.TRECV,
        trans = trans
    };

    public static ForwarderMessage FromEndTransmission(Transmission trans) => new()
    {
        type = ForwarderMessageType.TEND,
        trans = trans
    };

    public ForwarderMessageType Type => type;
    public string Message => message;
    public DateTime TimeStamp => time;
    public Exception Exception => exception;
    public Transmission Transmission => trans;
}

public class Transmission
{
    private IPEndPoint src;
    private IPEndPoint dst;
    private int uploaded;
    private int downloaded;
    private DateTime date = DateTime.Now;
    private List<Sentence> conversation = new();

    public Transmission(IPEndPoint src, IPEndPoint dst)
    {
        this.src = src;
        this.dst = dst;
    }

    internal void AddUpload(byte[] b)
    {
        if (b == null)
            return;
        uploaded += b.Length;
        conversation.Add(new Sentence(b, true));
    }

    internal void AddDownload(byte[] b)
    {
        if (b == null)
            return;
        downloaded += b.Length;
        conversation.Add(new Sentence(b, false));
    }

    public IPEndPoint SourceIP => src;
    public IPEndPoint DestinationIP => dst;
    public object Tag { get; set; }
    public int Uploaded => uploaded;
    public int Downloaded => downloaded;
    public DateTime Date => date;
    public Sentence[] Conversation => conversation.ToArray();
}

public class Sentence
{
    private DateTime time= DateTime.Now;
    private byte[] data;
    private bool me;

    public Sentence(byte[] data,bool me)
    {
        this.me = me;
        this.data = data;
    }

    public bool FromMe => me;
    public byte[] Data => data;
    public DateTime Time => time;
}
