using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IronShoes
{
    class DataSource
    {
        public static entity.Trank getDataSource(String str) {
            //首先对字符串进行处理去掉头尾的空格
            //str.TrimStart(' ').TrimEnd(' ');
            entity.Trank tk = new entity.Trank();
            str = Regex.Replace(str, @"\s+", " ").Trim();
            if (str.StartsWith("7E") && str.EndsWith("7E"))
            {
                str=trimFirEndAndChange(str);
                if (checkBcc(str)) {
                    string[] sArray = Regex.Split(str, " "); 
                    
                    String trimId = "";
                    for(int i=0;i<20;i++){
                        trimId+= sArray[i + 4].Remove(0,1);
                    }
                    trimId=trimId.Remove(0,4);

                    String infoNum = ConvertHelper.ConvertBase(sArray[24]+ sArray[25],16,10);
                    //设备类型sArray[26]
                   

                    //设备上传的时间
                    String trimTime = (int.Parse(sArray[27])+2000)+"/"+ sArray[28]+"/"+ sArray[29]+" "+ sArray[30]+":" +sArray[31]+":"+sArray[32];

                    //电压
                    String voltage = (int.Parse(ConvertHelper.ConvertBase(sArray[33] + sArray[34], 16, 10))*0.1) +"";

                    String pith = getPRY(sArray[35] + sArray[36]);

                    String roll = getPRY(sArray[37] + sArray[38]);

                    String yaw = getPRY(sArray[39] + sArray[40]);

                    //经度
                    String longitude = getLonLat(sArray[41] + sArray[42]+ sArray[43] + sArray[44]);

                    //纬度
                    String latitude = getLonLat(sArray[45] + sArray[46] + sArray[47] + sArray[48]);

                    //距离
                    String distance = getTempDis(sArray[49] + sArray[50]);

                    //温度
                    String temp = getTempDis(sArray[51] + sArray[52]);

                    String statValue = getStatValue(sArray[53] + sArray[54]);
                    String lotrpu = ConvertHelper.ConvertBase(statValue, 16, 2);
                    lotrpu = ConvertHelper.RepairZero(lotrpu, 16);
                    String loca = ConvertHelper.ConvertBase(lotrpu.Substring(8, 8),2,10);
                    String trno = ConvertHelper.ConvertBase(lotrpu.Substring(5, 3), 2,10);
                    String putsta = ConvertHelper.ConvertBase(lotrpu.Substring(3, 2), 2, 10);

                    //人力制动状态码
                    String brakestatusCode = sArray[55] + sArray[56];

                    //电流
                    String electricity = ""+(int.Parse(ConvertHelper.ConvertBase(sArray[57] + sArray[58], 16, 10)) * 0.1);

                    String Gprs = gteGPRS(sArray[57], sArray[58]);
                    
                    tk.TrimId = trimId;
                    tk.InfoNum = infoNum;
                    tk.TrimTime = trimTime;
                    tk.Voltage = voltage;
                    tk.Pith = pith;
                    tk.Roll = roll;
                    tk.Yaw = yaw;
                    tk.Longitude = longitude;
                    tk.Latitude = latitude;
                    tk.Distance = distance;
                    tk.Temp = temp;
                    tk.StatValue = statValue;
                    tk.Loca = loca;
                    tk.Trno = trno;
                    tk.Putsta = putsta;
                    tk.BrakestatusCode = brakestatusCode;
                    tk.Electricity = electricity;
                    tk.GPRS1 = Gprs;


                    str = "trimId:" + trimId+ "infoNum:"+ infoNum+ "trimTime:"+ trimTime+ "voltage:"
                    +voltage+ "pith:"+ pith+ "roll:"+ roll+ "yaw:"+ yaw+ "longitude:"+
                    longitude+ "latitude:"+ latitude+ "distance:"+ 
                        distance+ "temp:"+ temp+ "statValue:"+ statValue+ "loca:"+ loca+ "trno:"+ trno+ "putsta:"+ putsta+ "brakestatusCode:"+ brakestatusCode+ "electricity:"+ electricity;
                    Console.WriteLine("str:" + str);

                    return tk;
                }
                //



            }


            else {
                str = "7E数据不匹配";
            }

            

            return tk;
        }

        private static string gteGPRS(string v1, string v2)
        {
            String str = "";
            if (v1.Equals("04"))
            {
                str += "已上线,";
            }
            else {
                str += "未上线,";
            }
            if (v2.Equals("00"))
            {
                str += "正常,";
            }
            else if(v2.Equals("01") || v2.Equals("10"))
            {
                str += "模块未通信,";
            }
            else if (v2.Equals("02") || v2.Equals("20"))
            {
                str += "信号弱,";
            }
            else if (v2.Equals("03") || v2.Equals("30"))//4-未设置中心，5-拨号故障，6-服务器故障，7-RTCM故障；
            {
                str += "未插卡,";
            }
            else if (v2.Equals("04") || v2.Equals("40"))
            {
                str += "未设置中心,";
            }
            else if (v2.Equals("05") || v2.Equals("50"))
            {
                str += "拨号故障,";
            }
            else if (v2.Equals("06") || v2.Equals("60"))
            {
                str += "服务器故障,";
            }
            else if (v2.Equals("07") || v2.Equals("70"))
            {
                str += "RTCM故障,";
            }

            if ((str[str.Length - 1] + "").Equals(","))
            {
                str = str.Remove(str.Length - 1, 1);
            }
            return str;
        }

        private static String getStatValue(String str)
        {
            String statValue = "";
            str =ConvertHelper.ConvertBase(str, 16, 2);
            str = ConvertHelper.RepairZero(str,16);
            Console.WriteLine("str:" + str);
            //00：待放置，01：上道，10：入柜，11：预留
            String s1 = str.Substring(14,2);

            //充电状态（1：充电，0：未充电）；
            String s2 = str.Substring(13, 1);
            //bit5：故障状态（1：铁鞋故障；0：铁鞋正常）；
            String s3 = str.Substring(12, 1);
            //bit6：DIN1输入状态
            String s4 = str.Substring(11, 1);
            //bit7：DIN2输入状态；
            String s5 = str.Substring(10, 1);

            //bit8-9: 姿态报警（00：无报警，01：倾斜报警，10：拉斜报警，11：倾倒报警）；
            String s6 = str.Substring(8, 2);

            //bit10：欠压告警（0：无报警，1：欠压报警）；
            String s7 = str.Substring(7, 1);

            String s8 = str.Substring(5, 2);

            String s9 = str.Substring(3, 2);

            if (s1.Equals("00")) {
                statValue += "待放置,";
            }
            else if (s1.Equals("01")) {
                statValue += "上道,";
            }
            else if (s1.Equals("10"))
            {
                statValue += "入柜,";
            }
            else if (s1.Equals("11"))
            {
                statValue += "预留,";
            }

            if (s2.Equals("0"))
            {
                statValue += "未充电,";
            }
            else if (s2.Equals("1"))
            {
                
                statValue += "充电,";
            }

            if (s3.Equals("0"))
            {
                statValue += "铁鞋正常,";
            }
            else if (s3.Equals("1"))
            {
                statValue += "铁鞋故障,";
            }

            if (s4.Equals("0"))
            {
                statValue += "DIN1输入状态0,";
            }
            else if (s4.Equals("1"))
            {
                statValue += "DIN1输入状态1,";
            }

            if (s5.Equals("0"))
            {
                statValue += "DIN2输入状态0,";
            }
            else if (s5.Equals("1"))
            {
                statValue += "DIN2输入状态1,";
            }

            if (s6.Equals("00"))
            {
                statValue += "姿态无报警,";
            }
            else if (s6.Equals("01"))
            {
                statValue += "倾斜报警,";
            }
            else if (s6.Equals("10"))
            {
                statValue += "拉斜报警,";
            }
            else if (s6.Equals("11"))
            {
                statValue += "倾倒报警,";
            }


            if (s7.Equals("0"))
            {
                statValue += "无报警,";
            }
            else if (s7.Equals("1"))
            {
                statValue += "欠压报警,";
            }

            if (s8.Equals("00"))
            {
                statValue += "无报警,";
            }
            else if (s8.Equals("01"))
            {
                statValue += "距离过近报警,";
            }
            else if (s8.Equals("10"))
            {
                statValue += "距离过远报警,";
            }
            else if (s8.Equals("11"))
            {
                statValue += "预留,";
            }

            if (s9.Equals("00"))
            {
                statValue += "无报警,";
            }
            else if (s9.Equals("01"))
            {
                statValue += "温度上限报警,";
            }
            else if (s9.Equals("10"))
            {
                statValue += "温度下限报警,";
            }
            else if (s9.Equals("11"))
            {
                statValue += "预留,";
            }

            if ((statValue[statValue.Length - 1] + "").Equals(",")) {
                statValue = statValue.Remove(statValue.Length - 1,1);
            }
            return statValue ;
        }

        private static String getTempDis(String str) {
            return "" + (int.Parse(ConvertHelper.ConvertBase(str, 16, 10))) * 0.1;
        }
        private static string getLonLat(String str) {
            return ""+(int.Parse(ConvertHelper.ConvertBase(str, 16, 10))) * 0.000001;
        }

        private static string getPRY(string str) {
            //先把十六进制转成二进制的判断高位
            String s1=ConvertHelper.ConvertBase(str,16,2);
            if (s1.Length < 16)
            {
                return ""+(int.Parse(ConvertHelper.ConvertBase(s1, 2, 10))) * 0.1;
            }
            else {
                if ((s1[0]+"").Equals("1")) {
                    s1 = s1.Remove(0,1);
                    return "" + (int.Parse(ConvertHelper.ConvertBase(s1, 2, 10))) * -0.1;
                }
                else {
                    return "" + (int.Parse(ConvertHelper.ConvertBase(s1, 2, 10))) * 0.1;
                }
            }

            return "";
        }

        private static bool checkBcc(String str) {
            bool boolean = false;
            String da = str.Remove(str.Length - 3, 3);
            //十六进制转Byte数组先去空
            da = da.Replace(" ", "");
            String bccVal=str.Substring(str.Length-2,2);

            String HexBcc=GetBCCXorCode(ConvertHelper.HexStringToBytes(da));

            if (bccVal.Equals(HexBcc, StringComparison.CurrentCultureIgnoreCase)) {
                boolean = true;
            }
            else {
                boolean = false;
            }

            return boolean;
        }


        //去掉首尾字符替换转义
        private static String trimFirEndAndChange(String str) {
            str = GetString(str, "7E", true);
            str = Regex.Replace(str, @"\s+", " ").Trim();
            if (str.ToLower().Contains("7D 02".ToLower()))
            {
                str = str.Replace("7D 02", "7E");
            }
            if (str.ToLower().Contains("7D 01".ToLower()))
            {
                str = str.Replace("7D 01", "7D");
            }
            return str;
        }

        //去掉首尾字符串
        private static string GetString(string val, string str, bool all)
        {
            return Regex.Replace(val, @"(^(" + str + ")" + (all ? "*" : "") + "|(" + str + ")" + (all ? "*" : "") + "$)", "");
        }

        //bcc校验
        public static string GetBCCXorCode(byte[] data)
        {
            byte CheckCode = 0;
            int len = data.Length;
            for (int i = 0; i < len; i++)
            {
                CheckCode ^= data[i];
            }
            return Convert.ToString(CheckCode, 16).ToUpper();
        }
    }
}
