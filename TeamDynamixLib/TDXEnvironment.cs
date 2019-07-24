using System;
using System.Collections.Generic;
using System.Text;

namespace TeamDynamixLib {
    public class TDXEnvironment {
        public string ClientUrl {
            get; set;
        }
        public bool IsSandboxEnvironment {
            get; set;
        }
        public string ProxyURL {
            get; set;
        }
        public int ProxyPort {
            get; set;
        }
    }
}
