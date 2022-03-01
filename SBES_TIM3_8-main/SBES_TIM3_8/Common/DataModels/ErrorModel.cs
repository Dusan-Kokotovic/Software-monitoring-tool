using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Common.DataModels
{
    
    public enum ErrorLevel
    {
        [EnumMember]
        Information = 1,
        [EnumMember]
        Warning = 2,
        [EnumMember]
        Critical = 3,
        [EnumMember]
        NoError = 0
    };


    [DataContract]
    public class ErrorModel
    {

        [DataMember]
        public string FileName { get; set; }
        
        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public ErrorLevel ErrorLevel { get; set; }

        [DataMember]
        public DateTime DetectionDateTime { get; set; }


        public ErrorModel()
        {

        }

        public ErrorModel(string fileName,string filePath,ErrorLevel errorLevel,DateTime detectionDateTime)
        {
            this.FileName = fileName;
            this.FilePath = filePath;
            this.ErrorLevel = errorLevel;
            this.DetectionDateTime = detectionDateTime;
        }

        public override string ToString()
        {
            return $"File name:{this.FileName}\n" +
                $"File path:{this.FilePath}\n" +
                $"Criticality level:{this.ErrorLevel.ToString()}\n" +
                $"Detection time:{this.DetectionDateTime}";
        }


    }
}
