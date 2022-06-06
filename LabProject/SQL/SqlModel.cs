using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Model;
using LabProject.Tools;
using LabProject.Таблицы;
using MySql.Data.MySqlClient;

namespace LabProject.SQL
{
    public class SqlModel
    {
        private SqlModel() { }
        static SqlModel sqlModel;
        public static SqlModel GetInstance()
        {
            if (sqlModel == null)
                sqlModel = new SqlModel();
            return sqlModel;
        }

        public List<Equipment> EquisView()
        {
            var equis = new List<Equipment>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"select * from equipment, users where responsible_id = users.ID";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        equis.Add(new Equipment
                        {
                            ID = dr.GetInt32("id"),
                            Title = dr.GetString("title"),                            
                            Size = dr.GetString("size"),
                            DataBuy = dr.GetDateTime("databuy"),
                            ResponsibleId = dr.GetInt32("responsible_id"),

                            Users = new Users { ID = dr.GetInt32("responsible_id"), LastName = dr.GetString("lastname") }
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return equis;
        }

        public List<Rezerv> RezView()
        {
            var rezs = new List<Rezerv>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"select * from rezerv, users, equipment where user_id = users.ID and equipment_id = equipment.ID";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        rezs.Add(new Rezerv
                        {
                            ID = dr.GetInt32("id"),
                            DateStart = dr.GetDateTime("datastart"),
                            DateEnd = dr.GetDateTime("dataend"),
                            EquipmentId = dr.GetInt32("equipment_id"),
                            UserId = dr.GetInt32("user_id"),
                            Users = new Users { ID = dr.GetInt32("user_id"), LastName = dr.GetString("lastname") },
                             Equipment = new Equipment { ID = dr.GetInt32("equipment_id"), Title = dr.GetString("title") }
                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return rezs;
        }

        public List<Users> UsersView()
        {
            var users = new List<Users>();
            var mySqlDB = MySqlDB.GetDB();
            string query = $"select * from users, position where position_id = position.ID";
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        users.Add(new Users
                        {
                            ID = dr.GetInt32("id"),
                            FirstName = dr.GetString("firstname"),
                            LastName = dr.GetString("lastname"),
                            Patronymic = dr.GetString("patronymic"),
                            LoginUser = dr.GetString("login"),
                            PassUser = dr.GetString("pass"),
                            Phonenumber = dr.GetString("phonenumber"),
                            Adress = dr.GetString("adress"),
                            Passport = dr.GetString("passport"),
                            PositionId = dr.GetInt32("position_id"),

                            Position = new Position { ID = dr.GetInt32("position_id"), Name = dr.GetString("name") }

                        });                       
                    }
                }
                mySqlDB.CloseConnection();
            }
            return users;
        }



        public int Insert<T>(T value)
        {
            string table;
            List<MetaValue> values;
            GetMetaData(value, out table, out values);
            var query = CreateInsertQuery(table, values);
            var db = MySqlDB.GetDB();
            // лучше эти 2 запроса объединить в один с помощью транзакции
            int id = db.GetNextID(table);
            db.ExecuteNonQuery(query.Item1, query.Item2);
            return id;
        }
        // обновляет объект в бд по его id
        public void Update<T>(T value) where T : BaseDTO
        {
            string table;
            List<MetaValue> values;
            GetMetaData(value, out table, out values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDB.GetDB();
            db.ExecuteNonQuery(query.Item1, query.Item2);
        }

        public void Delete<T>(T value) where T : BaseDTO
        {
            var type = value.GetType();
            string table = GetTableName(type);
            var db = MySqlDB.GetDB();
            string query = $"delete from `{table}` where id = {value.ID}";
            db.ExecuteNonQuery(query);
        }

        public int GetNumRows(Type type)
        {
            string table = GetTableName(type);
            return MySqlDB.GetDB().GetRowsCount(table);
        }

        private static string GetTableName(Type type)
        {
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            return ((TableAttribute)tableAtrributes.First()).Table;
        }



        private static (string, MySqlParameter[]) CreateInsertQuery(string table, List<MetaValue> values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"INSERT INTO `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<MetaValue> values, int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE id = {id}");
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static List<MySqlParameter> InitParameters(List<MetaValue> values, StringBuilder stringBuilder)
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.Value));
                return $"{s.Name} = @p{count++}";
            });
            stringBuilder.Append(string.Join(",", rows));
            return parameters;
        }

        private static void GetMetaData<T>(T value, out string table, out List<MetaValue> values)
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Table;
            values = new List<MetaValue>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Column;
                    values.Add(new MetaValue { Name = column, Value = prop.GetValue(value) });
                }
            }
        }

        class MetaValue
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
