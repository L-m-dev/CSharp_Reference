using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningDiscs {
    public abstract class BaseDisc : IOpticalDisc {
        public string Brand { get; set; }
        public double CurrentArrayCapacity { get; set; }
        public double ReadRate { get; set; }
        public double WriteRate { get; set; }
        public double SpinRate { get; set; }
        public long CurrentUsedCapacity { get; set; } = 0;
        public long LastIndexUsedPointer { get; set; } = 0;
        public long MAXIMUM_CAPACITY;
        public long MINIMUM_ARRAY_SIZE;
        public bool IsRunning { get; set; } = false;

        public byte[] _data;
        public byte[] Data {
            get { return this._data; }
            set {
                if ((value.Length + this.CurrentUsedCapacity) <= this.MAXIMUM_CAPACITY) {
                    if (value.Length + this.CurrentUsedCapacity > this.CurrentArrayCapacity) {
                        ResizeArray((long)(value.Length + this.CurrentArrayCapacity));
                    }
                    else {
                        this._data = value;
                    }
                }
                else {
                    throw new ArgumentException("No capacity.");
                }
            }
        }

        public void WriteData(byte[] incomingData) {
            if (CurrentUsedCapacity >= MAXIMUM_CAPACITY || (CurrentUsedCapacity + incomingData.Length) > MAXIMUM_CAPACITY) {
                throw new ArgumentOutOfRangeException("No capacity left in this disk");
            }

            if ((incomingData.Length + this.CurrentUsedCapacity) > this.Data.Length) {
                ResizeArray(incomingData.Length + this.CurrentUsedCapacity);
            }

            for (int i = 0; i < incomingData.Length; i++) {
                if (incomingData[i] > 0) {
                    this.Data[i] = incomingData[i];
                    LastIndexUsedPointer = i;
                }
            }

        }

        public bool FormatData() {
            try {
                CurrentUsedCapacity = 0;
                LastIndexUsedPointer = 0;

                CurrentArrayCapacity = MINIMUM_ARRAY_SIZE;
                this.Data = new byte[MINIMUM_ARRAY_SIZE];
                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

        public void ResizeArray(long newSize) {
            if (newSize > MAXIMUM_CAPACITY) {
                newSize = MAXIMUM_CAPACITY;
            }

            if (this.Data == null) {
                this.CurrentArrayCapacity = newSize;
                this.Data = new byte[newSize];
            }
            else {
                byte[] newArray = new byte[newSize];
                Array.Copy(this.Data, newArray, this.CurrentUsedCapacity);
                GC.Collect();
                this.CurrentArrayCapacity = newArray.Length;
            }
        }

        public virtual string GetInfo() {
            return this.Brand;
        }

        public byte[] ReadData() {
            byte[] slicedArray = new byte[LastIndexUsedPointer];
            Array.Copy(this.Data, slicedArray, slicedArray.Length);
            return this.Data;
        }

        public void SpinDisc() {
            if (IsRunning) {
                Console.WriteLine("SPINNING!");
                Console.WriteLine("Spin Rate is " + this.SpinRate);
            }
        }

        bool IOpticalDisc.FormatData() {
            try {
                this.Data = new byte[MAXIMUM_CAPACITY];
                GC.Collect();
                return true;
            }
            catch (Exception ex) {
                return false;
            }

        }
    }
}
