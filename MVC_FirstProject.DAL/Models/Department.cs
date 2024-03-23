using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_FirstProject.DAL.Models
{
    // Model
    public class Department : ModelBase
    {
        [Required(ErrorMessage = "Code is Required Yaa Nahlaa!!")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

    }
}
