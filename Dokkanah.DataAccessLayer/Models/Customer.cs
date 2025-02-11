using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Dokaanah.Models
{
    public class Customer:IdentityUser
    {
         
		public string                             Password               { get; set; }
		public string?                        confirmPassword            { get; set; } 
        public string?                           Address                 { get; set; }
		public bool?                             isAgree                 { get; set; }
  
        public virtual ICollection<Order>?          Orders                { get; set; } = new List<Order>();
        #region Customer Online or Offline Region


        public bool IsOnline { get; set; } = false;  // لمعرفة هل المستخدم نشط أم لا

        public DateTimeOffset? LastActiveAt { get; set; } // وقت آخر نشاط 


        #endregion
   
    }
}
