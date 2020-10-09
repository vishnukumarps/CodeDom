using System;
using System.Reflection;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Generic;


namespace SampleCodeDom
{
    class TestApp
    {
        CodeCompileUnit targetUnit;
        CodeTypeDeclaration targetClass;
        string controllerName="";
        string outputFileName = "";

        public TestApp(string className)
        {
            
            targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("Vesseels");
            samples.Imports.Add(new CodeNamespaceImport("System"));
            samples.Imports.Add(new CodeNamespaceImport("Microsoft.AspNetCore.Mvc"));
            
            targetClass = new CodeTypeDeclaration(""+className+"Controller"+":Controller");
            targetClass.IsClass = true;
            targetClass.TypeAttributes =
                TypeAttributes.Public ;

           
            targetClass.CustomAttributes.Add(new CodeAttributeDeclaration("Route", new CodeAttributeArgument(new CodePrimitiveExpression("api/[controller]"))));
            targetClass.CustomAttributes.Add(new CodeAttributeDeclaration("ApiController"));
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);
        }

        public void AddFields(Dictionary<string, string> feildNames)
        {


            foreach (var item in feildNames)
            {

                // Declare the feildName.
                CodeMemberField field = new CodeMemberField();
                field.Attributes = MemberAttributes.Private;
                field.Name = item.Key;

                if (item.Value == "String")
                {
                    field.Type = new CodeTypeReference(typeof(System.String));
                }
                if (item.Value == "Double")
                {
                    field.Type = new CodeTypeReference(typeof(System.Double));
                }
                field.Comments.Add(new CodeCommentStatement(
                    "The " + item.Key + " of the object."));
                targetClass.Members.Add(field);
            }


        }

        public void AddProperties(Dictionary<string, string> propertyNames)
        {
          
            foreach (var item in propertyNames)
            {
                if (item.Value == "String")
                {

                    var field = new CodeMemberField
                    {
                        Attributes = MemberAttributes.Public | MemberAttributes.Final,
                        Name = item.Key,
                        Type = new CodeTypeReference(typeof(System.String)),
                    };
                    field.Name += " { get; set; }";
                    targetClass.Members.Add(field);
                }

                if (item.Value == "Double")
                {

                    var field = new CodeMemberField
                    {
                        Attributes = MemberAttributes.Public | MemberAttributes.Final,
                        Name = item.Key,
                        Type = new CodeTypeReference(typeof(System.Double)),
                    };
                    field.Name += " { get; set; }";
                    targetClass.Members.Add(field);
                }

            }


        }

        public  void AddNewProperty(Dictionary<string,string> propertyList)
        {
            foreach (var item in propertyList)
            {
                CodeSnippetTypeMember snippet = new CodeSnippetTypeMember();
                snippet.Comments.Add(new CodeCommentStatement("this is "+item.Value+" property", true));
                snippet.Text = "public "+item.Value+" "+item.Key+" { get; set; }";
                targetClass.Members.Add(snippet);

            }
            


        }

