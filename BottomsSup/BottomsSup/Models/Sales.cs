using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BottomsSup.Models
{
    public class Sales
    {
        [Key]
        public int SalesId { get; set; }

        [Display(Name = "Date of Sales")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfSales { get; set; }

        [Display(Name = "Total Sales of the Day")]
        [DataType(DataType.Currency)]
        public double TotalSales { get; set; }

        [Display(Name = "Cost of Labor For Day")]
        [DataType(DataType.Currency)]
        public double TotalLabor { get; set; }
        public double LaborPercentage { get; set; }

        [Display(Name = "First Date to Compare Sales")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FirstDateToCompare { get; set; }

        [Display(Name = "Second Date of Sales")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SecondDateToCompare { get; set; }

        [ForeignKey("Bar")]
        public int BarId { get; set; }
        public Bar Bar { get; set; }
    }
}





//<head>
//    <script src = "~/Scripts/Chart.js" ></ script >
//    < script src="~/Scripts/Chart.min.js"></script>
//    <script src = "~/Scripts/bootstrap.min.js" ></ script >
//    < script src="~/Scripts/jquery-3.3.1.min.js"></script>
//    <link href = "~/Content/bootstrap.min.css" rel="stylesheet" />
//    <style>
//        #chart_container {
//            width: 800px;
//            height: 400px;
//            border: 1px solid #000000;
//            padding: 1px;
//            border-radius: 4px;
//        }
//    </style>

//</head>


//<body>


//    <div id = "chart_container" >
//        < canvas id="bar_chart"></canvas>


//    </div>

//    <script>
//        var totals = ViewBag.Total;
//var dates = ViewBag.dates;
//var ctx = $("#bar_chart");
//        var myChart = new Chart(ctx, {
//            type: 'line',
//            data: {
//                labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
//                datasets: [{
//                    label: '# of Votes',
//                    data: [12, 19, 3, 5, 2, 3],
//                    backgroundColor: [
//                        'rgba(255, 99, 132, 0.2)',
//                        'rgba(54, 162, 235, 0.2)',
//                        'rgba(255, 206, 86, 0.2)',
//                        'rgba(75, 192, 192, 0.2)',
//                        'rgba(153, 102, 255, 0.2)',
//                        'rgba(255, 159, 64, 0.2)'
//                    ],
//                    borderColor: [
//                        'rgba(255, 99, 132, 1)',
//                        'rgba(54, 162, 235, 1)',
//                        'rgba(255, 206, 86, 1)',
//                        'rgba(75, 192, 192, 1)',
//                        'rgba(153, 102, 255, 1)',
//                        'rgba(255, 159, 64, 1)'
//                    ],
//                    borderWidth: 1
//                }]
//            },
//            options: {
//                scales: {
//                    yAxes: [{
//                        ticks: {
//                            beginAtZero: true
//                        }
//                    }]
//                }
//            }
//    </script>









//    @*//var barChart = new Chart(ctx, {
//            //    type: 'line',
//            //    data
//            //    datasets:[{
//            //        label: "Line Chart",
//            //        data: [12,19,3,5,2,3]
//            //        backgroundColor: ["blue"],
//            //        borderWidth:1

//            //    }]

//            //},
//            //    options: {
//            //        maintainAspectRatio: false,
//            //        scales: {
//            //            xAxes: [{ ticks: {beginAtZero:true}}]
//            //        },
//            //        legend: {display:false},
//            //    }

//        </script>*@



//</body>






//</html>*@