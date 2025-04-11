using System;
using System.Text;
using System.Transactions;

namespace SpinningDiscs {
    public class CD : BaseDisc, IOpticalDisc {

        //200 megabytes 
        public const double READ_RATE = 200000000;
        //50 megabytes
        public const double WRITE_RATE = 50000000;
       
        public CD(string brand) {
            Brand = brand;
            this.IsRunning = true;
            MINIMUM_ARRAY_SIZE = 64;
            //700 megabytes
            MAXIMUM_CAPACITY = 700000000;
            CurrentUsedCapacity = 0;       
            this.Data = new byte[MINIMUM_ARRAY_SIZE];
            this.ReadRate = READ_RATE;
            this.WriteRate = WRITE_RATE;
            this.SpinRate = 1000;
        }

        public override string GetInfo() {
            StringBuilder sB = new StringBuilder();
            sB.AppendLine(base.GetInfo());
            sB.AppendLine($"Current Used Space / Total : {CurrentUsedCapacity}/{MAXIMUM_CAPACITY}");
            sB.AppendLine($"Read/Write Rate: {READ_RATE}MBSP{WRITE_RATE} MBPS");
            sB.AppendLine("Spin Rate is " + SpinRate);
            return sB.ToString();
        }      
    }
}

