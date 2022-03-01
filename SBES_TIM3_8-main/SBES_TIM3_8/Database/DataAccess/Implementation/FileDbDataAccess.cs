using System;
using System.Collections.Generic;
using Database.Connection;
using System.Data;
using Common.DataModels;

namespace Database.DataAccess.Implementation
{
    public class FileDbDataAccess : IFileCRUD
    {

        public int Count()
        {
            string query = "select * from files";

            using(var connection = PooledConnection.GetConnection())
            {
                connection.Open();
                using(IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void Delete(FileModel entity)
        {
            DeleteById(entity.Id);
        }


        private void DeleteById(int id)
        {
            string query = "delete from files where id = $id";
            using (var connection = PooledConnection.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    Utils.ParameterUtil.AddParameter(command, "id", DbType.Int32);
                    command.Prepare();
                    Utils.ParameterUtil.SetParameterValue(command, "id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Exists(int id)
        {
            using(var connection = PooledConnection.GetConnection())
            {
                connection.Open();
                return ExistsById(id, connection);
            }
        }

        private bool ExistsById(int id,IDbConnection connection)
        {
            string query = "select * from files where id = $id";

            using(IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                Utils.ParameterUtil.AddParameter(command, "id", DbType.Int32);
                command.Prepare();
                Utils.ParameterUtil.SetParameterValue(command, "id", id);

                return command.ExecuteScalar() != null;
            }

        }

        public IEnumerable<FileModel> GetAll()
        {
            List<FileModel> files = new List<FileModel>();

            string query = "select * from files";
            using (var connection = PooledConnection.GetConnection())
            {
                connection.Open();

                using(IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    using(IDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            files.Add(this.ReadFileFromReader(reader));
                        }
                    }
                }
            }

            return files;
        }

        public FileModel GetById(int id)
        {
            string query = "select * from files where id = $id";
            FileModel file = null;

            using (var connection = PooledConnection.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    Utils.ParameterUtil.AddParameter(command, "id", DbType.Int32);
                    command.Prepare();
                    Utils.ParameterUtil.SetParameterValue(command, "id", id);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            file = ReadFileFromReader(reader);
                        }
                    }
                }
            }
            return file;
        }

        public void DeleteByPath(string path)
        {
            string query = "delete from files where path = $path";

            using (var connection = PooledConnection.GetConnection())
            {
                connection.Open();

                using(var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    Utils.ParameterUtil.AddParameter(command, "path", DbType.String);
                    command.Prepare();
                    Utils.ParameterUtil.SetParameterValue(command, "path", path);

                    command.ExecuteNonQuery();
                }
            }
        }

        private FileModel ReadFileFromReader(IDataReader reader)
        {
            return new FileModel(
                reader.GetInt32(0), 
                reader.GetString(1), 
                reader.GetString(2), 
                Convert.FromBase64String(reader.GetString(3)),
                (ErrorLevel)reader.GetInt32(4),
                Convert.FromBase64String(reader.GetString(5))
                );
        }

        public void Insert(FileModel entity)
        {
            string insert = "insert into files(id,name,path,signature,criticality,lastKnownSignature) values((select max(id) from files) + 1,$name,$path,$signature,$criticality,$lastKnownSignature)";
            using(var connection = PooledConnection.GetConnection())
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = insert;
                    Utils.ParameterUtil.AddParameter(command, "name", DbType.String);
                    Utils.ParameterUtil.AddParameter(command, "path", DbType.String);
                    Utils.ParameterUtil.AddParameter(command, "signature", DbType.String);
                    Utils.ParameterUtil.AddParameter(command, "criticality", DbType.Int32);
                    Utils.ParameterUtil.AddParameter(command, "lastKnownSignature", DbType.String);
                    command.Prepare();
                    Utils.ParameterUtil.SetParameterValue(command, "name", entity.Name);
                    Utils.ParameterUtil.SetParameterValue(command, "path", entity.Path);
                    Utils.ParameterUtil.SetParameterValue(command, "signature", Convert.ToBase64String(entity.Signature));
                    Utils.ParameterUtil.SetParameterValue(command, "criticality", (int)entity.FileCriticallity);
                    Utils.ParameterUtil.SetParameterValue(command, "lastKnownSignature", Convert.ToBase64String(entity.LastKnownSignature));
                    command.ExecuteNonQuery();
                }
            }


        }   

        public void Update(FileModel entity)
        {
            string update = "update files set criticality = $criticality,signature = $signature, lastKnownSignature = $lastKnownSignature where id = $id";

            using(var connection = PooledConnection.GetConnection())
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = update;
                    Utils.ParameterUtil.AddParameter(command, "criticality", DbType.Int32);
                    Utils.ParameterUtil.AddParameter(command, "signature", DbType.String);
                    Utils.ParameterUtil.AddParameter(command, "lastKnownSignature", DbType.String);
                    Utils.ParameterUtil.AddParameter(command, "id", DbType.Int32);

                    command.Prepare();
                    Utils.ParameterUtil.SetParameterValue(command, "criticality", (int)entity.FileCriticallity);
                    Utils.ParameterUtil.SetParameterValue(command, "signature", Convert.ToBase64String(entity.Signature));
                    Utils.ParameterUtil.SetParameterValue(command, "lastKnownSignature", Convert.ToBase64String(entity.LastKnownSignature));
                    Utils.ParameterUtil.SetParameterValue(command, "id", entity.Id);

                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateLastKnownSignature(FileModel entity)
        {
            string update = "update files set lastKnownSignature = $lastKnownSignature,criticality = $criticality where id = $id";

            using(var connection = PooledConnection.GetConnection())
            {
                connection.Open();
                using(var command = connection.CreateCommand())
                {
                    command.CommandText = update;
                    Utils.ParameterUtil.AddParameter(command, "lastKnownSignature", DbType.String);
                    Utils.ParameterUtil.AddParameter(command, "id", DbType.Int32);
                    Utils.ParameterUtil.AddParameter(command, "criticality", DbType.Int32);

                    command.Prepare();
                    Utils.ParameterUtil.SetParameterValue(command, "lastKnownSignature", Convert.ToBase64String(entity.LastKnownSignature));
                    Utils.ParameterUtil.SetParameterValue(command, "id", entity.Id);
                    Utils.ParameterUtil.SetParameterValue(command, "criticality", (int)entity.FileCriticallity);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
