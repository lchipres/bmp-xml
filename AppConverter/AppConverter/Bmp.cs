using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConverter
{
    class Bmp
    {
        private int tama, altoPix, anchoPix, bitPix;

        public Bmp(int tama, int altoPix, int anchoPix, int bitPix)
        {
            this.tama = tama;
            this.altoPix = altoPix;
            this.anchoPix = anchoPix;
            this.bitPix = bitPix;
        }
        public string ToString()
        {
            return "Tamaño: "+tama+" bytes"+Environment.NewLine+"Alto: "+altoPix+" Pixeles"+Environment.NewLine+"Ancho: "+anchoPix+" Pixeles"+Environment.NewLine+"Bits por pixel: "+bitPix;
        }

        public int Tama { get => tama; set => tama = value; }
        public int AltoPix { get => altoPix; set => altoPix = value; }
        public int AnchoPix { get => anchoPix; set => anchoPix = value; }
        public int BitPix { get => bitPix; set => bitPix = value; }
    }
}
