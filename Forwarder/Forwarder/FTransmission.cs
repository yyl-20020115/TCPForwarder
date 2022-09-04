using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Forwarder;
public partial class FTransmission : Form
{
    private static readonly Dictionary<Transmission, FTransmission> fmap = new ();
    private static Font fontMonospace = new("Lucida Console", 7.5f);
    private static Font fontSmall = new(FontFamily.GenericSansSerif, 6f);

    private readonly Transmission trans;

    private FTransmission(Transmission trans)
    {
        InitializeComponent();
        this.trans = trans;
        LoadTransmission();
        pTimeline.AutoScroll = false;
        pTimeline.HorizontalScroll.Enabled = false;
        pTimeline.HorizontalScroll.Visible = false;
        pTimeline.HorizontalScroll.Maximum = 0;
        pTimeline.AutoScroll = true;
    }

    public static void Execute(Transmission trans)
    {
        if (!fmap.TryGetValue(trans, out var f))
            fmap[trans] = f = new FTransmission(trans);
        f.Show();
        f.Focus();
    }

    public static FTransmission Get(Transmission trans) 
        => fmap.TryGetValue(trans, out var f) ? f : null;

    public void NotifyUpdate()
    {
        tUpdate.Enabled = true;
    }

    private void LoadTransmission()
    {
        if (this.trans != null)
        {
            lbInfo.Text = trans.SourceIP + " -> " + trans.DestinationIP;
            lbAt.Text = trans.Date.ToLongDateString() + " " + trans.Date.ToLongTimeString();
            lbCtt.Text = "Uploaded: " + Utils.FBytes(trans.Uploaded) + " Downloaded: " + Utils.FBytes(trans.Downloaded);
            int cscroll = pTimeline.VerticalScroll.Value;
            pTimeline.VerticalScroll.Value = 0;
            pTimeline.Controls.Clear();
            pTimeline.SuspendLayout();
            int y = 5, w2 = (int)(pTimeline.ClientSize.Width * 0.7);
            var tstart = trans.Date;
            var tlast = tstart;
            foreach (var s in trans.Conversation)
            {
                if ((s.Time - tlast).TotalSeconds > 30)
                {
                    var lbx = new Label
                    {
                        Parent = pTimeline,
                        Font = fontSmall,
                        Text = "On hold by " + Utils.FTime(s.Time - tlast),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BackColor = Color.LightYellow,
                        Top = y,
                        Left = 5,
                        Width = pTimeline.ClientSize.Width - 10
                    };
                    y += lbx.Height + 4;
                }
                var lbs = new Label
                {
                    AutoSize = true,
                    MinimumSize = new Size(w2, 0),
                    MaximumSize = new Size(w2, 0),
                    Padding = new Padding(3),
                    Top = y,
                    Width = pTimeline.ClientSize.Width / 2,
                    Left = s.FromMe ? 5 : pTimeline.ClientSize.Width - 5 - w2,
                    BackColor = s.FromMe ? Color.LightGreen : Color.LightBlue,
                    TextAlign = s.FromMe ? ContentAlignment.TopLeft : ContentAlignment.TopRight,
                    Text = Utils.BinToTextSample(s.Data),
                    Font = fontMonospace,
                    Parent = pTimeline
                };
                var lbt = new Label
                {
                    Parent = pTimeline,
                    Font = fontSmall,
                    Text = Utils.FTime(s.Time - tstart),
                    AutoSize = true,
                    Top = lbs.Top - 2
                };
                lbt.Left = s.FromMe ? w2 + 10 : lbs.Left - lbt.Width - 5;
                var lbd = new Label
                {
                    Parent = pTimeline,
                    Font = fontSmall,
                    Text = Utils.FTime(s.Time - tlast),
                    ForeColor = SystemColors.GrayText,
                    AutoSize = true,
                    Top = lbt.Top + lbt.Height - 1
                };
                lbd.Left = s.FromMe ? w2 + 10 : lbs.Left - lbd.Width - 5;
                var lbz = new Label
                {
                    Parent = pTimeline,
                    Font = fontSmall,
                    Text = Utils.FBytes(s.Data.Length),
                    AutoSize = true,
                    Top = lbd.Top + lbd.Height - 1
                };
                lbz.Left = s.FromMe ? w2 + 10 : lbs.Left - lbz.Width - 5;
                y += lbs.Height + 4;
                tlast = s.Time;
            }
            pTimeline.ResumeLayout();
            pTimeline.VerticalScroll.Value = cscroll;
        }
        else
        {

        }
    }

    private void FTransmission_FormClosed(object sender, FormClosedEventArgs e)
    {
        fmap.Remove(trans);
        Dispose();
    }

    private void pTimeline_Resize(object sender, EventArgs e)
    {
        LoadTransmission();
    }

    private void tUpdate_Tick(object sender, EventArgs e)
    {
        tUpdate.Enabled = false;
        LoadTransmission();
    }
}
