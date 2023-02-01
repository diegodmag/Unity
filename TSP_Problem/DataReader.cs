using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Globalization;
using Unity.VisualScripting;
using System.Text.RegularExpressions;
using System.Linq;

public class DataReader:MonoBehaviour 
{
    
    //private string[] data;
    //This is the double dimension array in  which we are going to store the coords 
    private ArrayList coords = new ArrayList();
    private float[][] totalCoordinates; 

    //These three ArrayList are used to clean anc covert teh data readed from the .csv 
    private ArrayList rawData = new ArrayList();
    private ArrayList cleanData = new ArrayList();
    private ArrayList cleanDoubleData = new ArrayList();
    public DataReader()
    {
       
    }

    private void Start()
    {
       //ShowContent(); 
       DataVerification();
       // Debug.Log(coordDatas.Count); 
       CoordsGeneration(3);
        //ShowContent(cleanDoubleData); 
    }

    private void ShowContent(ArrayList arrL)
    {
        foreach (double d in arrL)
        {
            Debug.Log(d);
        }


    }

    private void showSingleCoord(float[] coord)
    {
        StringBuilder sb = new StringBuilder();

        foreach(float c in coord)
        {
           sb.Append(c+"  ");
        }

        Debug.Log(sb.ToString());   
    }

    public void DataVerification()
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(@"D:\Plantilla\Unity_Training\Proyectos\TSP_Problem\Assets\Source\saturno_data.csv"))
            {
                string line;
                string cleanLine;
                // Read and display lines from the file until the end of
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    //Replace all that is not a number in the line with a white space 
                    cleanLine = Regex.Replace(line, @"^[-?\d+[\.?\d+]*]", " ");
                    //Split the chain from each white space, this give us an array which has three elements that are double numbers and
                    //the rest are white spaces 
                    string[] split = cleanLine.Split(" "); 
                    foreach (string s in split)
                    {
                        if(s!=" " && s!= "")
                        {
                            //We add to our ArrayList "rawData" those strings that are not a white space 
                            rawData.Add(s);
                        }
                    }              
                }
            }
        }
        catch (Exception e)
        {
            // Let the user know what went wrong.
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }

        //Cleaning the strings in "rawData"
        
        //An array that contains the characters that are going to be trim 
        char[] filter = new char[] { ' ', '"' };

        foreach (string data in rawData)
        {
            string cleanString = data.Trim(filter);
            if(cleanString != " " && cleanString != "")
            {
                cleanData.Add(cleanString);
            }
        }

        //We cast each string to double and then add the casted string to our ArrayList cleanDoubleData 
        foreach (string data in cleanData)
        {
            float number = float.Parse(data, System.Globalization.CultureInfo.InvariantCulture);
            cleanDoubleData.Add(number);
        }
    }

    public void CoordsGeneration(int dimension)
    {
        //We cast the ArrayList to a Array of doubles 
        float[] temp = cleanDoubleData.Cast<float>().ToArray();
        

        for(int i = 0; i< temp.Length; i += dimension)
        {
            //For each number in temp, we consider the first "dimension"-numbers as a single coordinate, so we create an array which will store the coords
            float[] coord = new float[dimension];
            for(int j = 0; j < coord.Length; j++, i++)
            {
                coord[j] = temp[i];
            }

            //showSingleCoord(coord);
            coords.Add(coord);
        }

        //totalCoordinates = new double[temp.Length / dimension, dimension];
        totalCoordinates = coords.Cast<float[]>().ToArray();

       
        

        foreach (float[] cord in totalCoordinates)
        {
            showSingleCoord(cord);
        }
       

    }

}
