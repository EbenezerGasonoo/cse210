using System;

class Program
{
    static void Main(string[] args)
    {
        //Creating the first job instance
        Job job1 = new Job();

        //Set the member variables using the dot notation
        job1._company = "Microsoft";
        job1._jobTitle = "Software Engineer";
        job1._startYear = 2019;
        job1._endYear = 2022;

        //Creating the first job instance
        Job job2 = new Job ();

        // Setting the member variable using the dot notation
        job2._company = "Apple";
        job2._jobTitle = "Product Designer";
        job2._startYear = 2022;
        job2._endYear = 2024;



        Resume myResume = new Resume();
        
        //Setting the member variables
        myResume._name = "Ebenezer Gasonoo";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();

    }
}
