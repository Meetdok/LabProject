using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Tools;

namespace LabProject.Таблицы
{
    [Table("equipment")]
    public class Equipment : BaseDTO
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("databuy")]
        public DateTime DataBuy { get; set; }

        [Column("size")]
        public string Size { get; set; }

        [Column("responsible_id")]
        public int ResponsibleId { get; set; }

        public Users Users { get; set; }
    }
}
