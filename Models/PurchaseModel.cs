using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class PurchaseModel
    {
        [DisplayName("Name")]
        [Required]
        public string shipName { get; set; }
        [DisplayName("Address 1")]
        [Required]
        public string shipAdd1 { get; set; }
        [DisplayName("Address 2")]
        public string shipAdd2 { get; set; }
        [DisplayName("State")]
        [Required]
        public stateList shipState { get; set; }
        [DisplayName("Zip Code")]
        [Required]
        public string shipZip { get; set; }

        [Required]
        [DisplayName("Name")]
        public string billName { get; set; }

        [Required]
        [DisplayName("Address 1")]
        public string billAdd1 { get; set; }
        [DisplayName("Address 2")]
        public string billAdd2 { get; set; }
        [Required]
        [DisplayName("State")]
        public stateList billState { get; set; }
        [Required]
        [DisplayName("Zip Code")]
        public string billZip { get; set; }
        [Required]
        [CreditCard]
        [DisplayName("Credit Card")]
        public int creditcard { get; set; }

        //public IEnumerable<SelectListItem> States { get; private set; }

    }
    public enum stateList
    {
        Alabama,
        Alaska,
        Arizona,
        Arkansas,
        California,
        Colorado,
        Connecticut,
        Delaware,
        [Display(Name = "District Of Columbia")]
        District_of_Columbia,
        Florida,
        Georgia,
        Hawaii,
        Idaho,
        Illinois,
        Indiana,
        Iowa,
        Kansas,
        Kentucky,
        Louisiana,
        Maine,
        Maryland,
        Massachusetts,
        Michigan,
        Minnesota,
        Mississippi,
        Missouri,
        Montana,
        Nebraska,
        Nevada,
        [Display(Name = "New Hampshire")]
        NewHampshire,
        [Display(Name = "New Jersey")]
        NewJersey,
        [Display(Name = "New Mexico")]
        NewMexico,
        [Display(Name = "New York")]
        NewYork,
        [Display(Name = "North Carolina")]
        NorthCarolina,
        [Display(Name = "North Dakota")]
        NorthDakota,
        Ohio,
        Oklahoma,
        Oregon,
        Pennsylvania,
        [Display(Name = "Rhode Island")]
        RhodeIsland,
        [Display(Name = " South Carolina")]
        SouthCarolina,
        [Display(Name = "South Dakota")]
        SouthDakota,
        Tennessee,
        Texas,
        Utah,
        Vermont,
        Virginia,
        Washington,
        [Display(Name = "West Virginia")]
        WestVirginia,
        Wisconsin,
        Wyoming
    }
}
