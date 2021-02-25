using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiHarjoituskoodi_1.Models
{
    public partial class Documentation
    {
        public int DocumentationId { get; set; }
        public string AvaiableRoute { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }
        public string Keycode { get; set; }
        
    }
}
