---
uid: helpers.md
title: Evil DICOM Helpers
---

##Helper Classes
There are a lot of details about DICOM that the libary doesn't expect you to have memorized. There are several helper classes to help with common problems. I suggest you explore the helper classes in the helper folder of the library. Of all helpers, you will probably use the TagHelper the most.

###The Tag Helper
Now I don't expect you to have memorized all DICOM tags. There is a class called the TagHelper that is designed to get to tag Ids quickly. The Tag Helper actually returns a Tag object with some useful properties:
```csharp
 //Returns a tag that has other useful properties
 var windowTag= TagHelper.WINDOW_CENTER;

 //Group string
 var group = windowTag.Group;

 //Element string
 var elem = windowTag.Element;

 //Whole Tag ID
 var fullID = windowTag.CompleteID;

 //Using DICOMObject.Find()
 var windowCenter = dcm.FindFirst(TagHelper.WINDOW_CENTER);
```

###Using the UID Helper
While I can't go through all the helper classes, I wanted to show you one more. Since UIDs are everywhere in DICOM files, you might want to know how to generate some. A nifty little helper class is just for that:
```csharp
var uid = dcm.FindFirst(TagHelper.SOPINSTANCE_UID);

//Override
uid.DData = UIDHelper.GenerateUID();
//Or more specific
uid.DData = UIDHelper.GenerateUID(ISO3166Helper.UNITED_STATES, vendorID: "456", suffix:"123");
```