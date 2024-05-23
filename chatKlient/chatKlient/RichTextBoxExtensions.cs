using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatKlient
{
    public static class RichTextBoxExtensions
    {
        const int EM_SETCHARFORMAT = 1092;
        const int SCF_SELECTION = 1;
        const int CFM_LINK = 0x20;

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        private struct CHARFORMAT2_STRUCT
        {
            public int cbSize;
            public int dwMask;
            public int dwEffects;
            public int yHeight;
            public int yOffset;
            public int crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szFaceName;
            public short wWeight;
            public short sSpacing;
            public int crBackColor;
            public int lcid;
            public int dwReserved;
            public short sStyle;
            public short wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref CHARFORMAT2_STRUCT lParam);

        public static void SetSelectionLink(this RichTextBox richTextBox, bool link)
        {
            CHARFORMAT2_STRUCT cf = new CHARFORMAT2_STRUCT();
            cf.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(cf);
            cf.dwMask = CFM_LINK;
            cf.dwEffects = link ? CFM_LINK : 0;

            SendMessage(richTextBox.Handle, EM_SETCHARFORMAT, (IntPtr)SCF_SELECTION, ref cf);
        }
    }
}
