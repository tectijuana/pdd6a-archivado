using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    //El código fuente de la interfaz Observer es muy simple puesto que sólo contiene la firma del método actualiza 
    public interface Observer
    {
        void actualiza();
    }
}

{
    //Los observadores se gestionan mediante una lista
    public abstract class Sujeto
    {
        protected IList<Observer> observadores = new List<Observer>();

        public void agrega (Observer observador)
        {
            observadores.Add(observador);
        }

        public void suprime(Observer observador)
        {
            observadores.Remove(observador);
        }

        public void notifica()
        {
            foreach (Observer observador in observadores)
                observador.actualiza();
        }
    }
}

{
    //Incluye la clase del programa principal. Este programa crea un 
    //vehículo y a continuación una vista a la que pide la visualización.
    //Se modifica el precio y la vista se refresca.
    //Se crea una segunda vista que se asocia al mismo vehículo.
    //El precio se modificaa de nuevo y ambas vistas se refrescan.

    public class Usuario
    {
        static void Main(string[] args)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.descripcion = "Vehiculo económico";
            vehiculo.precio = 50000;
            VistaVehiculo vistaVehiculo = new VistaVehiculo(vehiculo);
            vistaVehiculo.redibuja();
            vehiculo.precio = 4500.0;
            VistaVehiculo vistaVehiculo2 = new VistaVehiculo(vehiculo);
            vehiculo.precio = 5500.0;
            Console.Read();
        }
    }
}

{
    //El código fuente de la clase Vehiculo aparece a continuación. 
    //Contiene dos atributos y los accesos de lectura y escritura para ambos atributos. 
    //Los dos accesos de escritura invocan al método notifica.

    public class Vehiculo : Sujeto
    {
        protected string _descripcion;
        protected double _precio;

        public string descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
                this.notifica();
            }
        }

            public double precio
            {
                get
                {
                    return _precio;
                }
                set 
                {
                    _precio = value;
                    this.notifica();
                }
            }

        }
    }

{
    //La clase VistaVehiculo gestiona un texto que contiene la descripción y el 
    //precio del vehículo asociado (el sujeto). Este texto se actualiza 
    //tras cada notificación en el cuerpo del método actualiza.
    //El méodo redibuja imprime este texto por pantalla.

    public class VistaVehiculo : Observer
    {
        protected Vehiculo vehiculo;
        protected string texto = "";

        public VistaVehiculo (Vehiculo vehiculo)
        {
            this.vehiculo = vehiculo;
            vehiculo.agrega(this);
            actualizarTexto();
        }

        protected void actualizarTexto()
        {
            texto = "Descripcion " + vehiculo.descripcion + " Precio: " + vehiculo.precio;
        }

        public void actualiza()
        {
            actualizarTexto();
            this.redibuja();
        }

        public void redibuja()
        {
            Console.WriteLine(texto);
        }
    }
}

