using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Connections;
using DataAccess.Utils;

namespace DataAccess.DAO.Impl
{
    class DataDAOImpl : IDataDAO
    {
        public IEnumerable<Data> FindAll()
        {
            string query = "select * from db";
            List<Data> dataList = new List<Data>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Data data = new Data(reader.GetFloat(0), reader.GetDateTime(1), reader.GetDateTime(2));
                            dataList.Add(data);
                        }
                    }
                }
            }

            return dataList;
        }

        public void Save(Data entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                Save(entity, connection);
            }
        }

        private void Save(Data data, IDbConnection connection)
        {
            // id_th intentionally in the last place, so that the order between commands remains the same
            string insertSql = "insert into db (potrosnja, vremeProracuna, poslednjeVremeMerenja) " +
                "values (:potrosnja, :vremeProracuna , :poslednjeVremeMerenja)";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = insertSql;
                ParameterUtil.AddParameter(command, "potrosnja", DbType.String, 50);
                ParameterUtil.AddParameter(command, "vremeProracuna", DbType.String, 50);
                ParameterUtil.AddParameter(command, "poslednjeVremeMerenja", DbType.String, 50);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "potrosnja", data.Usage);
                ParameterUtil.SetParameterValue(command, "vremeProracuna", data.DateAndTime);
                ParameterUtil.SetParameterValue(command, "poslednjeVremeMerenja", data.LastDateAndTime);
                command.ExecuteNonQuery();
            }
        }
    }
}
