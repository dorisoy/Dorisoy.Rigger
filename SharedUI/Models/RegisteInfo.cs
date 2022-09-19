using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models
{
    [Serializable]
    public class RegisteInfo
    {
        public long? ID { get; set; }
        public string RegName { get; set; }
        public string Email { get; set; }
        public string MachineCode { get; set; }
        public string? RegistrationCode { get; set; }

        public DateTime? RegTime { get; set; }

        public int? Day { get; set; }
    }
}
