using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEngine.Models
{
    [Serializable]
    public class FileGameModel
    {
        public string Name {get;set;}
        public string Content {get;set;}
        public string Alias {get;set;}
        public FileGameType FileType {get;set;}
    }
}