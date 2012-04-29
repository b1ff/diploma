using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Gamification.Core.Entities;
using Gamification.Core.Enums;
using Gamification.Core.Extensions;
using Gamification.Web.Controllers;
using Gamification.Web.Utils;
using NUnit.Framework;

namespace Gamification.Testing.Unit
{
    [TestFixture]
    public class EnumsConventionsTests
    {
        [Test]
        public void AllEnums_ShouldBeDerivedFromByte()
        {
            var wrongEnumNames = new List<string>();
            CheckEnums(wrongEnumNames, typeof(BaseEntity).Assembly);
            CheckEnums(wrongEnumNames, typeof(HomeController).Assembly);
            CheckEnums(wrongEnumNames, typeof(CryptoHelper).Assembly);

            if (wrongEnumNames.IsNotEmpty())
            {
                Debug.WriteLine("You should derive next enums from byte:");
                foreach (var wrongEnumName in wrongEnumNames)
                {
                    Debug.WriteLine(wrongEnumName);
                }

                Assert.Fail();
            }
        }

        private void CheckEnums(List<string> wrongEnumListToAppend, Assembly assembly)
        {
            var wrongEnums = assembly.GetTypes()
                .Where(x => x.IsEnum && x.GetEnumUnderlyingType() != typeof(byte)).ToList();

            foreach (var wrongEnum in wrongEnums)
            {
                wrongEnumListToAppend.Add(wrongEnum.FullName);
            }
        }
    }
}
