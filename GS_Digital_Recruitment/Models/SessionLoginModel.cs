using System;

namespace GS_Digital_Recruitment.Models
{
    public class SessionLogin
    {
        public string fullname { get; set; }
        public string username { get; set; }
        public int customer { get; set; }
        public string batt_category { get; set; }
        public string batt_segmentation { get; set; }
        public string periodic_price { get; set; }
        public int country { get; set; }
        public string npk { get; set; }
        public string userrole { get; set; }
        public string userdepartment { get; set; }
        public string userplant { get; set; }
        public DateTime? login_date { get; set; }
    }

}