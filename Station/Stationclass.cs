using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Station
{
    public class Stationclass
    {
        DataTable _stations = new DataTable();
        //Not the best way to keep track but quick and dirty
        List<string> _reachedstations = new List<string>();

        public Stationclass(DataTable stations)
        {
            _stations = stations;

        }
        /// <summary>
        /// Gets stations N away
        /// </summary>
        /// <param name="start"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<string> GetStationsNDistance(string start, int n)
        {
            _reachedstations = new List<string>();
            List<string> stationsndist = new List<string>();
            stationsndist= GetNextStation(start, "", n);
            stationsndist.Sort();
            return stationsndist;
        }
        /// <summary>
        /// Helper functin
        /// </summary>
        /// <param name="station"></param>
        /// <param name="prevstation"></param>
        /// <param name="hopsleft"></param>
        /// <returns></returns>
        private List<string> GetNextStation(string station, string prevstation, int hopsleft)
        {
      
            List<string> stationsndist = new List<string>();
            var stationsforward =
              from stat in _stations.AsEnumerable()

              where stat.Field<string>("From Station") == station 
              &&  stat.Field<string>("To Station") != prevstation

              select new
              {
                  Fromstation = stat.Field<System.String>("From Station"),
                  ToStation = stat.Field<System.String>("To Station"),
              };

            var stationsbackwords =

                from stat in _stations.AsEnumerable()

                where stat.Field<string>("From Station") != prevstation &&
                stat.Field<string>("To Station") == station

                select new
                {
                    Fromstation = stat.Field<System.String>("From Station"),
                    ToStation = stat.Field<System.String>("To Station"),
                };


            if (hopsleft == 1)
            {
                foreach (var stat in stationsforward)
                {
                    stationsndist.Add(stat.ToStation);


                }
                foreach (var stat in stationsbackwords)
                {
                    stationsndist.Add(stat.Fromstation);


                }
            }
            else
            {
                --hopsleft;
                foreach (var stat in stationsforward)
                {
                    if (!_reachedstations.Contains(stat.ToStation))
                    {
                        _reachedstations.Add(stat.ToStation);
                    }
                    foreach (var rstations in   GetNextStation(stat.ToStation,stat.Fromstation,hopsleft))
                    {

                        
                        if (!stationsndist.Contains(rstations) && !_reachedstations.Contains(rstations))
                        {
                            stationsndist.Add(rstations);
                        }
                    }



                }
                foreach (var stat in stationsbackwords)
                {
                    if (!_reachedstations.Contains(stat.Fromstation))
                    {
                        _reachedstations.Add(stat.Fromstation);
                    }
                    foreach (var rstations in GetNextStation(stat.Fromstation, stat.ToStation, hopsleft))
                    {
                        if (!stationsndist.Contains(rstations) && !_reachedstations.Contains(rstations))
                        {
                            stationsndist.Add(rstations);
                        }
                    }



                }

            }
            return stationsndist;

        }

    }
}
