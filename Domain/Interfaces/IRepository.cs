using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Create(string message, string path);
        void Read(string path);
        void Delete(string path);
    }
}
