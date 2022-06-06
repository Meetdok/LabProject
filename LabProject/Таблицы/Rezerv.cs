using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LabProject.Tools;

namespace LabProject.Таблицы
{
    [Table("rezerv")]
    public class Rezerv : BaseDTO
    {     
        [Column("datastart")]
        public DateTime DateStart { get; set; } = DateTime.Now;

        [Column("dataend")]
        public DateTime DateEnd { get; set; } = DateTime.Now;

        [Column("equipment_id")]
        public int EquipmentId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        public Users Users { get; set; }
        public Equipment Equipment { get; set; }     
    }
}
