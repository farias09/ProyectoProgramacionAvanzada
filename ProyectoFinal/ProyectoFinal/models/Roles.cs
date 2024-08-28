using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoFinal.Models
{
    public class Roles
    {
        [Key]
        public int id_rol { get; set; }
        public string nombreRol { get; set; }
    }
}