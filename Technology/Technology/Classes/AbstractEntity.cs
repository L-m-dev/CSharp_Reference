using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Technology.Classes {
    public abstract class AbstractEntity {
        public int Id { get; set; }
        public static int NextId { get; set; } = 1;
        
        public AbstractEntity() {
            this.Id = NextId;
            NextId++;
        }
    }
}
