using System.Runtime.InteropServices;

namespace UmamusumeExplorer.Controls
{
    public class BorderlessAeroForm : Form
    {
        public void SetBorderlessAndAdjust()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;

            Size newSize = Size;
            newSize.Height -= SystemInformation.CaptionHeight + (SystemInformation.FrameBorderSize.Height + SystemInformation.VerticalResizeBorderThickness - 1) * 2;
            newSize.Width -= (SystemInformation.FrameBorderSize.Width + SystemInformation.HorizontalResizeBorderThickness - 1) * 2;

            Size = newSize;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0083: // WM_NCCALCSIZE
                    {
                        NCCALCSIZE_PARAMS ncp = Marshal.PtrToStructure<NCCALCSIZE_PARAMS>(m.LParam);

                        ncp.rgrc[0].left += 1;
                        ncp.rgrc[0].top += 1;
                        ncp.rgrc[0].right += 1;
                        ncp.rgrc[0].bottom += 1;

                        Marshal.StructureToPtr(ncp, m.LParam, true);

                        m.Result = 1;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct NCCALCSIZE_PARAMS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public RECT[] rgrc;
            public nint lppos;
        }
    }
}
