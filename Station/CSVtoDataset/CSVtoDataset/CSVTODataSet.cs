/*************************************************************************************************************************
 * Library to Import CSV Created by Joseph Rounds 7/7/2011
 *************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace CSVtoDatasetlibaray
{
    static public class CSVTODataSet
    {
        /// <summary>
        /// Takes a csv stream in outputs a datatable
        /// </summary>
        /// <param name="File">CSV Stream</param>
        /// <param name="Header">Does it have header</param>
        /// <returns>Datatable from csv</returns>
        static public DataTable CSVToData(Stream instream, bool Header)
        {
            StreamReader file = new StreamReader(instream);
            
            try
            {
                string line;
                DataTable table = new DataTable();
                // DataSet ReturnData = new DataSet();
                // TODO add Code to handle it doesn't have a header
              //If header was specified add header columns
                if (Header)
                {
                    line = file.ReadLine();
                    String[] Colomuns = line.Split(',');
                    foreach (string data in Colomuns)
                    {
                        table.Columns.Add(data);
                    }
                }
                else
                {
                    line = file.ReadLine();
                    String[] Colomuns = line.Split(',');
                    int x = 0;
                    //set up the colomuns 
                    foreach (string data in Colomuns)
                    {
                        table.Columns.Add("Colomun" + x.ToString());
                        x++;

                    }
                    //Add  the first row
                    DataRow row = table.NewRow();
                    x = 0;
                    foreach (string data in Colomuns)
                    {
                        row[x] = data;
                        x++;
                    }
                    table.Rows.Add(row);
                }

                while (!file.EndOfStream)
                {
                    line = file.ReadLine();

                    //check to make sure it is not a empty string
                    if (line != "" || line!=Environment.NewLine)
                    {
                        String[] colomuns = line.Split(',');
                        int x = 0;
                        //Make sure it has a least one column
                        if (colomuns.Length > 1)
                        {
                            //Add each column
                            DataRow row = table.NewRow();
                            foreach (string data in colomuns)
                            {
                                row[x] = data;
                                x++;
                            }
                            table.Rows.Add(row);
                        }
                    }

                }
                //    ReturnData.Tables.Add(table);

                return table;
            }
            catch (Exception e)
            {
                //return null if fails
                return null;
            }
        }

        /// <summary>
        /// Takes a csv stream in outputs a datatable
        /// </summary>
        /// <param name="File">CSV Stream</param>
        /// <param name="Header">Does it have header</param>
        /// <returns>Datatable from csv</returns>
        static public DataTable CSVToDatawithQuotes(Stream instream, bool Header)
        {
            StreamReader file = new StreamReader(instream);

            try
            {
                string line;
                DataTable table = new DataTable();
                // DataSet ReturnData = new DataSet();
                // TODO add Code to handle it doesn't have a header
                //If header was specified add header columns
                if (Header)
                {
                    line = file.ReadLine();
                    String[] Colomuns = line.Split('\"');
                    foreach (string data in Colomuns)
                    {
                        table.Columns.Add(data);
                    }
                }
                else
                {
                    line = file.ReadLine();
                    String[] Colomuns = line.Split('\"');
                    int x = 0;
                    //set up the colomuns 
                    foreach (string data in Colomuns)
                    {
                        if (data != "," && data != "")
                        {
                     
                            table.Columns.Add("Colomun" + x.ToString());
                     
                            x++;
                        }
               
                     
                    }
                    //Add  the first row
                    DataRow row = table.NewRow();
                    x = 0;
                    foreach (string data in Colomuns)
                    {
                        if (data != "," && data != "")
                        {
                            row[x] = data.Replace(",", "");
                            x++;
                        }
                    }
                    table.Rows.Add(row);
                }

                while (!file.EndOfStream)
                {
                    line = file.ReadLine();

                    //check to make sure it is not a empty string
                    if (line != "" || line != Environment.NewLine)
                    {
                        String[] colomuns = line.Split('\"');
                        int x = 0;
                        //Make sure it has a least one column
                        if (colomuns.Length > 1)
                        {
                            //Add each column
                            DataRow row = table.NewRow();
                            foreach (string data in colomuns)
                            {
                                if (data != "," && data != "")
                                {
                                    row[x] = data.Replace(",", "");
                                    x++;
                                }
                            }
                            table.Rows.Add(row);
                        }
                    }

                }
                //    ReturnData.Tables.Add(table);

                return table;
            }
            catch (Exception e)
            {
                //return null if fails
                return null;
            }
        }
    
    }

    
    
}
