using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Tools;

namespace LabProject.Таблицы
{
    [Table("position")]
    public class Position : BaseDTO
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
