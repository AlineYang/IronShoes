using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronShoes.entity
{
    class Trank
    {
        private String trimId;
        private String infoNum;
        private String trimTime;
        private String voltage;
        private String pith;
        private String roll;
        private String yaw;
        private String longitude;
        private String latitude;
        private String distance;
        private String temp;
        private String statValue;
        private String loca;
        private String trno;
        private String putsta;
        private String brakestatusCode;
        private String electricity;
        private String GPRS;

        public string TrimId { get => trimId; set => trimId = value; }
        public string InfoNum { get => infoNum; set => infoNum = value; }
        public string TrimTime { get => trimTime; set => trimTime = value; }
        public string Voltage { get => voltage; set => voltage = value; }
        public string Pith { get => pith; set => pith = value; }
        public string Roll { get => roll; set => roll = value; }
        public string Yaw { get => yaw; set => yaw = value; }
        public string Longitude { get => longitude; set => longitude = value; }
        public string Latitude { get => latitude; set => latitude = value; }
        public string Distance { get => distance; set => distance = value; }
        public string Temp { get => temp; set => temp = value; }
        public string StatValue { get => statValue; set => statValue = value; }
        public string Loca { get => loca; set => loca = value; }
        public string Trno { get => trno; set => trno = value; }
        public string Putsta { get => putsta; set => putsta = value; }
        public string BrakestatusCode { get => brakestatusCode; set => brakestatusCode = value; }
        public string Electricity { get => electricity; set => electricity = value; }
        public string GPRS1 { get => GPRS; set => GPRS = value; }
    }
}
