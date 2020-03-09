
# Examples

## Drawing Composite Images
You can draw composite images (CT, dose, structures)
using the following technique

```cs
var dd = DICOM2DoseMatrix.ParseDICOM("testdata/proneDose.dcm");
var rtss = DICOM2StructureSet.ParseDICOM("testdata/proneStruct.dcm");
var body = rtss.Structures.FirstOrDefault(s => s.Key == "BODY").Value;
var gtv = rtss.Structures.FirstOrDefault(s => s.Value?.DICOMType == "GTV").Value;
for (int z = 0; z < dd.DimensionZ; z++)
{
    var slice = dd.DrawContourOnSlice(body, new StructureLook(), z);
    slice.DrawAllContours(gtv.GetContoursOnSliceZ(dd, z), StructureLooks.GTV);
    slice.Show();
}
```

## Calculating DVH 
You need a structure set and dose matrix to be able to calculate the DVH. You can perform the task
like:

```cs
var dd = DICOM2DoseMatrix.ParseDICOM("testdata/proneDose.dcm");
var rtss = DICOM2StructureSet.ParseDICOM("testdata/proneStruct.dcm");
var gtv = rtss.Structures.FirstOrDefault(s => s.Value?.DICOMType == "GTV").Value;

var dvh = DVHCalculator.CalculateDVH(gtv, dd);
var data = dvh.GetDVHData();

var volume = gtv.CalculateVolumeCC();
```
