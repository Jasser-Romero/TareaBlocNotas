using AppCore.Interfaces;
using AppCore.Services;
using Autofac;
using BlocNotas.Formularios;
using Domain.Interfaces;
using Infraestructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlocNotas
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();

            builder.RegisterType<StreamNotasRepository>().As<INotasRepository>();
            builder.RegisterType<NotaServices>().As<INotasServices>();

            var container = builder.Build();

            Application.Run(new FrmMenu(container.Resolve<INotasServices>()));
        }
    }
}
