using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningDiscs {
    public interface IOpticalDisc {
        public static int SPEED_OF_LIGHT = 299792458;

        public byte[] ReadData();
        public void WriteData(byte[] data);
        public void SpinDisc();
        public bool FormatData();
        public string GetInfo();

    }
}
