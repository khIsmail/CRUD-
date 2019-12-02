using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WEBMVC.Models
{
    public class PersonModel
    {
        [DisplayName("NCIN")]
        public int NCIN { get; set; }
        [DisplayName("Nom")]
        public string NomPers { get; set; }
        [DisplayName("Prenom")]
        public string PrenomPers { get; set; }
    }
}