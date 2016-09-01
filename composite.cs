using System;
using System.Collections.Generic;
namespace Rextester
{
public class Program
{
static void Main(string[] args)
{
Compuesto raiz = new Compuesto("root");
raiz.Agregar(new Hoja("hoja A"));
raiz.Agregar(new Hoja("hoja B"));
Compuesto comp = new Compuesto("compuesto X");
comp.Agregar(new Hoja("hoja XA"));
comp.Agregar(new Hoja("hoja XB"));
raiz.Agregar(comp);
raiz.Agregar(new Hoja("hoja C"));
Hoja l = new Hoja("hoja D");
raiz.Agregar(l);
raiz.Eliminar(l);
raiz.Mostrar(1);
}
}

public abstract class Componente
{
    protected string nombre;
    public Componente (string nombre)
    {
        this.nombre = nombre;
    }
    public abstract void Agregar(Componente componente);
    public abstract void Eliminar(Componente componente);
    public abstract void Mostrar(int profundidad);
}

public class Compuesto: Componente
{
    private List<Componente> hijo = new List<Componente>();
    public Compuesto (string nombre): base(nombre) { }
    public override void Agregar(Componente componente)
    {
        hijo.Add(componente);
    }
    public override void Eliminar(Componente componente)
    {
        hijo.Remove(componente);
    }
    public override void Mostrar(int profundidad)
    {
        Console.WriteLine(string.Format("{0} nivel: {1}", nombre, profundidad));
        for (int i = 0; i < hijo.Count; i++)
            hijo[i].Mostrar(profundidad + 1);
    }
}

public class Hoja: Componente
{
    public Hoja(string nombre): base(nombre) { }
    public override void Agregar(Componente componente)
    {
        Console.WriteLine("no se puede agregar la hoja");
    }
    public override void Eliminar(Componente componente)
    {
        Console.WriteLine("no se puede eliminar la hoja");
    }
    public override void Mostrar(int profundidad)
    {
        Console.WriteLine(string.Format("-{0}", nombre));
    }
}
}