using MVC_FirstProject.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MVC_FirstProject.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is Required Yaa Nahlaa!!")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        //Navigation
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
