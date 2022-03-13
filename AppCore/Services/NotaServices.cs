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
        public NotaServices(INotasRepository model)
        {
            this.repository = model;
        }

        public void Create(string message, string path)
        {
            repository.Create(message, path);
        }

        public void Delete(string path)
        {
            repository.Delete(path);
        }

        public string Read(string path)
        {
            return repository.Read(path);
        }

        
    }
}
