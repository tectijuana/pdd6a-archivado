using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patrón_Command
{
    class Program
    {
        public interface ICommand
        {
            // El método Execute() será aquel que el objeto que reciba la referencia
            // será capaz de ejecutar.
            void Execute();
        }

        // Clase abstracta de la que heredarán las clases que serán encapsuladas por los
        // objectos Command. Por lo tanto, sus métodos serán aquellos que encapsulará el
        // método Execute().
        public abstract class LucesReceiver
        {
            protected bool encendidas;
            protected int distanciaAlumbrado;

            // Propiedad de sólo lectura que devolverá el estado de las luces
            public bool Encendidas
            {
                get { return encendidas; }
            }

            // Método encargado de apagar las luces. Establece el estado del atributo 'encendidas'
            // a 'false'. Será común a todas las clases que hereden de ella.
            public void Apagar()
            {
                this.encendidas = false;
            }

            // El método Encender será distinto en cada una de las clases que hereden de esta clase.
            public abstract int Encender();
        }

        //Luces posición
        public class LucesPosicion : LucesReceiver
        {
            private const int DISTANCIA = 1;

            public override int Encender()
            {
                this.encendidas = true;
                return DISTANCIA;
            }
        }

        //Luces Cortas
        public class LucesCortas : LucesReceiver
        {
            private const int DISTANCIA = 40;

            public override int Encender()
            {
                this.encendidas = true;
                return DISTANCIA;
            }
        }

        //Luces largas
        public class LucesLargas : LucesReceiver
        {
            private const int DISTANCIA = 200;

            public override int Encender()
            {
                this.encendidas = true;
                return DISTANCIA;
            }
        }

        //Iinvoker
        public interface IInvoker
        {
            // Inyecta un ICommand, permitiendo cambiar la operación encapsulada en
            // tiempo de ejecución
            void SetCommand(ICommand command);

            // Ejecuta el método encapsulado
            void Invoke();
        }

        public class ControladorLucesInvoker : IInvoker
        {
            ICommand command;

            public void SetCommand(ICommand command)
            {
                this.command = command;
            }

            public void Invoke()
            {
                command.Execute();
            }
        }

        //Encender luces Command
        public class EncenderLucesCommand : ICommand
        {
            // Referencia al objeto cuyo método se quiere encapsular
            private LucesReceiver luces;

            // El constructor inyectará el objeto cuyo método se quiere encapsular
            public EncenderLucesCommand(LucesReceiver luces)
            {
                this.luces = luces;
            }

            // El método Execute() ejecutará el método encapsulado
            public void Execute()
            {
                int distancia = luces.Encender();
                Console.WriteLine(string.Format("Encendiendo luces. Alumbrando a una distancia de {0} metros.", distancia));
            }
        }

        //Apagar luces Command
        public class ApagarLucesCommand : ICommand
        {
            private LucesReceiver luces;

            public ApagarLucesCommand(LucesReceiver luces)
            {
                this.luces = luces;
            }

            public void Execute()
            {
                luces.Apagar();
                Console.WriteLine("Apagando las luces");
            }
        }

        static void Main(string[] args)
        {
            // Instanciamos los objetos cuyos métodos serán encapsulados dentro de
            // objetos que implementan ICommand
            LucesReceiver lucesPosicion = new LucesPosicion();
            LucesReceiver lucesCortas = new LucesCortas();
            LucesReceiver lucesLargas = new LucesLargas();

            // Creamos los objetos Command que encapsulan los métodos de las clases anteriores
            ICommand encenderPosicion = new EncenderLucesCommand(lucesPosicion);
            ICommand apagarPosicion = new ApagarLucesCommand(lucesPosicion);

            ICommand encenderCortas = new EncenderLucesCommand(lucesCortas);
            ICommand apagarCortas = new ApagarLucesCommand(lucesCortas);

            ICommand encenderLargas = new EncenderLucesCommand(lucesLargas);
            ICommand apagarLargas = new ApagarLucesCommand(lucesLargas);

            // Creamos un nuevo Invoker (el objeto que será desacoplado de las luces)
            IInvoker invoker = new ControladorLucesInvoker();

            // Le asociamos los objetos Command y los ejecutamos
            // El objeto invoker invoca el método 'Execute()' sin saber en ningún momento
            // qué es lo que se está ejecutando realmente.
            invoker.SetCommand(encenderPosicion);      // Asociamos el ICommand
            invoker.Invoke();                          // Hacemos que se ejecute ICommand.Execute()

            // Realizamos lo mismo con el resto de instancias que implementan ICommand.
            // Como podemos ver, el ICommand puede asignarse en tiempo de ejecucion
            invoker.SetCommand(apagarPosicion);
            invoker.Invoke();

            // Luces cortas
            invoker.SetCommand(encenderCortas);
            invoker.Invoke();

            invoker.SetCommand(apagarCortas);
            invoker.Invoke();

            // Luces largas
            invoker.SetCommand(encenderLargas);
            invoker.Invoke();

            invoker.SetCommand(apagarLargas);
            invoker.Invoke();

            Console.ReadKey();
        }
    }
}
