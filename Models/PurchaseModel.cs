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
        public string ShipName { get; set; }
        [DisplayName("Address 1")]
        public string ShipAdd1 { get; set; }
        [DisplayName("Address 2")]
        public string ShipAdd2 { get; set; }
        [DisplayName("State")]
        public StateList ShipState { get; set; }
        [DisplayName("Zip Code")]
        public string ShipZip { get; set; }
        [DisplayName("Name")]
        public string BillName { get; set; }
        [DisplayName("Address 1")]
        public string BillAdd1 { get; set; }
        [DisplayName("Address 2")]
        public string BillAdd2 { get; set; }
        [DisplayName("State")]
        public StateList BillState { get; set; }
        [DisplayName("Zip Code")]
        public string BillZip { get; set; }
        [DisplayName("Credit Card")]
        public int Card { get; set; }
        [DisplayName("CVV")]
        public int Cvv { get; set; }
        [DisplayName("Month")]
        public MonthList Month { get; set; }
        [DisplayName("Year")]
        public YearList Year { get; set; }
    }
    public enum StateList
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

    public enum MonthList
    {
        Jan,
        Feb,
        Mar,
        Apr,
        May,
        Jun,
        Jul,
        Aug,
        Sep,
        Oct,
        Nov,
        Dec
    }

    public enum YearList
    {
        [Display(Name = "2020")]
        one = 2020,
        [Display(Name = "2021")]
        two = 2021,
        [Display(Name = "2022")]
        three = 2022,
        [Display(Name = "2023")]
        four = 2023,
        [Display(Name = "2024")]
        five = 2024,
        [Display(Name = "2025")]
        six = 2025,
        [Display(Name = "2026")]
        seven = 2026,
        [Display(Name = "2027")]
        eight = 2027
    }
}
