using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVtoDatasetlibaray;
using System.IO;
using System.Data;

namespace Station
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            int.TryParse(args[0], out n);
            FileStream stream = new FileStream(@"London tube lines.csv", FileMode.Open);
           DataTable stations = CSVTODataSet.CSVToData(stream,true);
            Stationclass distcalc = new Station.Stationclass(stations);
            foreach(string stat in distcalc.GetStationsNDistance("East Ham", n))
            {
                Console.WriteLine(stat);
            }
            Console.ReadKey();


        }
    }
}
