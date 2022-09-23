namespace WebAppTacos.ViewModels
{
    using System;
    using System.Drawing;
    public class OrderRows
    {
        public int TilausID { get; set; }
        public int TilausriviID { get; set; }
        public int TuoteID { get; set; }
        public int Maara { get; set; }
        public float Hinta { get; set; }
        public string Nimi { get; set; }
        public int varMaara { get; set; }
        public int TuoteryhmaID { get; set; }
        public string Tuoteryhmanimi { get; set; }
        public string Kuvaus { get; set; }

    }
}