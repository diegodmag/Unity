using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using Unity.VisualScripting;

public class TSPChromosome : ChromosomeBase
{
    private readonly int m_numberOfCities; 

    /**
     * Creamos el chromosoma de una solucion para el agente viajero, hereda de 
     * la clase ChromosomeBase y tiene un unico atributo que es el numero de ciudades.
     * El constructor hereda el constructor que recibe un int de la clase padre.
     */

    public TSPChromosome(int numberOfCities): base(numberOfCities)
    {
        m_numberOfCities= numberOfCities;
        /**
         * El método GetUniqueInts regresa un arreglo de enteros por medio de recibir : 
         * 1: la longitud del arreglo a regresar, en este caso un arreglo con el numero de ciudades 
         * 2: el valor mínimo que se encuentra entre los enteros (inclusivo)
         * 3: el valor maximo que se encuentra entre los enteros (exclusivo)
         */
        //citiesIndexes es un arreglo de enteros generado aleatoreamente que contiene los INDICES de las ciudades 
        var citiesIndexes = RandomizationProvider.Current.GetUniqueInts(numberOfCities, 0, numberOfCities); 
        

        /**
         * Se tiene que modificar los Genes del chromosoma , en este caso se reemplazan 
         * con un numero (obtenido aleatoriamente) del arreglo de indices de la ciudad
         */
        for(int i=0; i< numberOfCities; i++)
        {
            ReplaceGene(i, new Gene(citiesIndexes[i])); 
        }
    
   
    }

    public double Distance { get; internal set; }

    /**
     * Sobreescribimos el metodo para generar genes nuevos, regresa un 
     * objeto de tipo gen el cual guarda un entero aleatorio entre 0 y el numero de ciudades 
     */
    public override Gene GenerateGene(int geneIndex)
    {
        return new Gene(RandomizationProvider.Current.GetInt(0, m_numberOfCities)); 
    }

    /**
     * Regresamos un objeto de tipo IChromosome que sera de tipo TSPChromosome
     * Notemos que cada vez que llamemos al constructor, cada chromosoma sera distinto 
     */
    public override IChromosome CreateNew()
    {
        return new TSPChromosome(m_numberOfCities);
    }

    public override IChromosome Clone()
    {
        //Usando el metodo Clone de la clase superior 
        var clone = base.Clone() as TSPChromosome;
        clone.Distance = Distance;

        return clone;
    }

}
