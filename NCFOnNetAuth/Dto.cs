using System.Collections.Generic;

namespace NCFOnNetAuth
{
    public class Permission
        {
            public string name { get; set; }
            public string description { get; set; }
        }

        public class TerminalType
        {
            public string id { get; set; }
            public string description { get; set; }
        }

        public class Role
        {
            public string name { get; set; }
            public string description { get; set; }
            public List<Permission> permissions { get; set; }
            public List<TerminalType> terminalTypes { get; set; }
        }

        public class ExternalInfo
        {
        }

        public class LimitProfile
        {
            public int id { get; set; }
            public bool active { get; set; }
            public bool allowInternationalCardProducts { get; set; }
            public string name { get; set; }
            public decimal maxTransactionAmount { get; set; }
            public decimal maxSameCardTxPerDay { get; set; }
            public decimal maxSameCardAmountPerDay { get; set; }
            public string currency { get; set; }
        }

        public class TipProfile
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public decimal maxPercent { get; set; }
        }

        public class Terminal
        {
            public int id { get; set; }
            public string terminalId { get; set; }
            public ExternalInfo externalInfo { get; set; }
            public bool active { get; set; }
            public bool autoSettle { get; set; }
            public string autosettleTime { get; set; }
            public string terminalType { get; set; }
            public LimitProfile limitProfile { get; set; }
            public TipProfile tipProfile { get; set; }
        }

        public class RootObject
        {
            public string username { get; set; }
            public bool active { get; set; }
            public string fullName { get; set; }
            public string email { get; set; }
            public List<Role> roles { get; set; }
            public List<Terminal> terminals { get; set; }
            public bool isFirstTime { get; set; }
        }
    
}
