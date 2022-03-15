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
            try
            {
                using (StreamWriter sw = new StreamWriter(@path))
                {
                    sw.WriteLine(message);
                }
            } catch(IOException ex)
            {
                throw ex;
            }
            
            
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public string Read(string path)
        {
            string linea;
            StringBuilder sb = new StringBuilder();
            try
            {
                using (StreamReader sr = new StreamReader(@path))
                {
                    while ((linea = sr.ReadLine()) != null)
                    {
                        sb.Append(linea + "\n");
                    }
                }
                return sb.ToString();
            }
            catch (IOException ex)
            {
                throw ex;
            }
            
            
        }
    }
}
