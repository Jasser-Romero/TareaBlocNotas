using AppCore.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Services
{
    public class NotaServices : INotasServices
    {
        INotasRepository repository;

        public void Create(string message, string path)
        {
            throw new NotImplementedException();
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public string Read(string path)
        {
            return path;
        }

        
    }
}
