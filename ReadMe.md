# Author: Pan Shyani

## Project
The task is to merge data from two JSON files containing data reported by IoT devices which capture 
temperature and humidity data.  Each device uses a slightly different JSON schema. 

Develop a business layer solution using C# & .NET Core that will process data from each file, summarize the data 
in to a single standardized list, and add some calculated values: 

###### Average temperature of each device 
###### Average humidity of each device 
###### Total record count for both temperature and humidity   
###### First and Last time each device reported sensor values 
###### Save the merged list to a new JSON file. 
####
The structure of the merged list should be defined as:

public int CompanyId { get; set; }  // Foo1: PartnerId, Foo2: CompanyId    
public string CompanyName { get; set; }  // Foo1: PartnerName, Foo2: Company    
public int? DeviceId { get; set; }  // Foo1: Id, Foo2: DeviceID    
public string DeviceName { get; set; }  // Foo1: Model, Foo2: Name    
public DateTime? FirstReadingDtm { get; set; }  // Foo1: Trackers.Sensors.Crumbs, Foo2: Devices.SensorData    
public DateTime? LastReadingDtm { get; set; }    
public int? TemperatureCount { get; set; }    
public double? AverageTemperature { get; set; }    
public int? HumidityCount { get; set; }    
public double? AverageHumdity { get; set; }