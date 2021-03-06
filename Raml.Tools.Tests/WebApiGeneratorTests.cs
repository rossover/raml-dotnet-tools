﻿using NUnit.Framework;
using Raml.Parser;
using Raml.Tools.WebApiGenerator;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Raml.Tools.Tests
{
    [TestFixture]
    public class WebApiGeneratorTests
    {
        [Test]
        public async void Should_Generate_Controller_Objects_When_Movies()
        {
            var model = await GetMoviesGeneratedModel();
            Assert.AreEqual(2, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Box()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(10, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Congo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(2, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Contacts()
        {
            var model = await GetContactsGeneratedModel();
            Assert.AreEqual(2, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_GitHub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(17, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Instagram()
        {
            var model = await GetInstagramGeneratedModel();
            Assert.AreEqual(7, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Large()
        {
            var model = await GetLargeGeneratedModel();
            Assert.AreEqual(10, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Regression()
        {
            var model = await GetRegressionGeneratedModel();
            Assert.AreEqual(2, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Test()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(2, model.Controllers.Count());
        }

        [Test]
        public async void Should_Generate_Controller_Objects_When_Twitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(16, model.Controllers.Count());
        }

        [Test]
        public async void Should_Remove_Not_Used_Objects_Dars()
        {
            var model = await GetDarsGeneratedModel();
            Assert.AreEqual(2, model.Objects.Count());
        }

        [Test]
        public async void Should_Not_Remove_Used_Objects_Twitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ContainedWithin"));
            Assert.AreEqual(62, model.Objects.Count());
        }

        [Test]
        public async void Should_Name_Schemas_Using_Keys()
        {
            var model = await GetSchemaTestsGeneratedModel();
            Assert.IsTrue(model.Objects.Any(o => o.Name == "Thing"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "Things"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ThingResult"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ThingRequest"));
            Assert.AreEqual(5, model.Objects.Count());
        }

        [Test]
        public async void Should_Generate_Properties_When_Movies()
        {
            var model = await GetMoviesGeneratedModel();
            Assert.AreEqual(82, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Box()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(25, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Congo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(29, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Contacts()
        {
            var model = await GetContactsGeneratedModel();
            Assert.AreEqual(3, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_GitHub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(692, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Instagram()
        {
            var model = await GetInstagramGeneratedModel();
            Assert.AreEqual(35, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Large()
        {
            var model = await GetLargeGeneratedModel();
            Assert.AreEqual(24, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Regression()
        {
            var model = await GetRegressionGeneratedModel();
            Assert.AreEqual(130, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Test()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(36, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Twitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(348, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async Task Should_Generate_Valid_XML_Comments_WhenGithub()
        {
            var model = await GetGitHubGeneratedModel();
            var xmlDoc = new XmlDocument();
            foreach (var method in model.Controllers.SelectMany(c => c.Methods))
            {
                var xmlComment = GetXml(method.XmlComment);
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlComment));
            }
        }

        [Test]
        public async Task Should_Generate_Valid_XML_Comments_WhenTwitter()
        {
            var model = await GetTwitterGeneratedModel();
            var xmlDoc = new XmlDocument();
            foreach (var method in model.Controllers.SelectMany(c => c.Methods))
            {
                var xmlComment = GetXml(method.XmlComment);
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlComment));
            }
        }

        [Test]
        public async Task Should_Generate_Valid_XML_Comments_WhenMovies()
        {
            var model = await GetMoviesGeneratedModel();
            var xmlDoc = new XmlDocument();
            foreach (var method in model.Controllers.SelectMany(c => c.Methods))
            {
                var xmlComment = GetXml(method.XmlComment);
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlComment));
            }
        }

        [Test]
        public async Task Should_Link_Response_And_Request_With_Types_When_Orders_XML()
        {
            var model = await GetOrdersXmlGeneratedModel();
            Assert.AreEqual("PurchaseOrderType", model.Controllers.First().Methods.First(m => m.Verb == "Get").ReturnType);
            Assert.AreEqual("PurchaseOrderType", model.Controllers.First().Methods.First(m => m.Verb == "Post").Parameter.Type);
        }



        [Test]
        public async Task ShouldGenerateProperties_Issue17()
        {
            var model = await GetIssue17GeneratedModel();
            Assert.IsTrue(model.Objects.All(o => o.Properties.Count == 4));
        }

        [Test]
        public async Task ShouldParseSchemas_Issue13()
        {
            var model = await GetIssue13GeneratedModel();
            Assert.AreEqual(23, model.Objects.Count());
        }

        [Test]
        public async Task ShouldNotReuseModelIfDifferent_Issue25()
        {
            var model = await GetIssue25GeneratedModel();
            Assert.AreEqual(6, model.Objects.Count());
        }

        [Test]
        public async Task ShouldInheritUriParametersType_Issue23()
        {
            var model = await GetIssue23GeneratedModel();
            Assert.AreEqual("int", model.Controllers.First().Methods.First(m => m.Name == "GetById").UriParameters.First().Type);
            Assert.AreEqual("int", model.Controllers.First().Methods.First(m => m.Name == "GetHistory").UriParameters.First().Type);
        }

        [Test]
        public async Task ShouldNotRemoveAdditionalPropertiesProperty()
        {
            var model = await GetAdditionalPropertiesGeneratedModel();
            Assert.IsTrue(model.Objects.Any(o => o.Properties.Any(p => p.IsAdditionalProperties)));
        }

        [Test]
        public async Task NestedAdditionalProperties()
        {
            var raml = await new RamlParser().LoadAsync("files/additionalprops-nested.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();
            Assert.AreEqual(2, model.Objects.Count());
        }

        [Test]
        public async Task NestedAdditionalProperties_v4Schema()
        {
            var raml = await new RamlParser().LoadAsync("files/additionalprops-nested-v4.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();
            Assert.AreEqual(2, model.Objects.Count());
        }

        [Test]
        public async Task ShouldParseTratisWith2Responses()
        {
            var model = await GetIssue37GeneratedModel();
            Assert.AreEqual(3, model.Objects.Count());
        }

        [Test]
        public async Task ShouldGenerateRootResource()
        {
            var model = await GetRootGeneratedModel();
            Assert.IsTrue(model.Controllers.Any(c => c.Name == "RootUrl"));
            Assert.AreEqual(3, model.Controllers.First(c => c.Name == "RootUrl").Methods.Count);
        }

        [Test]
        public async Task ShouldAccept3LevelNestingSchema()
        {
            var raml = await new RamlParser().LoadAsync("files/level3nest.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();
            Assert.AreEqual(4, model.Objects.Count());
        }

        [Test]
        public async Task ShouldAccept3LevelNestingSchemaWithArray()
        {
            var raml = await new RamlParser().LoadAsync("files/level3nest-array.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();
            Assert.AreEqual(4, model.Objects.Count());
        }

        [Test]
        public async Task ShouldAccept3LevelNestingVersion3Schema_()
        {
            var raml = await new RamlParser().LoadAsync("files/level3nest-v3.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();
            Assert.AreEqual(4, model.Objects.Count());
        }

        [Test]
        public async Task ShouldAcceptReferenceToRAMLSchemaKey()
        {
            var raml = await new RamlParser().LoadAsync("files/issue59.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();
            Assert.AreEqual(20, model.Objects.Count());
        }

        [Test]
        public async Task ShouldAcceptReferenceToRAMLSchemaKey_issue64()
        {
            var raml = await new RamlParser().LoadAsync("files/issue64.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();
            Assert.AreEqual(4, model.Objects.Count());
            Assert.AreNotEqual("string", model.Controllers.First().Methods.First().Parameter.Type);
        }


        private static string GetXml(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                return comment;

            return "<root>" + comment.Replace("///", string.Empty).Replace("\\\"", "\"") + "</root>";
        }

		private static async Task<WebApiGeneratorModel> GetTestGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("files/test.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();

            return model;
        }

		private static async Task<WebApiGeneratorModel> GetBoxGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("files/box.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();

            return model;
        }


        private async Task<WebApiGeneratorModel> GetLargeGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/large.raml");

            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();
			return model;
		}

        private async Task<WebApiGeneratorModel> GetRegressionGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/regression.raml");

            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();
			return model;
		}

		private async Task<WebApiGeneratorModel> GetCongoGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("files/congo-drones-5-f.raml");
            return new WebApiGeneratorService(raml, "TestNs").BuildModel();
		}

		private async Task<WebApiGeneratorModel> GetInstagramGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("files/instagram.raml");
            return new WebApiGeneratorService(raml, "TestNs").BuildModel();
		}

		private async Task<WebApiGeneratorModel> GetTwitterGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("files/twitter.raml");
            return new WebApiGeneratorService(raml, "TestNs").BuildModel();
		}

		private async Task<WebApiGeneratorModel> GetGitHubGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("files/github.raml");
            return new WebApiGeneratorService(raml, "TestNs").BuildModel();
		}

		private async Task<WebApiGeneratorModel> GetContactsGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("files/contacts.raml");
            return new WebApiGeneratorService(raml, "TestNs").BuildModel();
		}

		private static async Task<WebApiGeneratorModel> GetMoviesGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("files/movies.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();

            return model;
        }

		private static async Task<WebApiGeneratorModel> GetDarsGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("files/dars.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();

            return model;
        }

		private static async Task<WebApiGeneratorModel> GetSchemaTestsGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("files/schematests.raml");
            var model = new WebApiGeneratorService(raml, "TestNs").BuildModel();

            return model;
        }


        private static async Task<WebApiGeneratorModel> GetOrdersXmlGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/ordersXml.raml");
            var model = new WebApiGeneratorService(raml, "OrdersXml").BuildModel();

            return model;
        }
	
        private static async Task<WebApiGeneratorModel> GetIssue17GeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/issue17.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }

        private static async Task<WebApiGeneratorModel> GetIssue13GeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/issue13.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }

        private static async Task<WebApiGeneratorModel> GetIssue25GeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/issue25.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }

        private static async Task<WebApiGeneratorModel> GetIssue23GeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/issue23.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }

        private static async Task<WebApiGeneratorModel> GetAdditionalPropertiesGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/additionalprops.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }

        private static async Task<WebApiGeneratorModel> GetIssue37GeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/issue37.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }

        private static async Task<WebApiGeneratorModel> GetRootGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/root.raml");
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }
    }
}