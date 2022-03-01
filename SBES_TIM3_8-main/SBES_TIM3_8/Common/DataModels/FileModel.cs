using System;
using System.Linq;

namespace Common.DataModels
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        private byte[] signature;
        private byte[] lastKnownSignature;

        public byte[] Signature => signature;

        public byte[] LastKnownSignature { get => this.lastKnownSignature; set => lastKnownSignature = value; }

        public ErrorLevel FileCriticallity { get; set; }

        public void SetSignature(byte[] signature)
        {
            this.signature = signature.ToArray();
        }

        public FileModel(int id,string name, string path,byte[] signature,ErrorLevel fileCriticality,byte[] lastKnownSignature)
        {
            this.Id = id;
            this.Name = name;
            this.Path = path;
            this.signature = signature;
            this.FileCriticallity = fileCriticality;
            this.LastKnownSignature = lastKnownSignature;
        }

        public FileModel()
        {

        }

        public override string ToString()
        {
            return $"File name:{this.Name} Path:{this.Path}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !obj.GetType().Equals(this.GetType()))
                return false;

            FileModel file = obj as FileModel;

            return this.Path == file.Path;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public void RaiseCriticality()
        {
            if(FileCriticallity != ErrorLevel.Critical)
            {
                this.FileCriticallity = (ErrorLevel)((int)FileCriticallity + 1);
            }
        }


        public static string GetFileNameFromPath(string path)
        {
            string retval = string.Empty;

            if (string.IsNullOrEmpty(path) || !path.Contains('\\'))
            {
                return retval;
            }
            else if(path.Contains('\\'))
            {
                retval = path.Substring(path.LastIndexOf('\\') + 1);
            }
            else if(path.Contains('/'))
            {
                retval = path.Substring(path.LastIndexOf('/') + 1);
            }

            return retval;
        }
    }
}
