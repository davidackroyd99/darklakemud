using DarkLakeMUD.Parser;
using NUnit.Framework;

namespace DarkLakeMUD.Tests
{
    public class ParserTests
    {
        private IParser _parser;

        [SetUp]
        public void Setup()
        {
            _parser = new DefaultParser();
        }

        [Test]
        public void BasicSayTest()
        {
            var command = _parser.Parse("say hello, everyone!");

            Assert.AreEqual(command.Verb, Verb.Say);
            Assert.AreEqual(command.Noun, "hello, everyone!");
        }
    }
}