using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Randomizations;
using UnityEngine;

public class TSPFitness : IFitness
{
    private Rect m_area;
    
    //La lista de ciudades (representacion grafica)
    

    public TSPFitness(int numberOfCities)
    {
        //Se inicializa la lista de Ciudades, la representacion grafica mas no el chromosoma 
        Cities = new List<TSPCity>(numberOfCities);
        var size = Camera.main.orthographicSize - 1; 
        //Se inicializa el rectangulo 
        m_area = new Rect(-size, -size, size*2, size*2);

        //Para cada ciudad se asigna una posicion random dentro del rectangulo 
        for (int i=0; i < numberOfCities; i++) {

            var city = new TSPCity { Position = GetCityRandomPosition() };
            Cities.Add(city);
        
        }

    }
    public IList<TSPCity> Cities { get; private set; }

    public double Evaluate(IChromosome chromosome)
    {
        //Se almacenan los genes del chromosoma , que es un arreglo de objetos de tipo Gen 
        var genes = chromosome.GetGenes();
        //La distancia a calcular 
        var distanceSum = 0.0; 
        //Obtenemos el indice de la ultima ciudad 
        var lastCityIndex = Convert.ToInt32(genes[0].Value,CultureInfo.InvariantCulture);
        //Nuestra lista de índices 
        var citiesIndexes = new List<int>();
        //Agregamos el índice de la última ciudad a la lista de ciudades del chromosoma evaluado 
        citiesIndexes.Add(lastCityIndex);

        /*
         * Para cada gen, obtenemos su valor y lo almacenamos en currentCityIndex, es decir la ciudad actualmente iterada
         * encontramos la distancia entre la ciudad actualmente iterada y la última ciudad , 
         */
        foreach (var g in genes)
        {
            var currentCityIndex = Convert.ToInt32(g.Value, CultureInfo.InvariantCulture);
            distanceSum += CalcDistanceTwoCities(Cities[currentCityIndex], Cities[lastCityIndex]);
            //Ahora la ciudad que se evaluaba es la última a alcanzar 
            lastCityIndex = currentCityIndex;

            citiesIndexes.Add(lastCityIndex); 
        }

        //Obtenemos la distancia entre la primera y ultima ciudad 
        distanceSum += CalcDistanceTwoCities(Cities[citiesIndexes.Last()], Cities[citiesIndexes.First()]);
        
        
        var fitness = 1.0 - (distanceSum / (Cities.Count * 1000.0));
        
        //Asignamos la suma de las distancia al chromosoma evaluado 
        ((TSPChromosome)chromosome).Distance = distanceSum;
        
        var diff = Cities.Count - citiesIndexes.Distinct().Count();

        if(diff > 0)
        {
            fitness /= diff;
        }
        if(fitness < 0)
        {
            fitness = 0;
        }
        
        
        
        return fitness; 


    }



    //Se obtiene una posicion (Un Vector2D) aleatoria dentro del rectangulo 
    public Vector2 GetCityRandomPosition()
    {
        return new Vector2(
            RandomizationProvider.Current.GetFloat(m_area.xMin, m_area.xMax + 1),
            RandomizationProvider.Current.GetFloat(m_area.yMin, m_area.yMax + 1)); 

    }

    //Calcula la distancia entre dos ciudades 
    private static double CalcDistanceTwoCities(TSPCity one, TSPCity two)
    {
        return Vector2.Distance(one.Position, two.Position);
    }

}
