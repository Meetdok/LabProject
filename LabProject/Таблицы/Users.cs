using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Tools;

namespace LabProject.Таблицы
{
    [Table("users")]
    public class Users : BaseDTO
    {
        [Column("firstname")]
        public string FirstName { get; set; }

        [Column("lastname")]
        public string LastName { get; set; }

        [Column("patronymic")]
        public string Patronymic { get; set; }

        [Column("login")]
        public string LoginUser { get; set; }

        [Column("pass")]
        public string PassUser { get; set; }

        [Column("phonenumber")]
        public string Phonenumber { get; set; }

        [Column("adress")]
        public string Adress { get; set; }

        [Column("passport")]
        public string Passport { get; set; }

        [Column("position_id")]
        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
