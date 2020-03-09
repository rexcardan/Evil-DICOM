---
uid: networking.md
title: Evil DICOM Networking
---

##Entities
In DICOM networking, the networking objects that communicate are called Entities, or Application Entities. An entity has a label called an AETitle, a port, and an IP address where it can be reached. To contruct this information in Evil DICOM, use:
```csharp
var myEntity = new Entity("AETitle","192.168.1.1",104);

//Create an entity on the localhost
var daemon = Entity.CreateLocal("Daemon", 104);
```
##DICOM Service Classes
DICOM services for sending, recieving and querying are divided into two main classes : DICOM Service User (DICOMSCU) and DICOM Service Provider (DICOMSCP). These objects can send and recieve specially encoded messages over a network to transmit information in a DICOM like format called DIMSE. 

###DICOMSCP
The DICOM SCP (provider) is the object that typically will hold information and will send it on request. You can query it typically, and you can ask to download DICOM objects from it. You can often also send DICOM objects to store to it. To construct a DICOM SCP, you use:
```csharp
var myEntity = new Entity("AETitle","192.168.1.1",104);
var scp = new DICOMSCP(myEntity);
//Asynchronously run - Don't block thread
scp.ListenForIncomingAssociations(keepListenerRunning:true);
```

###DICOMSCU
The DICOM SCU (user) is the client object which directs DICOM traffic. It calls the DICOMSCP and queries it, pulls from it, or pushes to it. It typically has a single focus while the provider is potentially doing multiple things simultaneously.
```csharp
var en1 = Entity.CreateLocal("EvilDICOM", 666);
var scu = new DICOMSCU(en1);
```

###Messaging 101
There are lots of message types you can do, but let's start with the basics. Typically you will query an SCP (daemon) and get all the information you need to actually pull files down, or move them somewhere (to another SCP for example). The next example shows how to do this with a class called the QueryBuilder. It starts with the highest level of querying (the patient) and works its way down to the individual images. The typical order of query descension is PATIENT >> STUDY >> SERIES >> IMAGE. You can get a list of all the images for a given patient like this:
```csharp
//The SCP we can Query (we'll just store this info...not create our own SCP)
var daemon = Entity.CreateLocal("Daemon", 104);

//The SCU that we will use in the QueryBuilder
var en1 = Entity.CreateLocal("EvilDICOM", 666);
var scu = new DICOMSCU(en1);

//Construct the QueryBuilder (us, them)
var qb = new QueryBuilder(scu, daemon);

//Get all studies
var studies = qb.GetStudyUids("PatientId");

//Filter studies if you want
//Next pass in to get all series from those studies
var series = qb.GetSeriesUids(studies);

//Filter series if you want
//Next get all images from those remaining series
var images = qb.GetImageUids(series);
```
The images are not the actual DICOM images, but information that contains the SOP instance UID of the image, series, and study. That's all you need to do something useful!

##C-Move
Now what? Let's take our image information and instruct the SCP to move it to our own SCP. Now typically, the foriegn SCP must already know where your SCP is. There is typically a "whitelist" of known servers. Let's assume our entity "EvilDICOMCatcher" at port 667 is listed. Then we can tell SCP to push via a C-MOVE operation like:
 ```csharp
ushort msg = 1;

foreach (var im in images)
{
    //This will only return after the image was successfully transferred or there was an error.
    var resp = scu.SendCMoveImage(daemon, im, "EvilDICOMCatcher", ref msg);
    Console.WriteLine("CMoveResp recieved {0} {1}", resp.MessageIDBeingResponsedTo, resp.Status);
}    
```

That is probably enough to make you dangerous. The networking API is not fully developed but contains many pieces you will need to perform DICOM and DIMSE tasks.