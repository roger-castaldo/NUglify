using System.IO;
using System.Diagnostics;
using System.Text;
using NUglify.Html;
using NUglify.JavaScript;
using NUglify.Tests.JavaScript.Common;
using NUnit.Framework;

namespace NUglify.Tests.JavaScript
{
    [TestFixture]
    public class Bugs
    {
      
		[Test]
        public void Bug35()
        {
            TestHelper.Instance.RunErrorTest();
        }

		[Test]
        public void Bug57()
        {
            TestHelper.Instance.RunErrorTest();
        }
      
		[Test]
        public void Bug63()
        {
            TestHelper.Instance.RunErrorTest(JSError.NoLeftParenthesis, JSError.ExpressionExpected, JSError.NoLeftCurly, JSError.BadSwitch);
        }

		[Test]
        public void Bug70()
        {
            TestHelper.Instance.RunTest();
        }


		[Test]
        public void Bug76()
        {
            TestHelper.Instance.RunTest("-rename:all");
        }

		
		[Test, Ignore("This is broken in .NET framework, can't be fixed by the developer")]
        public void Bug78()
        {
            TestHelper.Instance.RunTest();
        }
		
        [Test]
        public void Bug79()
        {
            TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug80()
        {
            TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug87()
        {
            TestHelper.Instance.RunTest("-reorder:false -fnames:lock -unused:remove -rename:all");
        }

        [Test]
        public void Bug92()
        {
            TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug94()
        {
            TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug120()
        {
            TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug138()
        {
            TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug139()
        {
            TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug139_A()
        {
            // previously these would throw exceptions because the closing backtick was skipped, creating an invalid ast
            // manually define cr and lfs to be sure we dont have platform sillynes

            var uglifyResult = Uglify.Js(@"var testString = `
`;
            testString += `} async init(){ }`;");
            Assert.That(uglifyResult.Code, Is.EqualTo("var testString=`\n`+`} async init(){ }`"));

            // CR
            uglifyResult = Uglify.Js("var testString = `"+ (char)13 +@"`;
            testString += `} async init(){ }`;");
            Assert.That(uglifyResult.Code, Is.EqualTo("var testString=`\r`+`} async init(){ }`"));

            // LF
            uglifyResult = Uglify.Js("var testString = `" + (char)10 + @"`;
            testString += `} async init(){ }`;");
            Assert.That(uglifyResult.Code, Is.EqualTo("var testString=`\n`+`} async init(){ }`"));

            // CRLF
            uglifyResult = Uglify.Js("var testString = `" + (char)13 + (char)10 + @"`;
            testString += `} async init(){ }`;");
            Assert.That(uglifyResult.Code, Is.EqualTo("var testString=`\r\n`+`} async init(){ }`"));

            //LFCR
            uglifyResult = Uglify.Js("var testString = `" + (char)10 + (char)13 + @"`;
            testString += `} async init(){ }`;");
            Assert.That(uglifyResult.Code, Is.EqualTo("var testString=`\n\r`+`} async init(){ }`"));

        }

        [Test]
        public void Bug156()
        {
	        TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug159()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }


        [Test]
        public void Bug160()
        {
	        TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug163()
        {
	        TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug181()
        {
	        var uglifyResult = Uglify.Js("function foo() { return 1; }",
		        new CodeSettings {Indent = "   ", OutputMode = OutputMode.MultipleLines});
	        Assert.That(uglifyResult.Code, Is.EqualTo("function foo()\n{\n   return 1\n}"));
        }

        [Test]
        public void Bug197()
        {
	        TestHelper.Instance.RunTest("-pretty -line:m,\t");
        }

        [Test]
        public void Bug199JSON()
        {
	        TestHelper.Instance.RunTest("-js:json");
        }

        [Test]
        public void Bug199JS()
        {
	        TestHelper.Instance.RunTest();
        }


        [Test]
        public void Bug199_SourceMap()
        {
	        UglifyResult result;

	        string sFileContent = @"define(""moment"", [], function() { return (function(modules) { })
({
	/***/ ""./node_modules/moment/locale sync recursive ^\\.\\/.*$"":
	/*! no static exports found */
	/***/ (function(module, exports, __webpack_require__) { } ) } ) } )";

	        var builder = new StringBuilder();
	        using (TextWriter mapWriter = new StringWriter(builder))
	        {
		        using (var sourceMap = new V3SourceMap(mapWriter))
		        {
			        sourceMap.MakePathsRelative = false;

			        var settings = new CodeSettings();
                    settings.LineTerminator = "\n";
			        settings.SymbolsMap = sourceMap;
			        sourceMap.StartPackage(@"C:\some\long\path\to\js", @"C:\some\other\path\to\map");

			        result = Uglify.Js(sFileContent, @"C:\some\path\to\output\js", settings);
		        }
	        }

	        var expected = @"define(""moment"",[],function(){return function(){}({""./node_modules/moment/locale sync recursive ^\\.\\/.*$"":function(){}})})
//# sourceMappingURL=C:\some\other\path\to\map
";
	        Assert.That(result.Code, Is.EqualTo(expected));

	        var actual = builder.ToString().Replace("\r\n", "\n");
	        Assert.That(actual, Is.EqualTo(@"{
""version"":3,
""file"":""C:\\some\\long\\path\\to\\js"",
""mappings"":""AAAAA,MAAM,CAAC,QAAQ,CAAE,CAAA,CAAE,CAAE,QAAQ,CAAA,CAAG,CAAE,OAAQ,QAAQ,CAAA,CAAU,EAC5D,CAAC,CACM,wDAAwD,CAEvDC,QAAQ,CAAA,CAAuC,EAHtD,CAAD,CADgC,CAA1B"",
""sources"":[""C:\\some\\path\\to\\output\\js""],
""names"":[""define"",""./node_modules/moment/locale sync recursive ^\\.\\/.*$""]
}
"));
        }

        [Test]
        public void Bug200()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }


        [Test]
        public void Bug201()
        {
	        TestHelper.Instance.RunErrorTest("-rename:all");
        }

        [Test]
        public void Bug204()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }
		
        [Test]
        public void Bug205()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }
        
        [Test]
        public void Bug214()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug215()
        {
	        TestHelper.Instance.RunErrorTest("-rename:all");
        }

        [Test]
        public void Bug216()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug241()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug253()
        {
            TestHelper.Instance.RunTest();
        }

        [Test]
        public void Bug264()
        {
	        TestHelper.Instance.RunErrorTest("-rename:all");
        }

        [Test]
        public void Bug266()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug274()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug279()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug284()
        {
	        TestHelper.Instance.RunErrorTest("-rename:all", JSError.NoSemicolon, JSError.ExpressionExpected, JSError.SyntaxError, JSError.UndeclaredFunction, JSError.UndeclaredVariable);
        }

        [Test]
        public void Bug285()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug290()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug293()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug298()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug300()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug301()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug305()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug306()
        {
	        TestHelper.Instance.RunTest("-js:json");
        }

        [Test]
        public void Bug345()
        {
            TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug353()
        {
            TestHelper.Instance.RunErrorTest();
        }

        [Test]
        public void Bug360()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug375()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug391()
        {
	        TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug394()
        {
            TestHelper.Instance.RunTest("-rename:all");
        }

        [Test]
        public void Bug429()
        {
            TestHelper.Instance.RunTest("-rename:all");
        }
    }
}
