using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Adfos.Entities
{
    [Serializable]
    public class LogEntry
    {
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public EventLogEntryType Type { get; set; }
        [XmlIgnore]
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Conventional Error Code
        /// </summary>
        [XmlIgnore]
        public int Number { get; set; }
        /// <summary>
        /// Technical Error Code
        /// </summary>
        public long Code { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string TraceInfo { get; set; }
        public string Ip { get; set; }
        public string userId { get; set; }
    }
}
