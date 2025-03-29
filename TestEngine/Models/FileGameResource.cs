using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestEngine.Models
{
    public enum FileGameType
    {
        Raw,
        Text,
        Texture,
        Explanator
    }
    public class FileGameResource<T> where T: class
    {
        public T Data{get; private set;}
        public FileGameType FileType {get; private set;}

        public FileGameResource(FileGameType fileType, T data)
        {
            FileType = fileType;
            Data = data;
        }
    }
}