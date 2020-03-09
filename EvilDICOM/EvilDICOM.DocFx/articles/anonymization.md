---
uid: anonymization.md
title: Evil DICOM Anonymization
---

##Anonymization Operations
Evil DICOM can quickly anonymize a DICOM file set with just a few lines of code. 

###Example Code:

```csharp
        string dir = @"E:\ZZZIMAGEREGJM";

        var toAnonymize = Directory.GetFiles(dir);

        var settings = AnonymizationSettings.Default;
        //Change mapping but keep connections
        settings.DoAnonymizeUIDs = true;
        settings.FirstName = "Homer";
        settings.LastName = "Simpson";
        settings.Id = "123456";

        //Gets a current list of UIDs so it can create new ones 
        var queue = EvilDICOM.Anonymization.AnonymizationQueue.BuildQueue(settings, toAnonymize);

        foreach (var file in toAnonymize)
        {
            Console.WriteLine("Anonymizing {0}", file);
            var dcm = DICOMObject.Read(file);
            queue.Anonymize(dcm);
            //Write back to initial location - though this can be a different place
            dcm.Write(file);
        }
```
