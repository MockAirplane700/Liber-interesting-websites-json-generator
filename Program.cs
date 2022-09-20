// Create a json file generator to add data to mongo campus 
// we require a way to add the data to a list 
// transcribe the data into a json file
// display a preview of the data 

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


internal class Program
{
    private static List<JObject> jObjects = new List<JObject>();
    private static bool check = true;

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        //getWebsiteDetails();
        // displayJsonPreview();
        getRequiredFeilds();
        generateJson(jObjects);

    }//end private static void main

    // generate json file 
    private static void generateJson(List<JObject> jsonObjects)
    {
        JObject website = new JObject(
            new JProperty("name", "somewebsite"),
            new JProperty("datePublished", "30 May 2020"),
            new JProperty("description", "lorem ipsum"),
            new JProperty("networkImage", "https://i.pinimg.com/564x/d0/e3/87/d0e3877bcf25be88f51fb32e5541d0e1.jpg"),
            new JProperty("websiteUrl", "https://www.google.ca"),
            new JProperty("source", "https://www.tiktok.com")
            );

        //File.WriteAllText(@"C:\Users\sizib\source\repos\Liber interesting websites json generator\data.json", website.ToString());

        // write JSON directly to a file
        using (StreamWriter file = File.CreateText(@"C:\Users\sizib\source\repos\Liber interesting websites json generator\data.json"))
        using (JsonTextWriter writer = new JsonTextWriter(file))
        {
            for (int k = 0; k < jsonObjects.Count; k++)
            {
                jsonObjects[k].WriteTo(writer);
            }
            
        }

    }//end generate json

    // transcribe json data to file

    // get data from user
    private static void getWebsiteDetails()
    {
        //todo: add a check to make sure input is not empty and check relevance of input
        //todo: allow for mistakes to be fixed

        // ask for user input 
        Console.WriteLine(" Please enter the needed user data");
        Console.WriteLine("Enter name of the website");
        string websiteName = Console.ReadLine();
        // date published
        Console.WriteLine("Enter the date the website was published");
        string datePublished = Console.ReadLine();
        // description 
        Console.WriteLine("Enter the description of the website");
        string websiteDescription = Console.ReadLine();
        // network image
        Console.WriteLine("Enter the network image url string");
        string networkImage = Console.ReadLine();
        // website url
        Console.WriteLine("Enter the website url");
        string websiteUrl = Console.ReadLine();
        // source
        Console.WriteLine("Enter the source of the website");

        Console.WriteLine("Website name: " + websiteName + "\nDate published: " + datePublished);
    }//end get user data

    // get required details for the json entries from the user 
    private static void getRequiredFeilds()
    {
 
        // get the number of entries so that we can use them to generate the json object
        Console.WriteLine("Please enter the names of these entry values seperated by the comma value for example: \nid,name,description,etc..\nPRESS ENTER ONCE COMPLETE.");
        string entryNames = Console.ReadLine();
        Console.WriteLine("Thank you! :)\n\n\n Press ESC key to end entry loop, press ENTER to move onto the next entry.");

        // call input json feilds until the user presses space bar or some other key
        
        while (check)
        {
            inputJsonFields(0, entryNames);
         
        }//end while loop
                
       
        // when user breaks out of the loop generate json file


    }//end get required fields


    // use obtained details to ask for json object entries until entries are complete

    private static void inputJsonFields( int entryNumber,string entryNames)
    {
     
        // seperate the entry names into a list of strings by the comma value.
        string[] substrings = entryNames.Split(",");
        JObject jsonObject = new JObject();

        foreach (var sub in substrings)
        {
            // Ask for the entry and place the entry in an object at the end 
            Console.Write(sub + ": ");
            string value = Console.ReadLine();
            // add the required values to json object
            jsonObject.Add(new JProperty(sub, value));
            if(Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                check = false;
                break;
            }
                  

        }//end for loop
        //TODO: SAVE THE JSON OBJECT TO A LIST OF JSON OBJECTS
        jObjects.Add(jsonObject);
       Console.WriteLine(jsonObject.ToString());


    }//end input json fields 


    // display a preview of the data
    private static void displayJsonPreview()
    {
        JObject o1 = JObject.Parse(File.ReadAllText(@"C:\Users\sizib\source\repos\Liber interesting websites json generator\data.json"));

        // read JSON directly from a file
        using (StreamReader file = File.OpenText(@"C:\Users\sizib\source\repos\Liber interesting websites json generator\data.json"))
        using (Newtonsoft.Json.JsonTextReader reader = new JsonTextReader(file))
        {
            JObject o2 = (JObject)JToken.ReadFrom(reader);
            Console.WriteLine("The name of the website: " +  o2.GetValue("name"));
            Console.WriteLine("The date when the website was published: " + o2.GetValue("datePublished"));
            Console.WriteLine("Where to find the website image: " + o2.GetValue("networkImage"));
            Console.WriteLine("Where to find the website itself: " + o2.GetValue("websiteUrl"));
            Console.WriteLine("From where we learned about the website, the source: " + o2.GetValue("source"));

        }

    }
}//end internal class

public class Website
{
    public string id;
    public string name;
    public string dataPublished;
    public string description;
    public string networkImage;
    public string websiteUrl;
    public string source;

}//end website