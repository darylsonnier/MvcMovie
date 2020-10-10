#pragma warning disable 1591
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    /// <summary>
    /// PurchaseModel class
    /// Models information for purchase.  Includes billing and shipping information as well as credit card purchase information.
    /// </summary>
    public class PurchaseModel
    {
        /// <summary>
        /// ShipName is the name of the shipee.
        /// </summary>
        [DisplayName("Name")]
        public string ShipName { get; set; }
        /// <summary>
        /// ShipAdd1 is line 1 of the shipee's address.
        /// This is typically a street address or PO Box.
        /// </summary>
        [DisplayName("Address 1")]
        public string ShipAdd1 { get; set; }
        /// <summary>
        /// ShipAdd2 is line 2 of the shipee's address.
        /// This usually is an apartment number of similar.
        /// </summary>
        [DisplayName("Address 2")]
        public string ShipAdd2 { get; set; }
        /// <summary>
        /// ShipState is the state for the shipee's address.
        /// It is of type StateList (enum of US States and districts)
        /// </summary>
        [DisplayName("State")]
        public StateList ShipState { get; set; }
        /// <summary>
        /// ShipZip is the shipee's zip code.
        /// </summary>
        [DisplayName("Zip Code")]
        public string ShipZip { get; set; }
        /// <summary>
        /// BillName is the name of the purchaser.
        /// </summary>
        [DisplayName("Name")]
        public string BillName { get; set; }
        /// <summary>
        /// BillAdd1 is line 1 of the purchaser's address.
        /// It is typically a street address or PO Box and is tied to the credit card.
        /// </summary>
        [DisplayName("Address 1")]
        public string BillAdd1 { get; set; }
        /// <summary>
        /// BillAdd2 is line 2 of the purchaser's address.
        /// It is typically an apartment number or similar.
        /// </summary>
        [DisplayName("Address 2")]
        public string BillAdd2 { get; set; }
        /// <summary>
        /// BillState is the state of the purchaser's address.
        /// It is of type StateList (enum of US States and districts).
        /// </summary>
        [DisplayName("State")]
        public StateList BillState { get; set; }
        /// <summary>
        /// BillZip is the zip code of the purchaser's address.
        /// </summary>
        [DisplayName("Zip Code")]
        public string BillZip { get; set; }
        /// <summary>
        /// Card is the purchaser's credit or debit card number.
        /// </summary>
        [DisplayName("Credit Card")]
        public int Card { get; set; }
        /// <summary>
        /// Cvv is the CVV security code of the purchaser's credit or debit card.
        /// </summary>
        [DisplayName("CVV")]
        public int Cvv { get; set; }
        /// <summary>
        /// Month is the expiration month of the purchaser's credit or debit card.
        /// It is of type MonthList (enum of months of the year).
        /// </summary>
        [DisplayName("Month")]
        public MonthList Month { get; set; }
        /// <summary>
        /// Year is the expiration year of the purchaser's credit or debit card.
        /// It is of type YearList (enum of years spanning from 2020 to 2027).
        /// </summary>
        [DisplayName("Year")]
        public YearList Year { get; set; }
    }
    /// <summary>
    /// StateList is an enum of the US states and districts.
    /// It is used for drop down lists.
    /// </summary>
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

    /// <summary>
    /// MonthList is an enum of the months in the year.
    /// It is used for drop down lists.
    /// </summary>
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
    /// <summary>
    /// YearList is an enum of the years 2020 to 2027.
    /// It is used for drop down lists.
    /// </summary>
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
