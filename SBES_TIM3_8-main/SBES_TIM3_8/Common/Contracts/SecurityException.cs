using System.Runtime.Serialization;

namespace Common.Contracts
{
    [DataContract]
    public class SecurityException
    {
        [DataMember]
        public string Message { get; set; }

        public SecurityException()
        {

        }

        public SecurityException(string message)
        {
            this.Message = message;
        }

        public override string ToString()
        {
            return this.Message;
        }
    }
}
