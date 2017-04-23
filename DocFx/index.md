<center>![Logo](images/evilDicomLogo.png "Evil DICOM Logo")</center>

Welcome to the Evil DICOM C# library homepage. Don't let the name scare you. Evil DICOM is one of the easiest DICOM libraries in the world to use and implement into your .NET applications. You can forget about the byte order, endianiness, tag order, and all of those little details that can make DICOM seem..well evil. With Evil DICOM you will be able to access DICOM objects and elements in an intuitive object oriented way. This site has tutorials, API documentation, and even little projects that you can download to get you started.


##Basic Operations

###Reading and Writing
```csharp
var dcm = DICOMObject.Read(@"MyDICOMFile.dcm");

//You can also read from bytes (eg. a stream)
var dBytes = File.ReadAllBytes(@"MyDICOMFile.dcm");
var dcm = DICOMObject.Read(dBytes);

//***COOL CODE GOES HERE***

//Writing is equally easy
dcm.Write("MyHackedDICOMFile.dcm");
```
The dcm variable is now a DICOM object. It is in fact the outermost DICOM object from the file string that was input into the Read() method. The DICOMObject class is the main class of Evil DICOM.  Of course the DICOM object is really just a container for all of the DICOM elements that you will want to get and luckily for you, it is easy to do. Now that we did the useless task of reading a file and writing it back unchanged, let's look at some code we can insert in between.


###Working With Elements
####The IDICOMElement Interface
Each DICOM element contains a little piece of data. The element (VR) type provides enough information for determining what type of data the element contains. All Evil DICOM elements implement the IDICOMElement interface. This interface looks like:
```csharp
public interface IDICOMElement
{
   Tag Tag { get; set; }
   DatType { get; }
   dynamic DData { get; set; }
   dynamic DData_ { get; set; }
}
```
The tag holds all of the identifying information for the element. The IDICOMElement interface also exposes the element's data, but it is not strongly typed. Because each element can hold different types of data. You can cast the data to a certain type if you know what it is supposed to be, otherwise I will show you some wicked ways of getting the strongly typed data you want.

###Generic Data Casting Elements
If you don't know the underlying VR type, but you know the type of data that it contains, you can always cast to the more generic AbstactElement where T is the type of data. As you can see below, there is an advantage to knowing the actual VR element type because it provides more intuitive methods of data access (eg. the "FirstName" and "LastName" properties of the PersonName class). You lose those on a generic cast.
```csharp
//Generic casting
var genericName = dcm.FindFirst(TagHelper.PATIENT_NAME) as AbstractElement<string;
var genValue = genericName.Data; // returns Flinstone^Fred
```
###Specific Casting Elements
Each VR (value representation) has its own class. If you know the underlying VR, you can cast to a VR class to get some handy strong typed variables.
```csharp
//The patient's name IDICOMElement
var pName = dcm.FindFirst(TagHelper.PATIENT_NAME);

//The patient's name strong typed for better data access
var strongName = dcm.FindFirst(TagHelper.PATIENT_NAME) as PersonName;
var firstName = strongName.FirstName;
var lastName = strongName.LastName;

//You can manipulate this way also
strongName.FirstName = "Fred";
strongName.LastName = "Flinstone";
```
###Finding DICOM Elements
The DICOMObject class (dcm variable below) provides two properties that give access to all contained elements. The Elements property gives access to the direct children of the DICOM object and the AllElements property returns all descendant elements including children of elements.
```csharp
var directChildren = dcm.Elements;
var allDescendants = dcm.AllElements
```
Of course you might need to be more specific about the elements you want returned. There are several methods to help you get what you need.
```csharp
//Finds the first instance of the Group Length element (0002,0000)
var firstInstance = dcm.FindFirst("00020000");

//Finds all instances of the Group Length element (0002,0000)
var allInstances = dcm.FindAll("00020000");

//Finds all Code Value (0008,0100) elements that are children of Procedure Code Sequence Elements (0008,1032)
var specificTree = dcm.FindAll(new Tag[]{ TagHelper.PROCEDURE_CODE_SEQUENCE, TagHelper.CODE_VALUE });

//Finds all elements that are of VR type PersonName
var allPersonsNameElements = dcm.FindAll(Enums.VR.PersonName);
```

###Replacing and Removing Elements
Sometimes, you may just want to remove or replace an element. That is easy as well:
```csharp
var refName = new PersonName
	{
		FirstName = "Fred",
		LastName = "Flinstone",
		Tag = TagHelper.REFERRING_PHYSICIAN_NAME
	};

//Whatever the referring physicians real name was, it is now Fred Flinstone
dcm.Replace(refName);

//Even if it doesn't exist yet, add it
dcm.ReplaceOrAdd(refName);

//Remove elements by tag
dcm.Remove("00020000");
dcm.Remove(TagHelper.SEGMENT_NUMBER);
```
###Working with DICOM Data
DICOM data containers need to be very flexible. They must be able to have single values, multiple values, and be null. Evil DICOM accommodates these needs with the DICOMData class. This class wraps a list of data of type T and provides easy access to the single data and multiple data. The following demonstrates how to grab a single (first) value and a multiple value property. It might seem weird to use an underscore at first, but when you are digging deep into DICOM, it helps to keep thinks terse but readable.
```csharp
//Patient's age is a single string value
var age = dcm.FindFirst(TagHelper.PATIENT_AGE) as AgeString;
var actualAge = age.Data; // data of type T (in this case string)

//Patient position holds double values
var position = dcm.FindFirst(TagHelper.PATIENT_POSITION) as AbstractElement<double>;

//Patient position contains an array of double values {X,Y,Z}
//Notice the underscore ( _ ) AFTER Data
//The underscore grabs the the multiplicity data (if there is more than one value)
var xyz = position.Data_; //Data as List<T> (in this case List<double>)
var x = xyz[0];
var y = xyz[1];
var z = xyz[2];
```
###See Also
*	@structure.md
*	@selection.md
*	@helpers.md
