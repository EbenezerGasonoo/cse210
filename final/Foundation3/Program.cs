using System;

// Address class for encapsulating address details
public class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {state}\n{country}";
    }

    // Getters and setters
    public string Street
    {
        get { return street; }
        set { street = value; }
    }

    public string City
    {
        get { return city; }
        set { city = value; }
    }

    public string State
    {
        get { return state; }
        set { state = value; }
    }

    public string Country
    {
        get { return country; }
        set { country = value; }
    }
}

// Base class Event with common attributes and methods
public abstract class Event
{
    private string title;
    private string description;
    private DateTime date;
    private TimeSpan time;
    private Address address;

    public Event(string title, string description, DateTime date, TimeSpan time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date.ToShortDateString()}\nTime: {time}\nAddress:\n{address.GetFullAddress()}";
    }

    public abstract string GetFullDetails();
    public abstract string GetShortDescription();

    // Getters and setters
    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    public DateTime Date
    {
        get { return date; }
        set { date = value; }
    }

    public TimeSpan Time
    {
        get { return time; }
        set { time = value; }
    }

    public Address EventAddress
    {
        get { return address; }
        set { address = value; }
    }
}

// Derived class Lecture with additional attributes and methods
public class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, DateTime date, TimeSpan time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Lecture\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }

    // Getters and setters
    public string Speaker
    {
        get { return speaker; }
        set { speaker = value; }
    }

    public int Capacity
    {
        get { return capacity; }
        set { capacity = value; }
    }
}

// Derived class Reception with additional attributes and methods
public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime date, TimeSpan time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Reception\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }

    // Getters and setters
    public string RsvpEmail
    {
        get { return rsvpEmail; }
        set { rsvpEmail = value; }
    }
}

// Derived class OutdoorGathering with additional attributes and methods
public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime date, TimeSpan time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }

    public override string GetShortDescription()
    {
        return $"Type: Outdoor Gathering\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }

    // Getters and setters
    public string WeatherForecast
    {
        get { return weatherForecast; }
        set { weatherForecast = value; }
    }
}

// Program class to demonstrate functionality
public class Program
{
    public static void Main()
    {
        // Create addresses
        Address address1 = new Address("789 Elm St", "Boston", "MA", "USA");
        Address address2 = new Address("101 Maple St", "London", "ENG", "UK");
        Address address3 = new Address("202 Oak St", "Sydney", "NSW", "Australia");

        // Create events
        Lecture lecture = new Lecture("Tech Talk", "A discussion on the latest in tech.", new DateTime(2024, 8, 15), new TimeSpan(14, 0, 0), address1, "Dr. Jane Doe", 100);
        Reception reception = new Reception("Art Exhibit Opening", "Join us for the opening of the new art exhibit.", new DateTime(2024, 9, 20), new TimeSpan(18, 0, 0), address2, "rsvp@artexhibit.com");
        OutdoorGathering gathering = new OutdoorGathering("Summer Picnic", "A fun-filled picnic in the park.", new DateTime(2024, 7, 10), new TimeSpan(11, 0, 0), address3, "Sunny with a chance of clouds");

        // Display event details
        DisplayEventDetails(lecture);
        DisplayEventDetails(reception);
        DisplayEventDetails(gathering);
    }

    public static void DisplayEventDetails(Event eventItem)
    {
        Console.WriteLine("Standard Details:");
        Console.WriteLine(eventItem.GetStandardDetails());
        Console.WriteLine("\nFull Details:");
        Console.WriteLine(eventItem.GetFullDetails());
        Console.WriteLine("\nShort Description:");
        Console.WriteLine(eventItem.GetShortDescription());
        Console.WriteLine();
    }
}
