using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Npgsql.EntityFrameworkCore;
using System.Data.Entity;


namespace Migrations_bd
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch.StartNew();
            XmlDocument xDocADDR = new XmlDocument();
            xDocADDR.Load(@"C:/Users/Dmitry/Desktop/AS_ADDR_OBJ.XML");
            // получим корневой элемент
            XElement rootElement = XElement.Parse(xDocADDR.InnerXml);
            xDocADDR = new XmlDocument();
            // обход всех узлов в корневом элементе
            ADDRESSOBJECTS addresobjs = DeserializeAddrObjs(rootElement);
            rootElement = null;
            //using (ApplicationContext db = new ApplicationContext())
            ApplicationContext db = new ApplicationContext();
            //{
            foreach (var obj in addresobjs.OBJECT.Where(x => x.ISACTUAL == ADDRESSOBJECTSOBJECTISACTUAL.Item1).Where(x => x.ISACTIVE == ADDRESSOBJECTSOBJECTISACTIVE.Item1))
            {
                gar_address_objects gar_addr_obj = new gar_address_objects
                {
                    ISACTIVE = obj.ISACTIVE,
                    ID = obj.ID,
                    NAME = obj.NAME,
                    CHANGEID = obj.CHANGEID,
                    ENDDATE = obj.ENDDATE,
                    ISACTUAL = obj.ISACTUAL,
                    LEVEL = obj.LEVEL,
                    NEXTID = obj.NEXTID,
                    OBJECTGUID = obj.OBJECTGUID,
                    OBJECTID = obj.OBJECTID,
                    OPERTYPEID = obj.OPERTYPEID,
                    PREVID = obj.PREVID,
                    STARTDATE = obj.STARTDATE,
                    TYPENAME = obj.TYPENAME,
                    UPDATEDATE = obj.UPDATEDATE
                };
                db.gar_AddObjCon.Add(gar_addr_obj);
            }
            addresobjs = null;

            xDocADDR = null;
            xDocADDR = new XmlDocument();
            xDocADDR.Load(@"C:/Users/Dmitry/Desktop/AS_HOUSES.XML");

            rootElement = XElement.Parse(xDocADDR.InnerXml);
            HOUSES Houseobj = DeserializeHouses(rootElement);
            rootElement = null;

            foreach (var house in Houseobj.HOUSE.Where(x => x.ISACTIVE == HOUSESHOUSEISACTIVE.Item1))
            {
                gar_houses gar_house = new gar_houses
                {
                    id = house.ID,
                    OBJECTID = house.OBJECTID,
                    HOUSETYPE = house.HOUSETYPE,
                    CHANGEID = house.CHANGEID,
                    HOUSENUM = house.HOUSENUM,
                    ADDNUM1 = house.ADDNUM1,
                    ADDNUM2 = house.ADDNUM2,
                    ADDTYPE1 = house.ADDTYPE1,
                    ADDTYPE2 = house.ADDTYPE2,
                    PREVID = house.PREVID,
                    NEXTID = house.NEXTID,
                    ENDDATE = house.ENDDATE,
                    ISACTUAL = house.ISACTUAL,
                    ISACTIVE = house.ISACTIVE,
                    OBJECTGUID = house.OBJECTGUID,
                    OPERTYPEID = house.OPERTYPEID,
                    STARTDATE = house.STARTDATE,
                    UPDATEDATE = house.UPDATEDATE
                };
                db.gar_HousesCon.Add(gar_house);
            }
            xDocADDR = null;
            xDocADDR = new XmlDocument();
            xDocADDR.Load(@"C:/Users/Dmitry/Desktop/AS_ADM_HIER.XML");

            rootElement = XElement.Parse(xDocADDR.InnerXml);
            ITEMS Adm_Delobj = DeserializeITEM(rootElement);
            rootElement = null;
            foreach (var adm_item in Adm_Delobj.ITEM)
            {
                gar_adm_hier gar_item = new gar_adm_hier
                {
                    id = adm_item.ID,
                    OBJECTID = adm_item.OBJECTID,
                    PARENTOBJID = adm_item.PARENTOBJID,
                    CHANGEID = adm_item.CHANGEID,
                    REGIONCODE = adm_item.REGIONCODE,
                    AREACODE = adm_item.AREACODE,
                    CITYCODE = adm_item.CITYCODE,
                    PLACECODE = adm_item.PLACECODE,
                    PLANCODE = adm_item.PLANCODE,
                    STREETCODE = adm_item.STREETCODE,
                    PREVID = adm_item.PREVID,
                    NEXTID = adm_item.NEXTID,
                    UPDATEDATE = adm_item.UPDATEDATE,
                    STARTDATE = adm_item.STARTDATE,
                    ENDDATE = adm_item.ENDDATE,
                    ISACTIVE = adm_item.ISACTIVE
                };
                db.gar_AdmHierCon.Add(gar_item);
            }

            db.SaveChanges();
            xDocADDR = new XmlDocument();
            xDocADDR.Load("C:/Users/Dmitry/Desktop/AS_ADM_HIER.XML");
            // получим корневой элемент
            XElement rootElementAdmin_Hier = XElement.Parse(xDocADDR.InnerXml);
            // обход всех узлов в корневом элементе
            //DataTable tableITEMS = new DataTable();
            //tableITEMS.Columns.Add("AdmObjID", typeof(String));
            //tableITEMS.Columns.Add("AdmParID", typeof(String));
            //tableITEMS.Columns.Add("AdmISACTIVE", typeof(String));
            //ITEMS item = DeserializeITEM(rootElementAdmin_Hier);
            //foreach (var obj in item.ITEM)
            //{
            //    DataRow dr3 = tableITEMS.NewRow();
            //    dr3["HousesISA"] = long.Parse(obj.OBJECTID.ToString());
            //    dr3["HousesISA"] = long.Parse(obj.PARENTOBJID.ToString());
            //    dr3["HousesNum"] = obj.ISACTIVE.ToString();
            //    tableHOUSES.Rows.Add(dr3);
            //    /*AdmObjID.Add(long.Parse(obj.OBJECTID.ToString()));
            //    AdmParID.Add(long.Parse(obj.PARENTOBJID.ToString()));
            //    AdmISACTIVE.Add(obj.ISACTIVE.ToString());*/





                using (ApplicationContext connection = new ApplicationContext())
            {

                //var listing = (from s1 in connection.gar_AdmHierCon
                //               join u1 in connection.gar_AddObjCon on s1.PARENTOBJID equals u1.OBJECTID
                //               where s1.PARENTOBJID == u1.OBJECTID
                //               select u1.OBJECTGUID
                //                      ).ToList();
                var listing = (from s in connection.gar_AddObjCon
                               select   s.OBJECTGUID).Distinct().ToList();
                var listing2 = (from jkl in connection.copy_b4_fias
                                select  jkl.aoguid).Distinct().ToList();
                var result = listing2.Except(listing).ToList();
                var result2 = from ty in connection.gar_AddObjCon
                              select new { ty.UPDATEDATE, ty.STARTDATE, ty.ENDDATE};
                Console.WriteLine(listing.Count);
                Console.WriteLine(listing2.Count);
                Console.WriteLine(result.Count);
                Console.WriteLine(listing.Count - listing2.Count);

                GC.Collect();




                //var vstavka2 = connection.gar_AdmHierCon.Join(connection.gar_AddObjCon,
                //    s => s.PARENTOBJID,
                //    u => u.OBJECTID,
                //    (s, u) => new
                //    {
                //        coderecord = "",
                //        typerecord = 0,
                //        aolevel = int.Parse(u.LEVEL),
                //        actstatus = 0,
                //        centstatus = 0,
                //        operstatus = 0,
                //        startdate = u.STARTDATE,
                //        enddate = u.ENDDATE,
                //        aoguid = u.OBJECTGUID,
                //        aoid = u.OBJECTID,
                //        parentguid = (from s1 in connection.gar_AdmHierCon
                //                      join u1 in connection.gar_AddObjCon on s1.PARENTOBJID equals u1.OBJECTID
                //                      where s1.PARENTOBJID == u1.OBJECTID
                //                      select u1.OBJECTGUID
                //                            ).ToString(),

                //        previd = u.PREVID,
                //        nextid = u.NEXTID,
                //        formalname = u.NAME,
                //        offname = u.NAME,
                //        shortname = u.TYPENAME,
                //        regioncode = s.REGIONCODE,
                //        autocode = "",
                //        areacode = s.AREACODE,
                //        citycode = s.CITYCODE,
                //        ctarcode = "",
                //        placecode = s.PLACECODE,
                //        streetcode = s.STREETCODE,
                //        extrcode = "",
                //        sextcode = "",
                //        postalcode = "",
                //        ifnsfl = "",
                //        terrifnsfl = "",
                //        ifnsul = "",
                //        terrifnsul = "",
                //        okato = "",
                //        updatedate = u.UPDATEDATE,
                //        normdoc = "",
                //        kladrcode = "",
                //        kladrplaincode = "",
                //        kladrpcurrstatus = 0,
                //        mirror_guid = "",
                //        oktmo = ""
                //    });

                var vstavka = (from s in connection.gar_AdmHierCon

                               join u in connection.gar_AddObjCon on s.PARENTOBJID equals u.OBJECTID
                               join k in connection.gar_AddObjCon on s.OBJECTID equals k.OBJECTID
                               //where s.PARENTOBJID==u.OBJECTID


                               select new NewClass(

"",
0,
int.Parse(u.LEVEL),
0,
0,
0,
u.STARTDATE,
u.ENDDATE,
k.OBJECTGUID.ToString(),
"",
u.OBJECTGUID.ToString(),
u.PREVID.ToString(),
u.NEXTID.ToString(),
u.NAME,
u.NAME,
u.TYPENAME,
s.REGIONCODE,
"",
s.AREACODE,
s.CITYCODE,
"",
s.PLACECODE,
s.STREETCODE,
"",
"",
"",
"",
"",
"",
"",
"",
u.UPDATEDATE,
"",
"",
"",
0,
"",
""

)).Distinct().ToList();













                //foreach(var inb in result.AsEnumerable<>)
                //{
                //    Console.WriteLine(inb.aoguid);
                //}

                foreach (var obj in vstavka)
                {
                    if(!listing2.Contains(obj.Aoguid))
                    { 

                    copy_b4_fias copy_B4_ = new copy_b4_fias
                    {

                        coderecord = obj.Coderecord,
                        typerecord = obj.Typerecord,
                        aolevel = obj.Aolevel,
                        actstatus = obj.Actstatus,
                        centstatus = obj.Centstatus,
                        operstatus = obj.Operstatus,
                        startdate = obj.Startdate,
                        enddate = obj.Enddate,
                        aoguid = obj.Aoguid,
                        aoid = obj.Aoid,
                        parentguid = obj.Parentguid,
                        previd = long.Parse(obj.Previd),
                        nextid = long.Parse(obj.Nextid),
                        formalname = obj.Formalname,
                        offname = obj.Offname,
                        shortname = obj.Shortname,
                        regioncode = obj.Regioncode,
                        autocode = obj.Autocode,
                        areacode = obj.Areacode,
                        citycode = obj.Citycode,
                        ctarcode = obj.Ctarcode,
                        placecode = obj.Placecode,
                        streetcode = obj.Streetcode,
                        extrcode = obj.Extrcode,
                        sextcode = obj.Sextcode,
                        postalcode = obj.Postalcode,
                        ifnsfl = obj.Ifnsfl.ToString(),
                        terrifnsfl = obj.Terrifnsfl.ToString(),
                        ifnsul = obj.Ifnsul,
                        terrifnsul = obj.Terrifnsul.ToString(),
                        okato = obj.Okato,
                        updatedate = DateTime.Parse(obj.Updatedate.ToString()),
                        normdoc = obj.Normdoc.ToString(),
                        kladrcode = obj.Kladrcode.ToString(),
                        kladrplaincode = obj.Kladrplaincode.ToString(),
                        kladrpcurrstatus = int.Parse(obj.Kladrpcurrstatus.ToString()),
                        mirror_guid = obj.Mirror_guid.ToString(),
                        oktmo = obj.Oktmo.ToString()

                    };
                    connection.copy_b4_fias.Add(copy_B4_);
                        
                }





                }

                    //connection.SaveChanges();


                    //var intert = from nub in connection.copy_b4_fias
                    //             join ter in result on nub.aoguid! equals ter.aoguid
                    //             select new copy_b4_fias
                    //             {

                    //                 coderecord = "",
                    //                 typerecord = 0,
                    //                 aolevel = ter.aolevel,
                    //                 actstatus = ter.actstatus,
                    //                 centstatus = ter.centstatus,
                    //                 operstatus = ter.operstatus,
                    //                 startdate = ter.startdate,
                    //                 enddate = ter.enddate,
                    //                 aoguid = ter.aoguid,
                    //                 aoid = ter.aoid,
                    //                 parentguid = ter.parentguid,
                    //                 previd = ter.previd,
                    //                 nextid = ter.nextid,
                    //                 formalname = ter.formalname,
                    //                 offname = ter.offname,
                    //                 shortname = ter.shortname,
                    //                 regioncode = ter.regioncode,
                    //                 autocode = ter.autocode,
                    //                 areacode = ter.areacode,
                    //                 citycode = ter.citycode,
                    //                 ctarcode = "",
                    //                 placecode = ter.citycode,
                    //                 streetcode = ter.streetcode,
                    //                 extrcode = "",
                    //                 sextcode = "",
                    //                 postalcode = "",
                    //                 ifnsfl = "",
                    //                 terrifnsfl = "",
                    //                 ifnsul = "",
                    //                 terrifnsul = "",
                    //                 okato = "",
                    //                 updatedate = ter.updatedate,
                    //                 normdoc = "",
                    //                 kladrcode = "",
                    //                 kladrplaincode = "",
                    //                 kladrpcurrstatus = 0,
                    //                 mirror_guid = "",
                    //                 oktmo = ""

                    //             };
                    //connection.copy_b4_fias.Add(result.Where(co)
                    connection.SaveChanges();


                var b4_fias_house = (from ing in connection.gar_AdmHierCon
                                     join jo in connection.gar_AddObjCon  on ing.PARENTOBJID equals jo.OBJECTID)



            }
             


        }

        private static ADDRESSOBJECTS DeserializeAddrObjs(XElement element)
        {
            StringReader reader = new StringReader(element.ToString());
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ADDRESSOBJECTS));
            return ((ADDRESSOBJECTS)xmlSerializer.Deserialize(reader));
        }
        private static HOUSES DeserializeHouses(XElement element)
        {
            StringReader reader = new StringReader(element.ToString());
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HOUSES));
            return ((HOUSES)xmlSerializer.Deserialize(reader));
        }
        private static ITEMS DeserializeITEM(XElement element)
        {
            StringReader reader = new StringReader(element.ToString());
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ITEMS));
            return ((ITEMS)xmlSerializer.Deserialize(reader));
        }



    }

    internal class NewClass
    {
       
        public string Coderecord { get; set; }
        public int Typerecord { get; set; }
        public int Aolevel { get; set; }
        public int Actstatus { get; set; }
        public int Centstatus { get; set; }
        public int Operstatus { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }
        public string Aoguid { get; set; }
        public string Aoid { get; set; }
        public string Parentguid { get; set; }
        public string Previd { get; set; }
        public string Nextid { get; set; }
        public string Formalname { get; set; }
        public string Offname { get; set; }
        public string Shortname { get; set; }
        public string Regioncode { get; set; }
        public string Autocode { get; set; }
        public string Areacode { get; set; }
        public string Citycode { get; set; }
        public string Ctarcode { get; set; }
        public string Placecode { get; set; }
        public string Streetcode { get; set; }
        public string Extrcode { get; set; }
        public string Sextcode { get; set; }
        public string Postalcode { get; set; }
        public string Ifnsfl { get; set; }
        public string Terrifnsfl { get; set; }
        public string Ifnsul { get; set; }
        public string Terrifnsul { get; set; }
        public string Okato { get; set; }
        public DateTime Updatedate { get; set; }
        public string Normdoc { get; set; }
        public string Kladrcode { get; set; }
        public string Kladrplaincode { get; set; }
        public int Kladrpcurrstatus { get; set; }
        public string Mirror_guid { get; set; }
        public string Oktmo { get; set; }

        public NewClass( string coderecord, int typerecord, int aolevel, int actstatus, int centstatus, int operstatus, DateTime startdate, DateTime enddate, string aoguid, string aoid, string parentguid, string previd, string nextid, string formalname, string offname, string shortname, string regioncode, string autocode, string areacode, string citycode, string ctarcode, string placecode, string streetcode, string extrcode, string sextcode, string postalcode, string ifnsfl, string terrifnsfl, string ifnsul, string terrifnsul, string okato, DateTime updatedate, string normdoc, string kladrcode, string kladrplaincode, int kladrpcurrstatus, string mirror_guid, string oktmo)
        {
           
            Coderecord = coderecord;
            Typerecord = typerecord;
            Aolevel = aolevel;
            Actstatus = actstatus;
            Centstatus = centstatus;
            Operstatus = operstatus;
            Startdate = startdate;
            Enddate = enddate;
            Aoguid = aoguid;
            Aoid = aoid;
            Parentguid = parentguid;
            Previd = previd;
            Nextid = nextid;
            Formalname = formalname;
            Offname = offname;
            Shortname = shortname;
            Regioncode = regioncode;
            Autocode = autocode;
            Areacode = areacode;
            Citycode = citycode;
            Ctarcode = ctarcode;
            Placecode = placecode;
            Streetcode = streetcode;
            Extrcode = extrcode;
            Sextcode = sextcode;
            Postalcode = postalcode;
            Ifnsfl = ifnsfl;
            Terrifnsfl = terrifnsfl;
            Ifnsul = ifnsul;
            Terrifnsul = terrifnsul;
            Okato = okato;
            Updatedate = updatedate;
            Normdoc = normdoc;
            Kladrcode = kladrcode;
            Kladrplaincode = kladrplaincode;
            Kladrpcurrstatus = kladrpcurrstatus;
            Mirror_guid = mirror_guid;
            Oktmo = oktmo;
        }

        public override bool Equals(object obj)
        {
            return obj is NewClass other &&
                   
                   Coderecord == other.Coderecord &&
                   Typerecord == other.Typerecord &&
                   Aolevel == other.Aolevel &&
                   Actstatus == other.Actstatus &&
                   Centstatus == other.Centstatus &&
                   Operstatus == other.Operstatus &&
                   Startdate == other.Startdate &&
                   Enddate == other.Enddate &&
                   Aoguid == other.Aoguid &&
                   Aoid == other.Aoid &&
                   Parentguid == other.Parentguid &&
                   Previd == other.Previd &&
                   Nextid == other.Nextid &&
                   Formalname == other.Formalname &&
                   Offname == other.Offname &&
                   Shortname == other.Shortname &&
                   Regioncode == other.Regioncode &&
                   Autocode == other.Autocode &&
                   Areacode == other.Areacode &&
                   Citycode == other.Citycode &&
                   Ctarcode == other.Ctarcode &&
                   Placecode == other.Placecode &&
                   Streetcode == other.Streetcode &&
                   Extrcode == other.Extrcode &&
                   Sextcode == other.Sextcode &&
                   Postalcode == other.Postalcode &&
                   Ifnsfl == other.Ifnsfl &&
                   Terrifnsfl == other.Terrifnsfl &&
                   Ifnsul == other.Ifnsul &&
                   Terrifnsul == other.Terrifnsul &&
                   Okato == other.Okato &&
                   Updatedate == other.Updatedate &&
                   Normdoc == other.Normdoc &&
                   Kladrcode == other.Kladrcode &&
                   Kladrplaincode == other.Kladrplaincode &&
                   Kladrpcurrstatus == other.Kladrpcurrstatus &&
                   Mirror_guid == other.Mirror_guid &&
                   Oktmo == other.Oktmo;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
           
            hash.Add(Coderecord);
            hash.Add(Typerecord);
            hash.Add(Aolevel);
            hash.Add(Actstatus);
            hash.Add(Centstatus);
            hash.Add(Operstatus);
            hash.Add(Startdate);
            hash.Add(Enddate);
            hash.Add(Aoguid);
            hash.Add(Aoid);
            hash.Add(Parentguid);
            hash.Add(Previd);
            hash.Add(Nextid);
            hash.Add(Formalname);
            hash.Add(Offname);
            hash.Add(Shortname);
            hash.Add(Regioncode);
            hash.Add(Autocode);
            hash.Add(Areacode);
            hash.Add(Citycode);
            hash.Add(Ctarcode);
            hash.Add(Placecode);
            hash.Add(Streetcode);
            hash.Add(Extrcode);
            hash.Add(Sextcode);
            hash.Add(Postalcode);
            hash.Add(Ifnsfl);
            hash.Add(Terrifnsfl);
            hash.Add(Ifnsul);
            hash.Add(Terrifnsul);
            hash.Add(Okato);
            hash.Add(Updatedate);
            hash.Add(Normdoc);
            hash.Add(Kladrcode);
            hash.Add(Kladrplaincode);
            hash.Add(Kladrpcurrstatus);
            hash.Add(Mirror_guid);
            hash.Add(Oktmo);
            return hash.ToHashCode();
        }
    }
}
