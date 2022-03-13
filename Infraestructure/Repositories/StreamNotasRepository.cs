using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Infraestructure.Repositories
{
    public class StreamNotasRepository : INotasRepository
    {
        public void Create(string message, string path)
        {
            using (StreamWriter sw = new StreamWriter(@path))
            {
                sw.WriteLine(message);
            }
            
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public string Read(string path)
        {
            string linea;
            string mensaje = string.Empty;
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(@path))
            {
                while ((linea = sr.ReadLine()) != null) 
                {
                    sb.Append(linea + "\n");
                }
            }
            return sb.ToString();
            
        }
    }
}
