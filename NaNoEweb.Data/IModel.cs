using System;

namespace NaNoEweb.Data
{
    public class IModel
    {
        public uint ID { get; set; } = 0;
        public uint MSSql_ID { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
