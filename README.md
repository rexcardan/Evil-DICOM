Evil DICOM
=============
![Logo](https://github.com/rexcardan/Evil-DICOM/blob/gh-pages/images/evilDicomLogo.png)

A simple to use C# library for reading and manipulating DICOM files. 
New documentation added to github via Github pages. See links below:

The following links will help you get started:

```cs
var dcm = DICOMObject.Read(@&quot;MyDICOMFile.dcm&quot;);
//You can also read from bytes (eg. a stream)
var dBytes = File.ReadAllBytes(@&quot;MyDICOMFile.dcm&quot;);
var dcm = DICOMObject.Read(dBytes);
//***COOL CODE GOES HERE***
//Writing is equally easy
dcm.SaveAs("MyHackedDICOMFile.dcm");
```

Read more at the project website at 
http://rexcardan.github.io/Evil-DICOM/

| Content | Link |
------------- | -------------
|	Introductory Video | [Youtube](https://www.youtube.com/watch?v=rmYpxxqQ90s) |
|	Examples | [Example Operations](http://rexcardan.github.io/Evil-DICOM/articles/operations.html) |
|	Online API | [API Documentation](http://rexcardan.github.io/Evil-DICOM/api/index.html) |

***

Supported by JetBrains' ReSharper

![JetBrain's Logo](https://h9jd9q.dm2304.livefilestore.com/y4m0Q1iIXt3uj4zsf5dnlHI4HkdM4wH7JP2G7YCXNLBb6t59byWqX17LvJbJMs1E0PRvabL8ac_aMalS2yiX3pWvDBh-ue-NgmjliEMrPCBIEZ_0HEuMLhNXWKD3TFnhuJ6vglTOksYSo-GjFTnmNmoyNh9m4xxi8myABrlmN57XoMutalXWtRV4hdaay3sJZFXfMO5sVsCsvjXb-fYWS-fxw?width=128&height=138&cropmode=none)
