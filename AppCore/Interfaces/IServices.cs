using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Interfaces
{
    public interface IServices<T>
    {
        void Create(string message, string path);
        string Read(string path);
        void Delete(string path);
    }
}
