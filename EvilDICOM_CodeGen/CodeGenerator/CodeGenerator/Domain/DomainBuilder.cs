using CodeGenerator.Extensions;
using EvilDICOM.Core;
using EvilDICOM.Core.Dictionaries;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Domain
{
    public class DomainBuilder
    {
        public static List<CompilationUnitSyntax> BuildFromFile(string filePath, string className)
        {
            //This object plus all descendant sequence objects
            List<CompilationUnitSyntax> allNodes = new List<CompilationUnitSyntax>();

            var dcm = DICOMObject.Read(filePath);

            var (props, descNodes) = GetProperties(dcm);
            allNodes.AddRange(descNodes);

            var syntaxFactory = SyntaxFactory.CompilationUnit();
            syntaxFactory = syntaxFactory.AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("EvilDICOM.Core.Wrapping")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("EvilDICOM.Core")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")));

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("EvilDICOM.IOD")).NormalizeWhitespace();
            var node = SyntaxFactory.ClassDeclaration(className);
            node = node.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            node = node.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("DICOMObjectWrapper")));
            var constructor = SyntaxFactory.ConstructorDeclaration(className).WithBody(
                SyntaxFactory.Block(
                SyntaxFactory.ParseStatement("DCM = new DICOMObject();")));
            constructor = constructor.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            props.Insert(0, constructor);
            node = node.AddMembers(props.ToArray());
            @namespace = @namespace.AddMembers(node);
            syntaxFactory = syntaxFactory.AddMembers(@namespace);

            allNodes.Add(syntaxFactory);
            return allNodes;
        }

        public static (List<MemberDeclarationSyntax>, List<CompilationUnitSyntax>) GetProperties(DICOMObject dcm)
        {
            List<CompilationUnitSyntax> allNodes = new List<CompilationUnitSyntax>();
            List<MemberDeclarationSyntax> props = new List<MemberDeclarationSyntax>();
            foreach (var el in dcm.Elements.Where(e => e.Tag.Group != "0002"))
            {
                //Create property
                var prop = GenerateProperty(el);
                if (prop != null)
                    props.Add(prop);
                //If sequence - Create class
                if (el is Sequence)
                {
                    allNodes.AddRange(BuildFromSequence(el as Sequence));
                }
            }
            return (props, allNodes);
        }

        public static List<CompilationUnitSyntax> BuildFromSequence(Sequence seq)
        {
            var entry = DICOMDictionary.Instance.Entries.FirstOrDefault(e => e.Id == seq.Tag.CompleteID);
            var syntaxFactory = SyntaxFactory.CompilationUnit();
            syntaxFactory = syntaxFactory.AddUsings(
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("EvilDICOM.Core.Wrapping")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("EvilDICOM.Core")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
                SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")));

            var (props, descNodes) = GetProperties(seq.Items.FirstOrDefault());
            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("EvilDICOM.IOD")).NormalizeWhitespace();
            var node = SyntaxFactory.ClassDeclaration(entry.Keyword.Replace("Sequence", ""));
            node = node.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            node = node.AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName("DICOMObjectWrapper")));
            var constructor = SyntaxFactory.ConstructorDeclaration(entry.Keyword.Replace("Sequence", "")).WithBody(
                SyntaxFactory.Block(
                SyntaxFactory.ParseStatement("DCM = new DICOMObject();")));
            constructor = constructor.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
            props.Insert(0, constructor);
            node = node.AddMembers(props.ToArray());
            @namespace = @namespace.AddMembers(node);
            syntaxFactory = syntaxFactory.AddMembers(@namespace);
            descNodes.Add(syntaxFactory);
            return descNodes;
        }

        public static MemberDeclarationSyntax GenerateProperty(IDICOMElement el)
        {
            var entry = DICOMDictionary.Instance.Entries.FirstOrDefault(e => e.Id == el.Tag.CompleteID);
            if (entry == null) { return null; }

            var type = VRDictionary.GetDataTypeFromVR(VRDictionary.GetVRFromAbbreviation(entry.VR));
            var propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(type.ToString()), entry.Keyword);
            if (type.IsGenericType)
            {
                propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName(type.ToGenericTypeString()), entry.Keyword);
            }
            var getTemplate = $"var el = DCM.GetSelector().{entry.Keyword}; return el != null ? el.Data : default({type.ToGenericTypeString()});";
            var setTemplate = $"DCM.GetSelector().{entry.Keyword} = DICOMForge.{entry.Keyword}(value);";
            if (el is Sequence)
            {
                getTemplate = $"return DCM.GetSelector().{entry.Keyword}?.Items.Select(i => new {entry.Keyword.Replace("Sequence", "")}() {{ DCM = i }}).ToList();";
                setTemplate = $"DCM.ReplaceOrAdd(DICOMForge.{entry.Keyword}(value.Select(v=>v.DCM).ToArray()));";
                propertyDeclaration = SyntaxFactory.PropertyDeclaration(SyntaxFactory.ParseTypeName($"List<{entry.Keyword.Replace("Sequence", "")}>"), entry.Keyword.Replace("Sequence", "s"));
            }

            var getStatement = SyntaxFactory.ParseStatement(getTemplate);
            var setStatement = SyntaxFactory.ParseStatement(setTemplate);

            propertyDeclaration = propertyDeclaration
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddAccessorListAccessors(
        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithBody(SyntaxFactory.Block(getStatement)),
        SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithBody(SyntaxFactory.Block(setStatement)));

            return propertyDeclaration;
        }
    }
}
