using System.Threading;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Framework.Threading;
using UnityEngine;


public class GAController : MonoBehaviour
{
    public Object CityPrefab;

    private LineRenderer m_lr; 
    private GeneticAlgorithm m_ga;
    private Thread m_gaThread;



    public int m_numberOfCities = 200; 

    // Start is called before the first frame update
    void Start()
    {
        var fitness = new TSPFitness(m_numberOfCities);
        var chromosome = new TSPChromosome(m_numberOfCities);

        //Operadores iniciales ya implementados 
        var crossover = new OrderedCrossover();
        var mutation = new ReverseSequenceMutation();
        var selection = new RouletteWheelSelection();
        var population = new Population(50, 100, chromosome);

        m_ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation);
        m_ga.Termination = new TimeEvolvingTermination(System.TimeSpan.FromHours(1));


        m_ga.TaskExecutor = new ParallelTaskExecutor
        {
            MinThreads = 100,
            MaxThreads = 200
        };


        m_ga.GenerationRan += delegate
        {
            var distance = ((TSPChromosome)m_ga.BestChromosome).Distance;
            Debug.Log($"Generation: {m_ga.GenerationsNumber} - Distance: ${distance}");

        };

        DrawCities();



        m_gaThread = new Thread(() => m_ga.Start());
        m_gaThread.Start();

    }

    private void Update()
    {
        DrawRoute(); 
    }



    private void OnDestroy()
    {
        // When the script is destroyed we stop the genetic algorithm and abort its thread too.
        m_ga.Stop();
        m_gaThread.Abort();
    }

    private void Awake()
    {
        m_lr = GetComponent<LineRenderer>();
        m_lr.positionCount = m_numberOfCities + 1; 
    }


    void DrawCities()
    {
        Debug.Log("Esta entrando para dibujar las ciudades"); 
        var cities = ((TSPFitness)m_ga.Fitness).Cities;

        for(int i=0; i < m_numberOfCities; i++)
        {
            var city = cities[i]; 
            var go = Instantiate(CityPrefab, city.Position,Quaternion.identity) as GameObject;
            go.name = "City " + i;
            go.GetComponent<CityController>().Data = city;
        }

    }

    void DrawRoute()
    {
        var c = m_ga.Population.CurrentGeneration.BestChromosome as TSPChromosome;

        if(c!= null)
        {
            var genes = c.GetGenes();
            var cities = ((TSPFitness)m_ga.Fitness).Cities; 

            for(int i=0; i < genes.Length; i++)
            {
                var city = cities[(int)genes[i].Value];
                m_lr.SetPosition(i, city.Position); 

            }

            var firstCity = cities[(int)genes[0].Value];
            m_lr.SetPosition(m_numberOfCities, firstCity.Position);

           
        }
    }

}