        public void AddProperties()
        {
            // Declare the read-only Width property.
            CodeMemberProperty widthProperty = new CodeMemberProperty();
            widthProperty.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;
            widthProperty.Name = "Width";
            widthProperty.HasGet = true;
            widthProperty.Type = new CodeTypeReference(typeof(System.Double));
            widthProperty.Comments.Add(new CodeCommentStatement(
                "The Width property for the object."));
            widthProperty.GetStatements.Add(new CodeMethodReturnStatement(
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "widthValue")));
            targetClass.Members.Add(widthProperty);

          
        }
        public void AddMethod(string methodName)
        {
            // Declaring a ToString method
            CodeMemberMethod mymethod = new CodeMemberMethod();
            mymethod.Name = methodName;
            mymethod.CustomAttributes.Add(new CodeAttributeDeclaration("HttpGet"));
            mymethod.CustomAttributes.Add(new CodeAttributeDeclaration("Route", 
                new CodeAttributeArgument(new CodePrimitiveExpression(methodName))));

            CodeTypeReference ctr = new CodeTypeReference("String");
            //Assign the return type to the method.
            mymethod.ReturnType = ctr;
            mymethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            CodeSnippetExpression snippet1 = new CodeSnippetExpression("Random rn = new Random()");
            CodeSnippetExpression snippet2 = new CodeSnippetExpression("var x=rn.Next(1000,2000)");
            CodeSnippetExpression snippet3 = new CodeSnippetExpression("return x.ToString()");
            CodeExpressionStatement stmt1 = new CodeExpressionStatement(snippet1);
            CodeExpressionStatement stmt2 = new CodeExpressionStatement(snippet2);
            CodeExpressionStatement stmt3 = new CodeExpressionStatement(snippet3);
          
            mymethod.Statements.Add(stmt1);
            mymethod.Statements.Add(stmt2);
            mymethod.Statements.Add(stmt3);
            mymethod.Attributes = MemberAttributes.Public;
            

            targetClass.Members.Add(mymethod);
        }

        public void AddCustomConstructor(string name)
        {
            // Declaring a ToString method
            CodeMemberMethod mymethod = new CodeMemberMethod();
            mymethod.Name = name;
            mymethod.CustomAttributes.Add(new CodeAttributeDeclaration("HttpGet"));

            CodeTypeReference ctr = new CodeTypeReference("String");
            //Assign the return type to the method.
           
            mymethod.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            CodeSnippetExpression snippet1 = new CodeSnippetExpression("Random rn = new Random()");
            CodeSnippetExpression snippet2 = new CodeSnippetExpression("rn.Next(1000,2000)");
         
            CodeExpressionStatement stmt1 = new CodeExpressionStatement(snippet1);
            CodeExpressionStatement stmt2 = new CodeExpressionStatement(snippet2);
           

            mymethod.Statements.Add(stmt1);
            mymethod.Statements.Add(stmt2);
           
            mymethod.Attributes = MemberAttributes.Public;


            targetClass.Members.Add(mymethod);
        }
        public void AddConstructor(Dictionary<string,string> parameter)
        {
            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            // Add parameters.
            foreach (var item in parameter)
            {
                
                if (item.Value == "String")
                {
                    constructor.Parameters.Add(new CodeParameterDeclarationExpression(
                   typeof(System.String), item.Key));
                }
                if (item.Value == "Double")
                {
                    constructor.Parameters.Add(new CodeParameterDeclarationExpression(
                   typeof(System.Double), item.Key));
                }
            }
            targetClass.Members.Add(constructor);
        }

        public void AddEntryPoint()
        {
            CodeEntryPointMethod start = new CodeEntryPointMethod();
            CodeObjectCreateExpression objectCreate =
                new CodeObjectCreateExpression(
                new CodeTypeReference("CodeDOMCreatedClass"),
                new CodePrimitiveExpression(5.3),
                new CodePrimitiveExpression(6.9));

            // Add the statement:
            // "CodeDOMCreatedClass testClass =
            //     new CodeDOMCreatedClass(5.3, 6.9);"
            start.Statements.Add(new CodeVariableDeclarationStatement(
                new CodeTypeReference("CodeDOMCreatedClass"), "testClass",
                objectCreate));

            // Creat the expression:
            // "testClass.ToString()"
            CodeMethodInvokeExpression toStringInvoke =
                new CodeMethodInvokeExpression(
                new CodeVariableReferenceExpression("testClass"), "ToString");

            // Add a System.Console.WriteLine statement with the previous
            // expression as a parameter.
            start.Statements.Add(new CodeMethodInvokeExpression(
                new CodeTypeReferenceExpression("System.Console"),
                "WriteLine", toStringInvoke));
            targetClass.Members.Add(start);
        }

        public void GenerateCSharpCode(string fileName)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(@"C:\Users\Dell E6520\source\repos\SampleCodeDom\SampleCodeDom\Controllers\" + fileName))
            {
                provider.GenerateCodeFromCompileUnit(
                    targetUnit, sourceWriter, options);
            }
        }
        void NameSpaceImporter()
        {
          
        }

        static void Main()
        {
            Console.WriteLine("Enter Controller Name");
            var controllerName = Console.ReadLine();
            TestApp sample = new TestApp(controllerName);

            
            Dictionary<string, string> fieldNames = new Dictionary<string, string>();
            fieldNames.Add("FirstName","String");
            fieldNames.Add("LastName", "String");

            Dictionary<string, string> propertyNames = new Dictionary<string, string>();
            propertyNames.Add("Name","String");
            propertyNames.Add("Category", "String");


            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("value1","String");
            parameters.Add("value2", "Double");


            sample.AddFields(fieldNames);
            sample.AddNewProperty(propertyNames);
            sample.AddMethod("Get");
            
            //sample.AddConstructor(parameters);
            //  sample.AddEntryPoint();
            sample.GenerateCSharpCode(controllerName+"Controller"+".cs");
        }
    }
}