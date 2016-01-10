using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivotFunction
{
     public class pivotTable
    {
        //connetion string for connect to server
        private string Conn = "Persist Security Info=False;Integrated Security=true;Initial Catalog=sputnik1;server=POLINA-PC\\SQLEXPRESS";
        // data table 
        private DataTable _table;

       
      
        public pivotTable() {
            try
            {
                _table = new DataTable();
                SqlConnection connection = new SqlConnection(Conn);

                SqlDataAdapter Adapter = new SqlDataAdapter("SELECT * FROM dbo.SalesPivot", connection);
                Adapter.Fill(_table);
               
            }
            catch(Exception ex) {
                Console.WriteLine("Error!" + ex.ToString());
                Console.ReadLine(); 
            }

           
        }

        public String CreatePivotTable() {

            // Check if there's any records from database
            if (_table.Rows.Count == 0 && _table!= null)
                return "";

            // list pivot row
             List<DateTime> pivot_rows  = new List<DateTime>();

            //list pivot coloumn 
             List<String> pivot_colunms= new List<string>();

        // string which contist html table
        String table_html="" ;
          
            // fill list with  all distinct date for rows labels 
            pivot_rows = _table.AsEnumerable().Select(c => (DateTime)c["Date"]).Distinct().ToList();
           
            // fill list with all distinct customers for columns labels
            pivot_colunms = _table.AsEnumerable().Select(c => (String)c["Customer"]).Distinct().ToList();
          
            //fill  the html table with rows
            String row_string = "<tr><th>Date</th> ";
            foreach (String str in pivot_colunms)
                row_string += "<th>"+ str + "</th>";
            row_string += "</tr>";
            table_html = row_string;
            
            foreach (DateTime date in pivot_rows)
            {
                 row_string = "<tr><td>"+ date.ToShortDateString() + "</td>";
                foreach (String customer in pivot_colunms)
                {
                    // find a sum for customer for this day
                    var Summa = (from row in _table.AsEnumerable()
                                         where row.Field<DateTime>("Date")==date && row.Field<String>("Customer")==customer
                                         select row.Field<Decimal>("TotalSales")).FirstOrDefault();

                    if((Decimal)Summa >0 )
                    row_string +="<td>" + Summa.ToString() + "</td>";
                    else
                        row_string += "<td></td>";
                    

                }
                table_html += row_string + "</tr>";
               
            }
          
            return "<html> <head> <style> table, td, th { border-collapse: collapse; border: 1px solid black; } th { background: lightblue; } </style ></head> <body> <table> "+ table_html + " </table></body></html>"; 
        }
  
    }
 

}
