using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DapperMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "first enter the name")]
       // [RegularExpression(@"(/^[A-Za-z]+$/)", ErrorMessage = "numbers not allowed")]
        public string Name { get; set; }
        //[Required(ErrorMessage ="enter the Email")]
       // [EmailAddress]
        public string Email { get; set; }
        //[Required(ErrorMessage ="enter the phone number")]
       // [StringLength(10)]
        //[RegularExpression(@"(/^[0-9]+$/)", ErrorMessage = "string not allowed")]
        public string Phone { get; set; }
       // [Required(ErrorMessage ="Enter the adderess")]
        //[StringLength(255)]

        public string Gender { get; set; }

        public string Skill { get; set; }
        public string Adderess { get; set; }
        
        public List<string> AddSkill { get; set; }
        public Boolean IsActive { get; set; }
        
    }
}