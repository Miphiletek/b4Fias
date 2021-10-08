using System;
using System.Collections.Generic;
using System.Text;

namespace Migrations_bd
{
    public class b4_fias_house
    {

        public string house_id { get; set; }
        public string house_guid { get; set; }
        public string ao_guid { get; set; }
        public string postal_code { get; set; }
        public string okato { get; set; }
        public string oktmo { get; set; }
        public string house_num { get; set; }
        public string build_num {get;set;}
        public string struc_num { get; set; }
        public int stat_status { get; set; }
        public DateTime update_date { get; set; }
        public int type_record { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int structure_type { get; set; }
        public int est_status { get; set; }
        public int if_exists_on_boi { get; set; }

    }
}
