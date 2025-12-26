using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using WMS.Controllers;
using WMS.ViewModel.BaseData.BaseBarCodeVMs;
using WMS.Model.BaseData;
using WMS.DataAccess;


namespace WMS.Test
{
    [TestClass]
    public class BaseBarCodeApiTest
    {
        private BaseBarCodeApiController _controller;
        private string _seed;

        public BaseBarCodeApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<BaseBarCodeApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new BaseBarCodeApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            BaseBarCodeApiVM vm = _controller.Wtm.CreateVM<BaseBarCodeApiVM>();
            BaseBarCode v = new BaseBarCode();
            
            v.DocNo = "kjnOcKfMnr6sIts5Ca";
            v.Code = "LXvgz2IhCjIoXJlN5SFr7DAARgp8OIjip5cF6VHLhVqbaqzBMMmp1n88d65PFHX9JiqXqn2YkRVZGLxHIqIsWQVPvuJd87yvzjkW1c3j6k5zhvFpr0QwKEYFMgAlEqNP3Ub4KQk3J2f9rZAgegUaEQ1zqawWcmgFnrEAL9mVClfETJ6cVjbdnGlgLO7tvebRwodHjS";
            v.ItemId = AddBaseItemMaster();
            v.Qty = 23;
            v.CustomerCode = "XewmZ8zpO9LTdVPgBf71";
            v.CustomerName = "ykDTRyioz6OyEmd8OyuQWodusuholOzn8CR7FC9SxGC3I9vY2ftHPjPVcJ6vPsavuZE6pbOROSQLvzEG04XScFV";
            v.CustomerNameFirstLetter = "RRBHynB8DBh3khG4LqspG1";
            v.Seiban = "kxzxGsmPOLlqqNG0LrrkBEQ6xHhJcSkBMTYvgoXkQTru212gn";
            v.ExtendedFields1 = "6S9ZZ303H5ek7zq96fkTX0PgCJCdAYYYG9gLnCTX8snKryt6X7T6OPQHm2A9PGnLLb5Ju01cEeCbNxxp3UsyVFwDl623TMIbIrksDCNk4codsGSLMXkcMOG1fSiWWr2keUS65ZUwlB02YeONDc1i0Zdm56gtisJU4YXSGjmZg3RwcW80icKfOrX8RrVTmJu9EXpyUS4vnd5MCMK5bypJ6YAL4fHNq0vDMDxXtpeOpi6w6E6qvxAffR";
            v.ExtendedFields2 = "8dpce8Fx6A2R0IZ1cLZhP1WNlSoFm0d3jYz98s5DOJWhEKIJomTgznC9MZfN6S0IwrXJWaf3ieibos1J5vL7KMMFTtUW6A5mZ7geRObq5hkkOrMXE3q7Td9GCQA8xZIHkc7xYmShdizuGuBRV9WDzxoSbSZAKvj39iNKF2JfUlpZFyiU9uaSuglCc5MUUtJkbjUmKtUnmbjyiTX57lEKzbkBh6mMiHoTtnd4qaMYNGcTaJlkcqFKchV19FLWAgaUGj9Z9JjiMTwGdkXPQfP";
            v.ExtendedFields3 = "HHaNaQ7vjTkcmYsiTf7IgHqqdVnwm1L8NfHE5yp7Q0v0RF0b9r0RtaB3NF2ipejrZG7pYEFr2LOycpwmgmeq9lEFeHZMTyJXDcPFfdtW7slpVAljGio71gao5wiZqTr3b7V5oEPRZp206y6GNgRdzgT1T1rkL5SKKBYVJLJpmaXujp5KH6iMLfYJdBDHpkRKrhUibEN0sm2yallzDCdSrgzRRYsyT81dNt0L3PfWgPyU2bMMAcEu09rgrQckXHQltpfEF1QzkpDYlMyZzKTlBZqJ5YXVSNZoIpi7R93gtURN2E0D1f07CnqEBg1sInbOFzkUNW52q0LuAmiUNIpQJixQWb5yCLbuCpNqjZJgvNFGKVqFV0XvrwApzQGD8pwigy3HShBXjD1iivatlOxo20T4cRHc14";
            v.ExtendedFields4 = "KLFZVLEJnz7XOVgxHtTxQ4o53SqkxUjnHq9";
            v.ExtendedFields5 = "iC5WV8zwoUOcYIZSc3wAY8YEiotuImwnXpXH0mHPbhA0M";
            v.ExtendedFields6 = "5YxT6ELUZfmgK1rEq0y1";
            v.ExtendedFields7 = "2AMTn5q0rDokUhHi6NJj66q8c94CtxVa8DOOUDmqKCGzCEqBFGjoc8cepEOe2ipoKGDEj56TPPrutATnDCXzE3CnlJi0";
            v.ExtendedFields8 = "mFA50t1nE3zwQ4AzWMZb2EGWw5TFtRYp7i6sVZMfocPuIoT7yfIjFDjIFnbxPviiwnNwW9InP4d6k9nULgMoqccHVyeFGZFXYA1OIytkXJ07UgMR5jHHW56WdqWnp3FYSe7rX0WeykQuSSaf9DniOyUm4TEhSuF6OYKRGNAwwwW9996Iub3fiunQRN7KUmSwzxfD4EP5rDup1oKoPtTFekNylkPu9RmB1eO96iwg8nemA6aT3BSf0LwC4rGyA9o3YXEUPfj9R1Hphaw0pcxUX3rZpWv3DoWJs2xITXjrQuOlIckRe9EBKJL81CJMf30hN7gd1JoqgItPMuw5Cf3AogIG8drh1hulTEZdcntAX0z20dSoesGgLBDcoQrgJDaeRrgu8B3rIq";
            v.ExtendedFields9 = "ntfV88GMg2oW4WW2JvBEKxO8d6cnCrWHwS4r4RTz7NcVHz5IyqKYlgc60GSfCsYx2dYRhMMMq4ZliM5jK4xbVX9QiKc3QTzRPu9jq6VUzYoTixLSp3PIP6intQdmK8";
            v.ExtendedFields10 = "WWjf9hHEz0G8PQDRZN7tQcQH9Ce09kSJ0mXfjcwwEBApsyJqnPaa1fN1P1UxEs8hbC39SwV3wrW6ZADxmPN96MnXTBunJz2hvispM7c1dKf8AcgrGMsigZtCgv2GLm9YbdyVzeYgYIO5H3mlO0CtO46iMtU3pWdL4RjuhTryyVoTclUUjVAaU91K56bJx6RGHaxpwglpFGUpzLE2lTvfyqGq1ggMQS9SqT9agTq7SyEU1afTJWYevjmhHUIF9y54O3wFNxIbvwuxz0aOZaKD8wZO6eQb6tzybMQxwzLnzMaIFtRZ3oIYxxUOaHoFI2elyPxwzmY9BvJJH9JslQ2s2DZFvQo3IcSvMJZTY0SUHDXPOtNGlcdZDQ7bgP47NHUQUdkSZJzxQ7HjOxTOX4i6xyU8Yx93uwGTOsGNpifJdbJ6B4A3QQSf6CBrRcd7HYusIWrLejxS7BBuzm";
            v.ExtendedFields11 = "nnjTji7vDeNSVKiADX4GqwFlkRseUbqCgj1KIYnmLiAHEwPrPKA5s5lO9K2FzItLk5MS0WiuZHivOiIvT6p4zXrJzmWmQqyR243SExw3zKDbzzTz5KfhkQMUwHMZ6GCD1Od0TMTFP2meElNo3qeUHMhBf9WYgfSW31pYOHuX1FkUq063wcjG2iDRgMnSBcvVCgrHREwC6KtGMQalh02KhBs7COdpAaZN6SJbkbKsfXaUWI57qaQcV0ZYWIh3Ym3x5t7wSPGcPXG2Fr5VCk3TDYBOtH689s71D2NZT2YB8IVLHGHXEu62fsmHXd396i1MpkaTAwdtbGvJcrJhUgLAy28gF0Xzq8PiLTYzmcw03PE1Rw7oyPHDepsxM";
            v.ExtendedFields12 = "kSuOvV6hwa97ar0xKb3YYFC1hw0eO34OlciZOIzYjyntHLl6unVETPzLGOCA7hUknnCqLzolwtuy4IifwpPZWEjGrkAoicIwJO6isU43yJSowJQsyOsPs72SozWpIqNc8tMtfZ4Vqzr7JH1Ijo6Vpxmfl6k6HSmszjQPrgP6D3KWRWPJ6TSwgb1lCuFEExI9N7Tgab";
            v.ExtendedFields13 = "MRoTxcnRWa4CXfoWjjmtOmcQAt3ost7u6CHKdgHsiVmmG5qilEx1CW5QHJDf9PvS2gUohYYS2S3GU8GwUwpZIm0YOhcF2MxoBEgHM7aXEE0zrFUaz215lY4fXVgGX3FnJecEHUZ80qzR1Y55OWW5gHEBSfrqYAlMn2DcOE6LbkqQbxKG2EcuNc2JK4qCEiGnSkghTn5cUaiRlm2zS8aDA29Ryoqq6kM6VHZ9CL007i9hDTvV47cUycRMWraNTMPeDH0PWWKFHiA0UjF7jEInMh2V8Js8L6ZptKaMPybqu522aOWJFk7Au9kUALaCIqOJRB8g0GpMqullzpqEKlyW9Fmgs9xvqnX6szuCXstNtbqkU8WpmtzkSLPUEg88vv0UbA81L3wnC3II4ezubrRd7kfQZwbi8ErLQ";
            v.ExtendedFields14 = "OfQwbawu9gSXO6JUimJi2cFLiuNMtNO6uneNOvjsVYojlOfB1t0w9dioywcdNcdrro6SHpzltRFW5n7lesViaU7AXfzOlSAeSo5fkFZj1FURohf8Ct2YlGNezlDoLcVGggefYHxqUGkZQYwQFC3zji96NQyn8eP8DNSs8ST3eORDiRHalPHMY543Kc6mlH9D2x3XCE4JV6OXpmARjKb7QWg3wIHFiAzsyeSeLujv8ClIjN6";
            v.ExtendedFields15 = "DmUwA7CCtBzX6SXNPW3qtaX2AOsW3ylj96T52krh11TYUYZNkaVDTV2dxvHlOtNkPfiwHnszKvhB61sdSodugltXoeNJt2wnjiAjIXy1KCZ7W1hY6QIziFqPYUXanw5JpLiCMpBGz1xKNXK6z5nD2Xe7EgMYQAsDO9URgxAkR0uSAYQAeTjrLkMA43tms9yozlWBoVtuW6HPORuaHHfvKwi84rhO4wMfRvHjgIBXfcLba0jPh33fyfOg0bpm7DfVxlmold8Zve1M1KQJjvJ8eRkcUuzumSDSMMUyX3B9Y1DL7joeuWT8ihmZBjPcvXJPjx";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BaseBarCode>().Find(v.ID);
                
                Assert.AreEqual(data.DocNo, "kjnOcKfMnr6sIts5Ca");
                Assert.AreEqual(data.Code, "LXvgz2IhCjIoXJlN5SFr7DAARgp8OIjip5cF6VHLhVqbaqzBMMmp1n88d65PFHX9JiqXqn2YkRVZGLxHIqIsWQVPvuJd87yvzjkW1c3j6k5zhvFpr0QwKEYFMgAlEqNP3Ub4KQk3J2f9rZAgegUaEQ1zqawWcmgFnrEAL9mVClfETJ6cVjbdnGlgLO7tvebRwodHjS");
                Assert.AreEqual(data.Qty, 23);
                Assert.AreEqual(data.CustomerCode, "XewmZ8zpO9LTdVPgBf71");
                Assert.AreEqual(data.CustomerName, "ykDTRyioz6OyEmd8OyuQWodusuholOzn8CR7FC9SxGC3I9vY2ftHPjPVcJ6vPsavuZE6pbOROSQLvzEG04XScFV");
                Assert.AreEqual(data.CustomerNameFirstLetter, "RRBHynB8DBh3khG4LqspG1");
                Assert.AreEqual(data.Seiban, "kxzxGsmPOLlqqNG0LrrkBEQ6xHhJcSkBMTYvgoXkQTru212gn");
                Assert.AreEqual(data.ExtendedFields1, "6S9ZZ303H5ek7zq96fkTX0PgCJCdAYYYG9gLnCTX8snKryt6X7T6OPQHm2A9PGnLLb5Ju01cEeCbNxxp3UsyVFwDl623TMIbIrksDCNk4codsGSLMXkcMOG1fSiWWr2keUS65ZUwlB02YeONDc1i0Zdm56gtisJU4YXSGjmZg3RwcW80icKfOrX8RrVTmJu9EXpyUS4vnd5MCMK5bypJ6YAL4fHNq0vDMDxXtpeOpi6w6E6qvxAffR");
                Assert.AreEqual(data.ExtendedFields2, "8dpce8Fx6A2R0IZ1cLZhP1WNlSoFm0d3jYz98s5DOJWhEKIJomTgznC9MZfN6S0IwrXJWaf3ieibos1J5vL7KMMFTtUW6A5mZ7geRObq5hkkOrMXE3q7Td9GCQA8xZIHkc7xYmShdizuGuBRV9WDzxoSbSZAKvj39iNKF2JfUlpZFyiU9uaSuglCc5MUUtJkbjUmKtUnmbjyiTX57lEKzbkBh6mMiHoTtnd4qaMYNGcTaJlkcqFKchV19FLWAgaUGj9Z9JjiMTwGdkXPQfP");
                Assert.AreEqual(data.ExtendedFields3, "HHaNaQ7vjTkcmYsiTf7IgHqqdVnwm1L8NfHE5yp7Q0v0RF0b9r0RtaB3NF2ipejrZG7pYEFr2LOycpwmgmeq9lEFeHZMTyJXDcPFfdtW7slpVAljGio71gao5wiZqTr3b7V5oEPRZp206y6GNgRdzgT1T1rkL5SKKBYVJLJpmaXujp5KH6iMLfYJdBDHpkRKrhUibEN0sm2yallzDCdSrgzRRYsyT81dNt0L3PfWgPyU2bMMAcEu09rgrQckXHQltpfEF1QzkpDYlMyZzKTlBZqJ5YXVSNZoIpi7R93gtURN2E0D1f07CnqEBg1sInbOFzkUNW52q0LuAmiUNIpQJixQWb5yCLbuCpNqjZJgvNFGKVqFV0XvrwApzQGD8pwigy3HShBXjD1iivatlOxo20T4cRHc14");
                Assert.AreEqual(data.ExtendedFields4, "KLFZVLEJnz7XOVgxHtTxQ4o53SqkxUjnHq9");
                Assert.AreEqual(data.ExtendedFields5, "iC5WV8zwoUOcYIZSc3wAY8YEiotuImwnXpXH0mHPbhA0M");
                Assert.AreEqual(data.ExtendedFields6, "5YxT6ELUZfmgK1rEq0y1");
                Assert.AreEqual(data.ExtendedFields7, "2AMTn5q0rDokUhHi6NJj66q8c94CtxVa8DOOUDmqKCGzCEqBFGjoc8cepEOe2ipoKGDEj56TPPrutATnDCXzE3CnlJi0");
                Assert.AreEqual(data.ExtendedFields8, "mFA50t1nE3zwQ4AzWMZb2EGWw5TFtRYp7i6sVZMfocPuIoT7yfIjFDjIFnbxPviiwnNwW9InP4d6k9nULgMoqccHVyeFGZFXYA1OIytkXJ07UgMR5jHHW56WdqWnp3FYSe7rX0WeykQuSSaf9DniOyUm4TEhSuF6OYKRGNAwwwW9996Iub3fiunQRN7KUmSwzxfD4EP5rDup1oKoPtTFekNylkPu9RmB1eO96iwg8nemA6aT3BSf0LwC4rGyA9o3YXEUPfj9R1Hphaw0pcxUX3rZpWv3DoWJs2xITXjrQuOlIckRe9EBKJL81CJMf30hN7gd1JoqgItPMuw5Cf3AogIG8drh1hulTEZdcntAX0z20dSoesGgLBDcoQrgJDaeRrgu8B3rIq");
                Assert.AreEqual(data.ExtendedFields9, "ntfV88GMg2oW4WW2JvBEKxO8d6cnCrWHwS4r4RTz7NcVHz5IyqKYlgc60GSfCsYx2dYRhMMMq4ZliM5jK4xbVX9QiKc3QTzRPu9jq6VUzYoTixLSp3PIP6intQdmK8");
                Assert.AreEqual(data.ExtendedFields10, "WWjf9hHEz0G8PQDRZN7tQcQH9Ce09kSJ0mXfjcwwEBApsyJqnPaa1fN1P1UxEs8hbC39SwV3wrW6ZADxmPN96MnXTBunJz2hvispM7c1dKf8AcgrGMsigZtCgv2GLm9YbdyVzeYgYIO5H3mlO0CtO46iMtU3pWdL4RjuhTryyVoTclUUjVAaU91K56bJx6RGHaxpwglpFGUpzLE2lTvfyqGq1ggMQS9SqT9agTq7SyEU1afTJWYevjmhHUIF9y54O3wFNxIbvwuxz0aOZaKD8wZO6eQb6tzybMQxwzLnzMaIFtRZ3oIYxxUOaHoFI2elyPxwzmY9BvJJH9JslQ2s2DZFvQo3IcSvMJZTY0SUHDXPOtNGlcdZDQ7bgP47NHUQUdkSZJzxQ7HjOxTOX4i6xyU8Yx93uwGTOsGNpifJdbJ6B4A3QQSf6CBrRcd7HYusIWrLejxS7BBuzm");
                Assert.AreEqual(data.ExtendedFields11, "nnjTji7vDeNSVKiADX4GqwFlkRseUbqCgj1KIYnmLiAHEwPrPKA5s5lO9K2FzItLk5MS0WiuZHivOiIvT6p4zXrJzmWmQqyR243SExw3zKDbzzTz5KfhkQMUwHMZ6GCD1Od0TMTFP2meElNo3qeUHMhBf9WYgfSW31pYOHuX1FkUq063wcjG2iDRgMnSBcvVCgrHREwC6KtGMQalh02KhBs7COdpAaZN6SJbkbKsfXaUWI57qaQcV0ZYWIh3Ym3x5t7wSPGcPXG2Fr5VCk3TDYBOtH689s71D2NZT2YB8IVLHGHXEu62fsmHXd396i1MpkaTAwdtbGvJcrJhUgLAy28gF0Xzq8PiLTYzmcw03PE1Rw7oyPHDepsxM");
                Assert.AreEqual(data.ExtendedFields12, "kSuOvV6hwa97ar0xKb3YYFC1hw0eO34OlciZOIzYjyntHLl6unVETPzLGOCA7hUknnCqLzolwtuy4IifwpPZWEjGrkAoicIwJO6isU43yJSowJQsyOsPs72SozWpIqNc8tMtfZ4Vqzr7JH1Ijo6Vpxmfl6k6HSmszjQPrgP6D3KWRWPJ6TSwgb1lCuFEExI9N7Tgab");
                Assert.AreEqual(data.ExtendedFields13, "MRoTxcnRWa4CXfoWjjmtOmcQAt3ost7u6CHKdgHsiVmmG5qilEx1CW5QHJDf9PvS2gUohYYS2S3GU8GwUwpZIm0YOhcF2MxoBEgHM7aXEE0zrFUaz215lY4fXVgGX3FnJecEHUZ80qzR1Y55OWW5gHEBSfrqYAlMn2DcOE6LbkqQbxKG2EcuNc2JK4qCEiGnSkghTn5cUaiRlm2zS8aDA29Ryoqq6kM6VHZ9CL007i9hDTvV47cUycRMWraNTMPeDH0PWWKFHiA0UjF7jEInMh2V8Js8L6ZptKaMPybqu522aOWJFk7Au9kUALaCIqOJRB8g0GpMqullzpqEKlyW9Fmgs9xvqnX6szuCXstNtbqkU8WpmtzkSLPUEg88vv0UbA81L3wnC3II4ezubrRd7kfQZwbi8ErLQ");
                Assert.AreEqual(data.ExtendedFields14, "OfQwbawu9gSXO6JUimJi2cFLiuNMtNO6uneNOvjsVYojlOfB1t0w9dioywcdNcdrro6SHpzltRFW5n7lesViaU7AXfzOlSAeSo5fkFZj1FURohf8Ct2YlGNezlDoLcVGggefYHxqUGkZQYwQFC3zji96NQyn8eP8DNSs8ST3eORDiRHalPHMY543Kc6mlH9D2x3XCE4JV6OXpmARjKb7QWg3wIHFiAzsyeSeLujv8ClIjN6");
                Assert.AreEqual(data.ExtendedFields15, "DmUwA7CCtBzX6SXNPW3qtaX2AOsW3ylj96T52krh11TYUYZNkaVDTV2dxvHlOtNkPfiwHnszKvhB61sdSodugltXoeNJt2wnjiAjIXy1KCZ7W1hY6QIziFqPYUXanw5JpLiCMpBGz1xKNXK6z5nD2Xe7EgMYQAsDO9URgxAkR0uSAYQAeTjrLkMA43tms9yozlWBoVtuW6HPORuaHHfvKwi84rhO4wMfRvHjgIBXfcLba0jPh33fyfOg0bpm7DfVxlmold8Zve1M1KQJjvJ8eRkcUuzumSDSMMUyX3B9Y1DL7joeuWT8ihmZBjPcvXJPjx");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

        [TestMethod]
        public void EditTest()
        {
            BaseBarCode v = new BaseBarCode();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DocNo = "kjnOcKfMnr6sIts5Ca";
                v.Code = "LXvgz2IhCjIoXJlN5SFr7DAARgp8OIjip5cF6VHLhVqbaqzBMMmp1n88d65PFHX9JiqXqn2YkRVZGLxHIqIsWQVPvuJd87yvzjkW1c3j6k5zhvFpr0QwKEYFMgAlEqNP3Ub4KQk3J2f9rZAgegUaEQ1zqawWcmgFnrEAL9mVClfETJ6cVjbdnGlgLO7tvebRwodHjS";
                v.ItemId = AddBaseItemMaster();
                v.Qty = 23;
                v.CustomerCode = "XewmZ8zpO9LTdVPgBf71";
                v.CustomerName = "ykDTRyioz6OyEmd8OyuQWodusuholOzn8CR7FC9SxGC3I9vY2ftHPjPVcJ6vPsavuZE6pbOROSQLvzEG04XScFV";
                v.CustomerNameFirstLetter = "RRBHynB8DBh3khG4LqspG1";
                v.Seiban = "kxzxGsmPOLlqqNG0LrrkBEQ6xHhJcSkBMTYvgoXkQTru212gn";
                v.ExtendedFields1 = "6S9ZZ303H5ek7zq96fkTX0PgCJCdAYYYG9gLnCTX8snKryt6X7T6OPQHm2A9PGnLLb5Ju01cEeCbNxxp3UsyVFwDl623TMIbIrksDCNk4codsGSLMXkcMOG1fSiWWr2keUS65ZUwlB02YeONDc1i0Zdm56gtisJU4YXSGjmZg3RwcW80icKfOrX8RrVTmJu9EXpyUS4vnd5MCMK5bypJ6YAL4fHNq0vDMDxXtpeOpi6w6E6qvxAffR";
                v.ExtendedFields2 = "8dpce8Fx6A2R0IZ1cLZhP1WNlSoFm0d3jYz98s5DOJWhEKIJomTgznC9MZfN6S0IwrXJWaf3ieibos1J5vL7KMMFTtUW6A5mZ7geRObq5hkkOrMXE3q7Td9GCQA8xZIHkc7xYmShdizuGuBRV9WDzxoSbSZAKvj39iNKF2JfUlpZFyiU9uaSuglCc5MUUtJkbjUmKtUnmbjyiTX57lEKzbkBh6mMiHoTtnd4qaMYNGcTaJlkcqFKchV19FLWAgaUGj9Z9JjiMTwGdkXPQfP";
                v.ExtendedFields3 = "HHaNaQ7vjTkcmYsiTf7IgHqqdVnwm1L8NfHE5yp7Q0v0RF0b9r0RtaB3NF2ipejrZG7pYEFr2LOycpwmgmeq9lEFeHZMTyJXDcPFfdtW7slpVAljGio71gao5wiZqTr3b7V5oEPRZp206y6GNgRdzgT1T1rkL5SKKBYVJLJpmaXujp5KH6iMLfYJdBDHpkRKrhUibEN0sm2yallzDCdSrgzRRYsyT81dNt0L3PfWgPyU2bMMAcEu09rgrQckXHQltpfEF1QzkpDYlMyZzKTlBZqJ5YXVSNZoIpi7R93gtURN2E0D1f07CnqEBg1sInbOFzkUNW52q0LuAmiUNIpQJixQWb5yCLbuCpNqjZJgvNFGKVqFV0XvrwApzQGD8pwigy3HShBXjD1iivatlOxo20T4cRHc14";
                v.ExtendedFields4 = "KLFZVLEJnz7XOVgxHtTxQ4o53SqkxUjnHq9";
                v.ExtendedFields5 = "iC5WV8zwoUOcYIZSc3wAY8YEiotuImwnXpXH0mHPbhA0M";
                v.ExtendedFields6 = "5YxT6ELUZfmgK1rEq0y1";
                v.ExtendedFields7 = "2AMTn5q0rDokUhHi6NJj66q8c94CtxVa8DOOUDmqKCGzCEqBFGjoc8cepEOe2ipoKGDEj56TPPrutATnDCXzE3CnlJi0";
                v.ExtendedFields8 = "mFA50t1nE3zwQ4AzWMZb2EGWw5TFtRYp7i6sVZMfocPuIoT7yfIjFDjIFnbxPviiwnNwW9InP4d6k9nULgMoqccHVyeFGZFXYA1OIytkXJ07UgMR5jHHW56WdqWnp3FYSe7rX0WeykQuSSaf9DniOyUm4TEhSuF6OYKRGNAwwwW9996Iub3fiunQRN7KUmSwzxfD4EP5rDup1oKoPtTFekNylkPu9RmB1eO96iwg8nemA6aT3BSf0LwC4rGyA9o3YXEUPfj9R1Hphaw0pcxUX3rZpWv3DoWJs2xITXjrQuOlIckRe9EBKJL81CJMf30hN7gd1JoqgItPMuw5Cf3AogIG8drh1hulTEZdcntAX0z20dSoesGgLBDcoQrgJDaeRrgu8B3rIq";
                v.ExtendedFields9 = "ntfV88GMg2oW4WW2JvBEKxO8d6cnCrWHwS4r4RTz7NcVHz5IyqKYlgc60GSfCsYx2dYRhMMMq4ZliM5jK4xbVX9QiKc3QTzRPu9jq6VUzYoTixLSp3PIP6intQdmK8";
                v.ExtendedFields10 = "WWjf9hHEz0G8PQDRZN7tQcQH9Ce09kSJ0mXfjcwwEBApsyJqnPaa1fN1P1UxEs8hbC39SwV3wrW6ZADxmPN96MnXTBunJz2hvispM7c1dKf8AcgrGMsigZtCgv2GLm9YbdyVzeYgYIO5H3mlO0CtO46iMtU3pWdL4RjuhTryyVoTclUUjVAaU91K56bJx6RGHaxpwglpFGUpzLE2lTvfyqGq1ggMQS9SqT9agTq7SyEU1afTJWYevjmhHUIF9y54O3wFNxIbvwuxz0aOZaKD8wZO6eQb6tzybMQxwzLnzMaIFtRZ3oIYxxUOaHoFI2elyPxwzmY9BvJJH9JslQ2s2DZFvQo3IcSvMJZTY0SUHDXPOtNGlcdZDQ7bgP47NHUQUdkSZJzxQ7HjOxTOX4i6xyU8Yx93uwGTOsGNpifJdbJ6B4A3QQSf6CBrRcd7HYusIWrLejxS7BBuzm";
                v.ExtendedFields11 = "nnjTji7vDeNSVKiADX4GqwFlkRseUbqCgj1KIYnmLiAHEwPrPKA5s5lO9K2FzItLk5MS0WiuZHivOiIvT6p4zXrJzmWmQqyR243SExw3zKDbzzTz5KfhkQMUwHMZ6GCD1Od0TMTFP2meElNo3qeUHMhBf9WYgfSW31pYOHuX1FkUq063wcjG2iDRgMnSBcvVCgrHREwC6KtGMQalh02KhBs7COdpAaZN6SJbkbKsfXaUWI57qaQcV0ZYWIh3Ym3x5t7wSPGcPXG2Fr5VCk3TDYBOtH689s71D2NZT2YB8IVLHGHXEu62fsmHXd396i1MpkaTAwdtbGvJcrJhUgLAy28gF0Xzq8PiLTYzmcw03PE1Rw7oyPHDepsxM";
                v.ExtendedFields12 = "kSuOvV6hwa97ar0xKb3YYFC1hw0eO34OlciZOIzYjyntHLl6unVETPzLGOCA7hUknnCqLzolwtuy4IifwpPZWEjGrkAoicIwJO6isU43yJSowJQsyOsPs72SozWpIqNc8tMtfZ4Vqzr7JH1Ijo6Vpxmfl6k6HSmszjQPrgP6D3KWRWPJ6TSwgb1lCuFEExI9N7Tgab";
                v.ExtendedFields13 = "MRoTxcnRWa4CXfoWjjmtOmcQAt3ost7u6CHKdgHsiVmmG5qilEx1CW5QHJDf9PvS2gUohYYS2S3GU8GwUwpZIm0YOhcF2MxoBEgHM7aXEE0zrFUaz215lY4fXVgGX3FnJecEHUZ80qzR1Y55OWW5gHEBSfrqYAlMn2DcOE6LbkqQbxKG2EcuNc2JK4qCEiGnSkghTn5cUaiRlm2zS8aDA29Ryoqq6kM6VHZ9CL007i9hDTvV47cUycRMWraNTMPeDH0PWWKFHiA0UjF7jEInMh2V8Js8L6ZptKaMPybqu522aOWJFk7Au9kUALaCIqOJRB8g0GpMqullzpqEKlyW9Fmgs9xvqnX6szuCXstNtbqkU8WpmtzkSLPUEg88vv0UbA81L3wnC3II4ezubrRd7kfQZwbi8ErLQ";
                v.ExtendedFields14 = "OfQwbawu9gSXO6JUimJi2cFLiuNMtNO6uneNOvjsVYojlOfB1t0w9dioywcdNcdrro6SHpzltRFW5n7lesViaU7AXfzOlSAeSo5fkFZj1FURohf8Ct2YlGNezlDoLcVGggefYHxqUGkZQYwQFC3zji96NQyn8eP8DNSs8ST3eORDiRHalPHMY543Kc6mlH9D2x3XCE4JV6OXpmARjKb7QWg3wIHFiAzsyeSeLujv8ClIjN6";
                v.ExtendedFields15 = "DmUwA7CCtBzX6SXNPW3qtaX2AOsW3ylj96T52krh11TYUYZNkaVDTV2dxvHlOtNkPfiwHnszKvhB61sdSodugltXoeNJt2wnjiAjIXy1KCZ7W1hY6QIziFqPYUXanw5JpLiCMpBGz1xKNXK6z5nD2Xe7EgMYQAsDO9URgxAkR0uSAYQAeTjrLkMA43tms9yozlWBoVtuW6HPORuaHHfvKwi84rhO4wMfRvHjgIBXfcLba0jPh33fyfOg0bpm7DfVxlmold8Zve1M1KQJjvJ8eRkcUuzumSDSMMUyX3B9Y1DL7joeuWT8ihmZBjPcvXJPjx";
                context.Set<BaseBarCode>().Add(v);
                context.SaveChanges();
            }

            BaseBarCodeApiVM vm = _controller.Wtm.CreateVM<BaseBarCodeApiVM>();
            var oldID = v.ID;
            v = new BaseBarCode();
            v.ID = oldID;
       		
            v.DocNo = "yPPAdUMATB7E0V4YUNAsrHoE";
            v.Code = "FrDHM6A7H6SfMzvRPHcIOQ8XkxLjYe58prQ2U51WnkdNbVE0zpAsh5EDL05AgWO5LXPWeGDG55ceiMKYXLAsx2quteGWI84xzAFWRy5zPUwf0RiEtrZMepHMotswj9CuG1mvt465FhPuouVT0YiBM4YjwPtQsa81SLnqebDE91jTsMHw2ey0tifV4Z3IEvahET41";
            v.Qty = 47;
            v.CustomerCode = "eAZdXQyp0qhZaM8nbQa6n";
            v.CustomerName = "MXCudaE2oGbeliJsPhmp8XipYWNZ36syDL5GpB7";
            v.CustomerNameFirstLetter = "esQgshZFwZlui5xvPCVBcR0Uocv9CitndnAbfKa7UZw4tC2hN8oyfDchY0cbaMwFTc9CuH9YmY9YcVmx2BXu2wlCtFbegcZCbJ";
            v.Seiban = "2pkvIJuLCgE1enSs8JpbTUx6h6miLkdv175";
            v.ExtendedFields1 = "l23E4freC8OyfUljREUplE94jPgzRRsfBTEhFKVCG96SZpZymAlGnmLKkeJ803FwrORHprEojcQnhB0GyVSadXYgGePdEaO5ZyOD5aTsDezdKX6MuZtcYXowmEHfxlAaAM3zzXvp1sz3DDMQsPXYygvxqiDzUf22kQPFMJMnqWokgyELLZ0kyo0VVG7FWPAudUAiaPpQH2IWu5jfyqUeyFbxtAa1aD90VhAKsuZBL";
            v.ExtendedFields2 = "GMfhh8ToUX0Cn5j6Eun2wJoFect7tlvTmEyAY8PhKg9XXXLZjR9dMz2tCA25eZcWBBdGvpMFvsSRx5j2nbN5P7PFebJ7nlAUYDuxWhj1X7xbG5jlk0eDoK4jecqxBudyTCO2Kbkji5tlKf69SHaGaTv15i0FULfDp8sPOCJZIr5q3GXdsYFEl9NIt9jECKcF7jYlAGmUhl1ETiWsEAGgME55ZXkZ1jg6JtJqWA8EMcDKE6ez7CZmzcRFNmijrA6fQlAXqS0gBiLMfLMxiDxSGOnkJeI8s";
            v.ExtendedFields3 = "7s4j6u8RevPtxtN6Xi6RsKMXN1CWX9RjScyE4fssJrYkE41te9GOLeNmbCeBFnetzxbPAtsVJ0iVxSfHy3LKPOTMmbzeyJUARaXq11fXHYBj5wqXUHe8OthBMJJ15wQefiran17VkrBXQ16H9vSqL3q4TKX2J38n2uV5jtpcUn0jG0SpDeCz4CT8J3SCMJSmCHPaqyKQCJ01AkxkWXGJvalueYkYbkknfoupCETNZ9c1tfGcKzkAU0hQTmt15u6MarlSh";
            v.ExtendedFields4 = "QQfjwdTjNekDH1V3Hgz1lj7J5QZ7kxxNbfiZuXKVvJvBRrobgT9rXq632cFH3lpRMczYkHYpAfyJGmEEPLECzzamOWzWYfjx4TEc3PSbtsm72BCiiytZ4RiyUGK3wrIQS1iebBiylT05xdBjm7INNNDd0GQp54xRVkwzCI5ixLTinQhMjyeVjMPQI6ErPPAEtaNwvMStiEguzlE8IHxoSs65i9iYVN77mhIRSG4aMpbAakE7Wv5csDnzrhioKSL3Ok";
            v.ExtendedFields5 = "34UikGqoJireTVujnHOn29cKveBdg0WmATALpiq63y2voDSEWwgLwQHfyEtnOplU3P3Ug2OI6HdTd1FTFxYBXFoNq0inuUk6uvIePjKy0ynUNOdzTHvqbSHCJLw02wKLYswPGYZ2cs17c1cp44znX8zAlImCVgitMYhZs0SwBMVGxJRERKZSdJbh0BKNFus3y8vw8ZpedCpTh1SA0I2PxCmxmybBAf4kw3x3VPZ4LoGAQ8aUuu7JZAn3zLtQXKoeJNCbCVWZ4EDCA";
            v.ExtendedFields6 = "xAgMQt6tihsuB0m4aZ3hLOGj2tA8pDTB0BV2Twq2N9QxEweUCAffar8xq1MLUKTMafBufg1Gp7T4BpAe4DyIge7xFoKQjSJDLKAQx5dpImx82aUOdQFDPcMPQjrXVrvA4qDalzrNzWp6Id34PPuUDsfpwd2ZzsGXrZGno0x4vKR9hHxzSw2CItHMiFRHXoSRMEpWGT2TvIqOU9T9egMqDCPc1ixLbOzyNmVdG3J8L2QrzfU1yZZ6p9aYdgQcmC4F98z6FWTeoI26LbJWBQHhFNifyMvuAFHUp";
            v.ExtendedFields7 = "MDmYssUk3Uv532Jbf3bNFCtQFurlNAeGq9b934WaNWuWfFgcjXW8qWtDXvFt7dOo6Q3YO3Ll4tWuwHJ2silyrZusKhwJG96eNXPrDGfstfLrNOJgaqC2CuKLDOmj8Qh1t2dWPDPOpEyH71LPble1sCD7V8LrKBioqQDcvCWf6lJGMEJofcy2IXHf5Tstl6bRke18dRJ0";
            v.ExtendedFields8 = "KVX1Op8KRoa0JvxxIyl5TgZTPXHnyHx0lERsZCxD6rvA6tcbJzOEkX94SUP64YF4NHubUQw0k3F6o2yo9Wseilzt7649xuVfJOKyH0KYPDkzEt0FxXuhPeFWwS1Mcoi96tplTi1s7EeLbXvMm4330YA2q41B8N4Tia8SlV7AoB0npr6P5PObCQKPqYsvjxE25n920SbXaK9aeKVdX8jEF38H1SREavUi66p3YzUpaAmUsFgIAorrKP79iJJRXNZgz2kXQPATlOSF5qcXHMGqYBnHu3ezV778zdGgSUcTIk6HVJSAUqgS56idb6jUnLONfjhmLSFGINEZoOnlxJiwGAb3RVgcHOdXWAqWAtQ";
            v.ExtendedFields9 = "1yzMXeNSz2lMR5RjpDDbQP6almkDwbrCJc4m27q5MwYeeUbi5XYoXuqfHwKJv81Z3eeTQCBfetj8RwOXRm5Skt0xRdBIsfQQLUQBsn18dInNUm34lN2qQfNgNulAdx1znqh4Pad5bM74YzL0bc2Ltl0rSOFu0BnYxzxKIfDAIGT5U39G2uUFgMlgN3Cw4ai42dIYSf8jAy0dSN5u6bBnuUM9jBt6VuhEI5HFM6lDHbCdUfR9fSYKIFpTnQcVUVOp3bSOsGS0kOjUsG10n6NKvEZe29SW8nHN";
            v.ExtendedFields10 = "2tVoceMeUlJCMDmt";
            v.ExtendedFields11 = "zAXgLX8c4yp42igT4VntFqTTQAwlOFXYsSEQ75fcQFqSoSPZz3XfDyYtMgdfAQ4NshLbRhIrySm2Pe2Or0pSj1aJsFj3LONG49945cV0DpaN2pM669i1sVfLakEVOYIbHvZbU3gr0A8uln7bXj6qCJ2VGGE7sy2ZevDgKEoBjTBfi2lJC3PlNt7ti7ApvXQ6WwLxvmOZwKA9dAq5bHHl8fhvXu78pL7avQLTntj6y0XHdc0VNcHq9j4tgJxV7emVrNFnIpLV4KLVig6J4wodzi37IdFY27do0YBc7oz6Zix745DizuVUf6LfLadQanAZAMfiibIR5lABbdvv986JYH70VdefzhHHQjlNSRGCmUPnyPU29cEKrlrKkesgoqeBiZB7iSMsGQoYLJlo9W1TphFtS7PfhC6BlvED4E3z9rSNemDIbH08ByiQlC4ohfaAtj7ukVem6jtNTCHVBrP77jBBK31GzA9FyA84v";
            v.ExtendedFields12 = "1ff1K2AR42VuVON3SElJJLoJ3dU5srCHYTzjc407Z";
            v.ExtendedFields13 = "H3OpB3vkQjp3aqoRJA9rTmwauI9sqPhfVS8ghZ40j25ux33QFrsl5pCe1x7M0IAA8XXyDuLzNp6ATL2083yTU1P4yFcN1OlMH5dvnfm6QEKcQXhGa7HgbeFT3BfckXFPgDrd1UB2nNZWBTy0owzuDEZNGHe";
            v.ExtendedFields14 = "rWrelaGAvJnociYQf5xf9iRVeXcDFUmo5oWVXTXKUxPGX0WxNc4qVM2ijiluGSfm6";
            v.ExtendedFields15 = "VxcQ8s9ubs7ffm8j7eI0JR2kUed9bLCZRyIdC6XZLld9tsK0SVdeWR7ACBD1XlJwHUj5Y0MfmVhVnPUBKiQQHM3eY2nZHpuD6j6f5TJ65p9NoVQwz5ICgXUbTKSdt0xBa1BygdNc64xc0z7gVyxzZEocycEbseq7xPV4LtUl6z53t2CcRURGryoBDcjapflC0n22dEakTl7ChbWn7";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DocNo", "");
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.ItemId", "");
            vm.FC.Add("Entity.Qty", "");
            vm.FC.Add("Entity.CustomerCode", "");
            vm.FC.Add("Entity.CustomerName", "");
            vm.FC.Add("Entity.CustomerNameFirstLetter", "");
            vm.FC.Add("Entity.Seiban", "");
            vm.FC.Add("Entity.ExtendedFields1", "");
            vm.FC.Add("Entity.ExtendedFields2", "");
            vm.FC.Add("Entity.ExtendedFields3", "");
            vm.FC.Add("Entity.ExtendedFields4", "");
            vm.FC.Add("Entity.ExtendedFields5", "");
            vm.FC.Add("Entity.ExtendedFields6", "");
            vm.FC.Add("Entity.ExtendedFields7", "");
            vm.FC.Add("Entity.ExtendedFields8", "");
            vm.FC.Add("Entity.ExtendedFields9", "");
            vm.FC.Add("Entity.ExtendedFields10", "");
            vm.FC.Add("Entity.ExtendedFields11", "");
            vm.FC.Add("Entity.ExtendedFields12", "");
            vm.FC.Add("Entity.ExtendedFields13", "");
            vm.FC.Add("Entity.ExtendedFields14", "");
            vm.FC.Add("Entity.ExtendedFields15", "");
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<BaseBarCode>().Find(v.ID);
 				
                Assert.AreEqual(data.DocNo, "yPPAdUMATB7E0V4YUNAsrHoE");
                Assert.AreEqual(data.Code, "FrDHM6A7H6SfMzvRPHcIOQ8XkxLjYe58prQ2U51WnkdNbVE0zpAsh5EDL05AgWO5LXPWeGDG55ceiMKYXLAsx2quteGWI84xzAFWRy5zPUwf0RiEtrZMepHMotswj9CuG1mvt465FhPuouVT0YiBM4YjwPtQsa81SLnqebDE91jTsMHw2ey0tifV4Z3IEvahET41");
                Assert.AreEqual(data.Qty, 47);
                Assert.AreEqual(data.CustomerCode, "eAZdXQyp0qhZaM8nbQa6n");
                Assert.AreEqual(data.CustomerName, "MXCudaE2oGbeliJsPhmp8XipYWNZ36syDL5GpB7");
                Assert.AreEqual(data.CustomerNameFirstLetter, "esQgshZFwZlui5xvPCVBcR0Uocv9CitndnAbfKa7UZw4tC2hN8oyfDchY0cbaMwFTc9CuH9YmY9YcVmx2BXu2wlCtFbegcZCbJ");
                Assert.AreEqual(data.Seiban, "2pkvIJuLCgE1enSs8JpbTUx6h6miLkdv175");
                Assert.AreEqual(data.ExtendedFields1, "l23E4freC8OyfUljREUplE94jPgzRRsfBTEhFKVCG96SZpZymAlGnmLKkeJ803FwrORHprEojcQnhB0GyVSadXYgGePdEaO5ZyOD5aTsDezdKX6MuZtcYXowmEHfxlAaAM3zzXvp1sz3DDMQsPXYygvxqiDzUf22kQPFMJMnqWokgyELLZ0kyo0VVG7FWPAudUAiaPpQH2IWu5jfyqUeyFbxtAa1aD90VhAKsuZBL");
                Assert.AreEqual(data.ExtendedFields2, "GMfhh8ToUX0Cn5j6Eun2wJoFect7tlvTmEyAY8PhKg9XXXLZjR9dMz2tCA25eZcWBBdGvpMFvsSRx5j2nbN5P7PFebJ7nlAUYDuxWhj1X7xbG5jlk0eDoK4jecqxBudyTCO2Kbkji5tlKf69SHaGaTv15i0FULfDp8sPOCJZIr5q3GXdsYFEl9NIt9jECKcF7jYlAGmUhl1ETiWsEAGgME55ZXkZ1jg6JtJqWA8EMcDKE6ez7CZmzcRFNmijrA6fQlAXqS0gBiLMfLMxiDxSGOnkJeI8s");
                Assert.AreEqual(data.ExtendedFields3, "7s4j6u8RevPtxtN6Xi6RsKMXN1CWX9RjScyE4fssJrYkE41te9GOLeNmbCeBFnetzxbPAtsVJ0iVxSfHy3LKPOTMmbzeyJUARaXq11fXHYBj5wqXUHe8OthBMJJ15wQefiran17VkrBXQ16H9vSqL3q4TKX2J38n2uV5jtpcUn0jG0SpDeCz4CT8J3SCMJSmCHPaqyKQCJ01AkxkWXGJvalueYkYbkknfoupCETNZ9c1tfGcKzkAU0hQTmt15u6MarlSh");
                Assert.AreEqual(data.ExtendedFields4, "QQfjwdTjNekDH1V3Hgz1lj7J5QZ7kxxNbfiZuXKVvJvBRrobgT9rXq632cFH3lpRMczYkHYpAfyJGmEEPLECzzamOWzWYfjx4TEc3PSbtsm72BCiiytZ4RiyUGK3wrIQS1iebBiylT05xdBjm7INNNDd0GQp54xRVkwzCI5ixLTinQhMjyeVjMPQI6ErPPAEtaNwvMStiEguzlE8IHxoSs65i9iYVN77mhIRSG4aMpbAakE7Wv5csDnzrhioKSL3Ok");
                Assert.AreEqual(data.ExtendedFields5, "34UikGqoJireTVujnHOn29cKveBdg0WmATALpiq63y2voDSEWwgLwQHfyEtnOplU3P3Ug2OI6HdTd1FTFxYBXFoNq0inuUk6uvIePjKy0ynUNOdzTHvqbSHCJLw02wKLYswPGYZ2cs17c1cp44znX8zAlImCVgitMYhZs0SwBMVGxJRERKZSdJbh0BKNFus3y8vw8ZpedCpTh1SA0I2PxCmxmybBAf4kw3x3VPZ4LoGAQ8aUuu7JZAn3zLtQXKoeJNCbCVWZ4EDCA");
                Assert.AreEqual(data.ExtendedFields6, "xAgMQt6tihsuB0m4aZ3hLOGj2tA8pDTB0BV2Twq2N9QxEweUCAffar8xq1MLUKTMafBufg1Gp7T4BpAe4DyIge7xFoKQjSJDLKAQx5dpImx82aUOdQFDPcMPQjrXVrvA4qDalzrNzWp6Id34PPuUDsfpwd2ZzsGXrZGno0x4vKR9hHxzSw2CItHMiFRHXoSRMEpWGT2TvIqOU9T9egMqDCPc1ixLbOzyNmVdG3J8L2QrzfU1yZZ6p9aYdgQcmC4F98z6FWTeoI26LbJWBQHhFNifyMvuAFHUp");
                Assert.AreEqual(data.ExtendedFields7, "MDmYssUk3Uv532Jbf3bNFCtQFurlNAeGq9b934WaNWuWfFgcjXW8qWtDXvFt7dOo6Q3YO3Ll4tWuwHJ2silyrZusKhwJG96eNXPrDGfstfLrNOJgaqC2CuKLDOmj8Qh1t2dWPDPOpEyH71LPble1sCD7V8LrKBioqQDcvCWf6lJGMEJofcy2IXHf5Tstl6bRke18dRJ0");
                Assert.AreEqual(data.ExtendedFields8, "KVX1Op8KRoa0JvxxIyl5TgZTPXHnyHx0lERsZCxD6rvA6tcbJzOEkX94SUP64YF4NHubUQw0k3F6o2yo9Wseilzt7649xuVfJOKyH0KYPDkzEt0FxXuhPeFWwS1Mcoi96tplTi1s7EeLbXvMm4330YA2q41B8N4Tia8SlV7AoB0npr6P5PObCQKPqYsvjxE25n920SbXaK9aeKVdX8jEF38H1SREavUi66p3YzUpaAmUsFgIAorrKP79iJJRXNZgz2kXQPATlOSF5qcXHMGqYBnHu3ezV778zdGgSUcTIk6HVJSAUqgS56idb6jUnLONfjhmLSFGINEZoOnlxJiwGAb3RVgcHOdXWAqWAtQ");
                Assert.AreEqual(data.ExtendedFields9, "1yzMXeNSz2lMR5RjpDDbQP6almkDwbrCJc4m27q5MwYeeUbi5XYoXuqfHwKJv81Z3eeTQCBfetj8RwOXRm5Skt0xRdBIsfQQLUQBsn18dInNUm34lN2qQfNgNulAdx1znqh4Pad5bM74YzL0bc2Ltl0rSOFu0BnYxzxKIfDAIGT5U39G2uUFgMlgN3Cw4ai42dIYSf8jAy0dSN5u6bBnuUM9jBt6VuhEI5HFM6lDHbCdUfR9fSYKIFpTnQcVUVOp3bSOsGS0kOjUsG10n6NKvEZe29SW8nHN");
                Assert.AreEqual(data.ExtendedFields10, "2tVoceMeUlJCMDmt");
                Assert.AreEqual(data.ExtendedFields11, "zAXgLX8c4yp42igT4VntFqTTQAwlOFXYsSEQ75fcQFqSoSPZz3XfDyYtMgdfAQ4NshLbRhIrySm2Pe2Or0pSj1aJsFj3LONG49945cV0DpaN2pM669i1sVfLakEVOYIbHvZbU3gr0A8uln7bXj6qCJ2VGGE7sy2ZevDgKEoBjTBfi2lJC3PlNt7ti7ApvXQ6WwLxvmOZwKA9dAq5bHHl8fhvXu78pL7avQLTntj6y0XHdc0VNcHq9j4tgJxV7emVrNFnIpLV4KLVig6J4wodzi37IdFY27do0YBc7oz6Zix745DizuVUf6LfLadQanAZAMfiibIR5lABbdvv986JYH70VdefzhHHQjlNSRGCmUPnyPU29cEKrlrKkesgoqeBiZB7iSMsGQoYLJlo9W1TphFtS7PfhC6BlvED4E3z9rSNemDIbH08ByiQlC4ohfaAtj7ukVem6jtNTCHVBrP77jBBK31GzA9FyA84v");
                Assert.AreEqual(data.ExtendedFields12, "1ff1K2AR42VuVON3SElJJLoJ3dU5srCHYTzjc407Z");
                Assert.AreEqual(data.ExtendedFields13, "H3OpB3vkQjp3aqoRJA9rTmwauI9sqPhfVS8ghZ40j25ux33QFrsl5pCe1x7M0IAA8XXyDuLzNp6ATL2083yTU1P4yFcN1OlMH5dvnfm6QEKcQXhGa7HgbeFT3BfckXFPgDrd1UB2nNZWBTy0owzuDEZNGHe");
                Assert.AreEqual(data.ExtendedFields14, "rWrelaGAvJnociYQf5xf9iRVeXcDFUmo5oWVXTXKUxPGX0WxNc4qVM2ijiluGSfm6");
                Assert.AreEqual(data.ExtendedFields15, "VxcQ8s9ubs7ffm8j7eI0JR2kUed9bLCZRyIdC6XZLld9tsK0SVdeWR7ACBD1XlJwHUj5Y0MfmVhVnPUBKiQQHM3eY2nZHpuD6j6f5TJ65p9NoVQwz5ICgXUbTKSdt0xBa1BygdNc64xc0z7gVyxzZEocycEbseq7xPV4LtUl6z53t2CcRURGryoBDcjapflC0n22dEakTl7ChbWn7");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            BaseBarCode v = new BaseBarCode();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DocNo = "kjnOcKfMnr6sIts5Ca";
                v.Code = "LXvgz2IhCjIoXJlN5SFr7DAARgp8OIjip5cF6VHLhVqbaqzBMMmp1n88d65PFHX9JiqXqn2YkRVZGLxHIqIsWQVPvuJd87yvzjkW1c3j6k5zhvFpr0QwKEYFMgAlEqNP3Ub4KQk3J2f9rZAgegUaEQ1zqawWcmgFnrEAL9mVClfETJ6cVjbdnGlgLO7tvebRwodHjS";
                v.ItemId = AddBaseItemMaster();
                v.Qty = 23;
                v.CustomerCode = "XewmZ8zpO9LTdVPgBf71";
                v.CustomerName = "ykDTRyioz6OyEmd8OyuQWodusuholOzn8CR7FC9SxGC3I9vY2ftHPjPVcJ6vPsavuZE6pbOROSQLvzEG04XScFV";
                v.CustomerNameFirstLetter = "RRBHynB8DBh3khG4LqspG1";
                v.Seiban = "kxzxGsmPOLlqqNG0LrrkBEQ6xHhJcSkBMTYvgoXkQTru212gn";
                v.ExtendedFields1 = "6S9ZZ303H5ek7zq96fkTX0PgCJCdAYYYG9gLnCTX8snKryt6X7T6OPQHm2A9PGnLLb5Ju01cEeCbNxxp3UsyVFwDl623TMIbIrksDCNk4codsGSLMXkcMOG1fSiWWr2keUS65ZUwlB02YeONDc1i0Zdm56gtisJU4YXSGjmZg3RwcW80icKfOrX8RrVTmJu9EXpyUS4vnd5MCMK5bypJ6YAL4fHNq0vDMDxXtpeOpi6w6E6qvxAffR";
                v.ExtendedFields2 = "8dpce8Fx6A2R0IZ1cLZhP1WNlSoFm0d3jYz98s5DOJWhEKIJomTgznC9MZfN6S0IwrXJWaf3ieibos1J5vL7KMMFTtUW6A5mZ7geRObq5hkkOrMXE3q7Td9GCQA8xZIHkc7xYmShdizuGuBRV9WDzxoSbSZAKvj39iNKF2JfUlpZFyiU9uaSuglCc5MUUtJkbjUmKtUnmbjyiTX57lEKzbkBh6mMiHoTtnd4qaMYNGcTaJlkcqFKchV19FLWAgaUGj9Z9JjiMTwGdkXPQfP";
                v.ExtendedFields3 = "HHaNaQ7vjTkcmYsiTf7IgHqqdVnwm1L8NfHE5yp7Q0v0RF0b9r0RtaB3NF2ipejrZG7pYEFr2LOycpwmgmeq9lEFeHZMTyJXDcPFfdtW7slpVAljGio71gao5wiZqTr3b7V5oEPRZp206y6GNgRdzgT1T1rkL5SKKBYVJLJpmaXujp5KH6iMLfYJdBDHpkRKrhUibEN0sm2yallzDCdSrgzRRYsyT81dNt0L3PfWgPyU2bMMAcEu09rgrQckXHQltpfEF1QzkpDYlMyZzKTlBZqJ5YXVSNZoIpi7R93gtURN2E0D1f07CnqEBg1sInbOFzkUNW52q0LuAmiUNIpQJixQWb5yCLbuCpNqjZJgvNFGKVqFV0XvrwApzQGD8pwigy3HShBXjD1iivatlOxo20T4cRHc14";
                v.ExtendedFields4 = "KLFZVLEJnz7XOVgxHtTxQ4o53SqkxUjnHq9";
                v.ExtendedFields5 = "iC5WV8zwoUOcYIZSc3wAY8YEiotuImwnXpXH0mHPbhA0M";
                v.ExtendedFields6 = "5YxT6ELUZfmgK1rEq0y1";
                v.ExtendedFields7 = "2AMTn5q0rDokUhHi6NJj66q8c94CtxVa8DOOUDmqKCGzCEqBFGjoc8cepEOe2ipoKGDEj56TPPrutATnDCXzE3CnlJi0";
                v.ExtendedFields8 = "mFA50t1nE3zwQ4AzWMZb2EGWw5TFtRYp7i6sVZMfocPuIoT7yfIjFDjIFnbxPviiwnNwW9InP4d6k9nULgMoqccHVyeFGZFXYA1OIytkXJ07UgMR5jHHW56WdqWnp3FYSe7rX0WeykQuSSaf9DniOyUm4TEhSuF6OYKRGNAwwwW9996Iub3fiunQRN7KUmSwzxfD4EP5rDup1oKoPtTFekNylkPu9RmB1eO96iwg8nemA6aT3BSf0LwC4rGyA9o3YXEUPfj9R1Hphaw0pcxUX3rZpWv3DoWJs2xITXjrQuOlIckRe9EBKJL81CJMf30hN7gd1JoqgItPMuw5Cf3AogIG8drh1hulTEZdcntAX0z20dSoesGgLBDcoQrgJDaeRrgu8B3rIq";
                v.ExtendedFields9 = "ntfV88GMg2oW4WW2JvBEKxO8d6cnCrWHwS4r4RTz7NcVHz5IyqKYlgc60GSfCsYx2dYRhMMMq4ZliM5jK4xbVX9QiKc3QTzRPu9jq6VUzYoTixLSp3PIP6intQdmK8";
                v.ExtendedFields10 = "WWjf9hHEz0G8PQDRZN7tQcQH9Ce09kSJ0mXfjcwwEBApsyJqnPaa1fN1P1UxEs8hbC39SwV3wrW6ZADxmPN96MnXTBunJz2hvispM7c1dKf8AcgrGMsigZtCgv2GLm9YbdyVzeYgYIO5H3mlO0CtO46iMtU3pWdL4RjuhTryyVoTclUUjVAaU91K56bJx6RGHaxpwglpFGUpzLE2lTvfyqGq1ggMQS9SqT9agTq7SyEU1afTJWYevjmhHUIF9y54O3wFNxIbvwuxz0aOZaKD8wZO6eQb6tzybMQxwzLnzMaIFtRZ3oIYxxUOaHoFI2elyPxwzmY9BvJJH9JslQ2s2DZFvQo3IcSvMJZTY0SUHDXPOtNGlcdZDQ7bgP47NHUQUdkSZJzxQ7HjOxTOX4i6xyU8Yx93uwGTOsGNpifJdbJ6B4A3QQSf6CBrRcd7HYusIWrLejxS7BBuzm";
                v.ExtendedFields11 = "nnjTji7vDeNSVKiADX4GqwFlkRseUbqCgj1KIYnmLiAHEwPrPKA5s5lO9K2FzItLk5MS0WiuZHivOiIvT6p4zXrJzmWmQqyR243SExw3zKDbzzTz5KfhkQMUwHMZ6GCD1Od0TMTFP2meElNo3qeUHMhBf9WYgfSW31pYOHuX1FkUq063wcjG2iDRgMnSBcvVCgrHREwC6KtGMQalh02KhBs7COdpAaZN6SJbkbKsfXaUWI57qaQcV0ZYWIh3Ym3x5t7wSPGcPXG2Fr5VCk3TDYBOtH689s71D2NZT2YB8IVLHGHXEu62fsmHXd396i1MpkaTAwdtbGvJcrJhUgLAy28gF0Xzq8PiLTYzmcw03PE1Rw7oyPHDepsxM";
                v.ExtendedFields12 = "kSuOvV6hwa97ar0xKb3YYFC1hw0eO34OlciZOIzYjyntHLl6unVETPzLGOCA7hUknnCqLzolwtuy4IifwpPZWEjGrkAoicIwJO6isU43yJSowJQsyOsPs72SozWpIqNc8tMtfZ4Vqzr7JH1Ijo6Vpxmfl6k6HSmszjQPrgP6D3KWRWPJ6TSwgb1lCuFEExI9N7Tgab";
                v.ExtendedFields13 = "MRoTxcnRWa4CXfoWjjmtOmcQAt3ost7u6CHKdgHsiVmmG5qilEx1CW5QHJDf9PvS2gUohYYS2S3GU8GwUwpZIm0YOhcF2MxoBEgHM7aXEE0zrFUaz215lY4fXVgGX3FnJecEHUZ80qzR1Y55OWW5gHEBSfrqYAlMn2DcOE6LbkqQbxKG2EcuNc2JK4qCEiGnSkghTn5cUaiRlm2zS8aDA29Ryoqq6kM6VHZ9CL007i9hDTvV47cUycRMWraNTMPeDH0PWWKFHiA0UjF7jEInMh2V8Js8L6ZptKaMPybqu522aOWJFk7Au9kUALaCIqOJRB8g0GpMqullzpqEKlyW9Fmgs9xvqnX6szuCXstNtbqkU8WpmtzkSLPUEg88vv0UbA81L3wnC3II4ezubrRd7kfQZwbi8ErLQ";
                v.ExtendedFields14 = "OfQwbawu9gSXO6JUimJi2cFLiuNMtNO6uneNOvjsVYojlOfB1t0w9dioywcdNcdrro6SHpzltRFW5n7lesViaU7AXfzOlSAeSo5fkFZj1FURohf8Ct2YlGNezlDoLcVGggefYHxqUGkZQYwQFC3zji96NQyn8eP8DNSs8ST3eORDiRHalPHMY543Kc6mlH9D2x3XCE4JV6OXpmARjKb7QWg3wIHFiAzsyeSeLujv8ClIjN6";
                v.ExtendedFields15 = "DmUwA7CCtBzX6SXNPW3qtaX2AOsW3ylj96T52krh11TYUYZNkaVDTV2dxvHlOtNkPfiwHnszKvhB61sdSodugltXoeNJt2wnjiAjIXy1KCZ7W1hY6QIziFqPYUXanw5JpLiCMpBGz1xKNXK6z5nD2Xe7EgMYQAsDO9URgxAkR0uSAYQAeTjrLkMA43tms9yozlWBoVtuW6HPORuaHHfvKwi84rhO4wMfRvHjgIBXfcLba0jPh33fyfOg0bpm7DfVxlmold8Zve1M1KQJjvJ8eRkcUuzumSDSMMUyX3B9Y1DL7joeuWT8ihmZBjPcvXJPjx";
                context.Set<BaseBarCode>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            BaseBarCode v1 = new BaseBarCode();
            BaseBarCode v2 = new BaseBarCode();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DocNo = "kjnOcKfMnr6sIts5Ca";
                v1.Code = "LXvgz2IhCjIoXJlN5SFr7DAARgp8OIjip5cF6VHLhVqbaqzBMMmp1n88d65PFHX9JiqXqn2YkRVZGLxHIqIsWQVPvuJd87yvzjkW1c3j6k5zhvFpr0QwKEYFMgAlEqNP3Ub4KQk3J2f9rZAgegUaEQ1zqawWcmgFnrEAL9mVClfETJ6cVjbdnGlgLO7tvebRwodHjS";
                v1.ItemId = AddBaseItemMaster();
                v1.Qty = 23;
                v1.CustomerCode = "XewmZ8zpO9LTdVPgBf71";
                v1.CustomerName = "ykDTRyioz6OyEmd8OyuQWodusuholOzn8CR7FC9SxGC3I9vY2ftHPjPVcJ6vPsavuZE6pbOROSQLvzEG04XScFV";
                v1.CustomerNameFirstLetter = "RRBHynB8DBh3khG4LqspG1";
                v1.Seiban = "kxzxGsmPOLlqqNG0LrrkBEQ6xHhJcSkBMTYvgoXkQTru212gn";
                v1.ExtendedFields1 = "6S9ZZ303H5ek7zq96fkTX0PgCJCdAYYYG9gLnCTX8snKryt6X7T6OPQHm2A9PGnLLb5Ju01cEeCbNxxp3UsyVFwDl623TMIbIrksDCNk4codsGSLMXkcMOG1fSiWWr2keUS65ZUwlB02YeONDc1i0Zdm56gtisJU4YXSGjmZg3RwcW80icKfOrX8RrVTmJu9EXpyUS4vnd5MCMK5bypJ6YAL4fHNq0vDMDxXtpeOpi6w6E6qvxAffR";
                v1.ExtendedFields2 = "8dpce8Fx6A2R0IZ1cLZhP1WNlSoFm0d3jYz98s5DOJWhEKIJomTgznC9MZfN6S0IwrXJWaf3ieibos1J5vL7KMMFTtUW6A5mZ7geRObq5hkkOrMXE3q7Td9GCQA8xZIHkc7xYmShdizuGuBRV9WDzxoSbSZAKvj39iNKF2JfUlpZFyiU9uaSuglCc5MUUtJkbjUmKtUnmbjyiTX57lEKzbkBh6mMiHoTtnd4qaMYNGcTaJlkcqFKchV19FLWAgaUGj9Z9JjiMTwGdkXPQfP";
                v1.ExtendedFields3 = "HHaNaQ7vjTkcmYsiTf7IgHqqdVnwm1L8NfHE5yp7Q0v0RF0b9r0RtaB3NF2ipejrZG7pYEFr2LOycpwmgmeq9lEFeHZMTyJXDcPFfdtW7slpVAljGio71gao5wiZqTr3b7V5oEPRZp206y6GNgRdzgT1T1rkL5SKKBYVJLJpmaXujp5KH6iMLfYJdBDHpkRKrhUibEN0sm2yallzDCdSrgzRRYsyT81dNt0L3PfWgPyU2bMMAcEu09rgrQckXHQltpfEF1QzkpDYlMyZzKTlBZqJ5YXVSNZoIpi7R93gtURN2E0D1f07CnqEBg1sInbOFzkUNW52q0LuAmiUNIpQJixQWb5yCLbuCpNqjZJgvNFGKVqFV0XvrwApzQGD8pwigy3HShBXjD1iivatlOxo20T4cRHc14";
                v1.ExtendedFields4 = "KLFZVLEJnz7XOVgxHtTxQ4o53SqkxUjnHq9";
                v1.ExtendedFields5 = "iC5WV8zwoUOcYIZSc3wAY8YEiotuImwnXpXH0mHPbhA0M";
                v1.ExtendedFields6 = "5YxT6ELUZfmgK1rEq0y1";
                v1.ExtendedFields7 = "2AMTn5q0rDokUhHi6NJj66q8c94CtxVa8DOOUDmqKCGzCEqBFGjoc8cepEOe2ipoKGDEj56TPPrutATnDCXzE3CnlJi0";
                v1.ExtendedFields8 = "mFA50t1nE3zwQ4AzWMZb2EGWw5TFtRYp7i6sVZMfocPuIoT7yfIjFDjIFnbxPviiwnNwW9InP4d6k9nULgMoqccHVyeFGZFXYA1OIytkXJ07UgMR5jHHW56WdqWnp3FYSe7rX0WeykQuSSaf9DniOyUm4TEhSuF6OYKRGNAwwwW9996Iub3fiunQRN7KUmSwzxfD4EP5rDup1oKoPtTFekNylkPu9RmB1eO96iwg8nemA6aT3BSf0LwC4rGyA9o3YXEUPfj9R1Hphaw0pcxUX3rZpWv3DoWJs2xITXjrQuOlIckRe9EBKJL81CJMf30hN7gd1JoqgItPMuw5Cf3AogIG8drh1hulTEZdcntAX0z20dSoesGgLBDcoQrgJDaeRrgu8B3rIq";
                v1.ExtendedFields9 = "ntfV88GMg2oW4WW2JvBEKxO8d6cnCrWHwS4r4RTz7NcVHz5IyqKYlgc60GSfCsYx2dYRhMMMq4ZliM5jK4xbVX9QiKc3QTzRPu9jq6VUzYoTixLSp3PIP6intQdmK8";
                v1.ExtendedFields10 = "WWjf9hHEz0G8PQDRZN7tQcQH9Ce09kSJ0mXfjcwwEBApsyJqnPaa1fN1P1UxEs8hbC39SwV3wrW6ZADxmPN96MnXTBunJz2hvispM7c1dKf8AcgrGMsigZtCgv2GLm9YbdyVzeYgYIO5H3mlO0CtO46iMtU3pWdL4RjuhTryyVoTclUUjVAaU91K56bJx6RGHaxpwglpFGUpzLE2lTvfyqGq1ggMQS9SqT9agTq7SyEU1afTJWYevjmhHUIF9y54O3wFNxIbvwuxz0aOZaKD8wZO6eQb6tzybMQxwzLnzMaIFtRZ3oIYxxUOaHoFI2elyPxwzmY9BvJJH9JslQ2s2DZFvQo3IcSvMJZTY0SUHDXPOtNGlcdZDQ7bgP47NHUQUdkSZJzxQ7HjOxTOX4i6xyU8Yx93uwGTOsGNpifJdbJ6B4A3QQSf6CBrRcd7HYusIWrLejxS7BBuzm";
                v1.ExtendedFields11 = "nnjTji7vDeNSVKiADX4GqwFlkRseUbqCgj1KIYnmLiAHEwPrPKA5s5lO9K2FzItLk5MS0WiuZHivOiIvT6p4zXrJzmWmQqyR243SExw3zKDbzzTz5KfhkQMUwHMZ6GCD1Od0TMTFP2meElNo3qeUHMhBf9WYgfSW31pYOHuX1FkUq063wcjG2iDRgMnSBcvVCgrHREwC6KtGMQalh02KhBs7COdpAaZN6SJbkbKsfXaUWI57qaQcV0ZYWIh3Ym3x5t7wSPGcPXG2Fr5VCk3TDYBOtH689s71D2NZT2YB8IVLHGHXEu62fsmHXd396i1MpkaTAwdtbGvJcrJhUgLAy28gF0Xzq8PiLTYzmcw03PE1Rw7oyPHDepsxM";
                v1.ExtendedFields12 = "kSuOvV6hwa97ar0xKb3YYFC1hw0eO34OlciZOIzYjyntHLl6unVETPzLGOCA7hUknnCqLzolwtuy4IifwpPZWEjGrkAoicIwJO6isU43yJSowJQsyOsPs72SozWpIqNc8tMtfZ4Vqzr7JH1Ijo6Vpxmfl6k6HSmszjQPrgP6D3KWRWPJ6TSwgb1lCuFEExI9N7Tgab";
                v1.ExtendedFields13 = "MRoTxcnRWa4CXfoWjjmtOmcQAt3ost7u6CHKdgHsiVmmG5qilEx1CW5QHJDf9PvS2gUohYYS2S3GU8GwUwpZIm0YOhcF2MxoBEgHM7aXEE0zrFUaz215lY4fXVgGX3FnJecEHUZ80qzR1Y55OWW5gHEBSfrqYAlMn2DcOE6LbkqQbxKG2EcuNc2JK4qCEiGnSkghTn5cUaiRlm2zS8aDA29Ryoqq6kM6VHZ9CL007i9hDTvV47cUycRMWraNTMPeDH0PWWKFHiA0UjF7jEInMh2V8Js8L6ZptKaMPybqu522aOWJFk7Au9kUALaCIqOJRB8g0GpMqullzpqEKlyW9Fmgs9xvqnX6szuCXstNtbqkU8WpmtzkSLPUEg88vv0UbA81L3wnC3II4ezubrRd7kfQZwbi8ErLQ";
                v1.ExtendedFields14 = "OfQwbawu9gSXO6JUimJi2cFLiuNMtNO6uneNOvjsVYojlOfB1t0w9dioywcdNcdrro6SHpzltRFW5n7lesViaU7AXfzOlSAeSo5fkFZj1FURohf8Ct2YlGNezlDoLcVGggefYHxqUGkZQYwQFC3zji96NQyn8eP8DNSs8ST3eORDiRHalPHMY543Kc6mlH9D2x3XCE4JV6OXpmARjKb7QWg3wIHFiAzsyeSeLujv8ClIjN6";
                v1.ExtendedFields15 = "DmUwA7CCtBzX6SXNPW3qtaX2AOsW3ylj96T52krh11TYUYZNkaVDTV2dxvHlOtNkPfiwHnszKvhB61sdSodugltXoeNJt2wnjiAjIXy1KCZ7W1hY6QIziFqPYUXanw5JpLiCMpBGz1xKNXK6z5nD2Xe7EgMYQAsDO9URgxAkR0uSAYQAeTjrLkMA43tms9yozlWBoVtuW6HPORuaHHfvKwi84rhO4wMfRvHjgIBXfcLba0jPh33fyfOg0bpm7DfVxlmold8Zve1M1KQJjvJ8eRkcUuzumSDSMMUyX3B9Y1DL7joeuWT8ihmZBjPcvXJPjx";
                v2.DocNo = "yPPAdUMATB7E0V4YUNAsrHoE";
                v2.Code = "FrDHM6A7H6SfMzvRPHcIOQ8XkxLjYe58prQ2U51WnkdNbVE0zpAsh5EDL05AgWO5LXPWeGDG55ceiMKYXLAsx2quteGWI84xzAFWRy5zPUwf0RiEtrZMepHMotswj9CuG1mvt465FhPuouVT0YiBM4YjwPtQsa81SLnqebDE91jTsMHw2ey0tifV4Z3IEvahET41";
                v2.ItemId = v1.ItemId; 
                v2.Qty = 47;
                v2.CustomerCode = "eAZdXQyp0qhZaM8nbQa6n";
                v2.CustomerName = "MXCudaE2oGbeliJsPhmp8XipYWNZ36syDL5GpB7";
                v2.CustomerNameFirstLetter = "esQgshZFwZlui5xvPCVBcR0Uocv9CitndnAbfKa7UZw4tC2hN8oyfDchY0cbaMwFTc9CuH9YmY9YcVmx2BXu2wlCtFbegcZCbJ";
                v2.Seiban = "2pkvIJuLCgE1enSs8JpbTUx6h6miLkdv175";
                v2.ExtendedFields1 = "l23E4freC8OyfUljREUplE94jPgzRRsfBTEhFKVCG96SZpZymAlGnmLKkeJ803FwrORHprEojcQnhB0GyVSadXYgGePdEaO5ZyOD5aTsDezdKX6MuZtcYXowmEHfxlAaAM3zzXvp1sz3DDMQsPXYygvxqiDzUf22kQPFMJMnqWokgyELLZ0kyo0VVG7FWPAudUAiaPpQH2IWu5jfyqUeyFbxtAa1aD90VhAKsuZBL";
                v2.ExtendedFields2 = "GMfhh8ToUX0Cn5j6Eun2wJoFect7tlvTmEyAY8PhKg9XXXLZjR9dMz2tCA25eZcWBBdGvpMFvsSRx5j2nbN5P7PFebJ7nlAUYDuxWhj1X7xbG5jlk0eDoK4jecqxBudyTCO2Kbkji5tlKf69SHaGaTv15i0FULfDp8sPOCJZIr5q3GXdsYFEl9NIt9jECKcF7jYlAGmUhl1ETiWsEAGgME55ZXkZ1jg6JtJqWA8EMcDKE6ez7CZmzcRFNmijrA6fQlAXqS0gBiLMfLMxiDxSGOnkJeI8s";
                v2.ExtendedFields3 = "7s4j6u8RevPtxtN6Xi6RsKMXN1CWX9RjScyE4fssJrYkE41te9GOLeNmbCeBFnetzxbPAtsVJ0iVxSfHy3LKPOTMmbzeyJUARaXq11fXHYBj5wqXUHe8OthBMJJ15wQefiran17VkrBXQ16H9vSqL3q4TKX2J38n2uV5jtpcUn0jG0SpDeCz4CT8J3SCMJSmCHPaqyKQCJ01AkxkWXGJvalueYkYbkknfoupCETNZ9c1tfGcKzkAU0hQTmt15u6MarlSh";
                v2.ExtendedFields4 = "QQfjwdTjNekDH1V3Hgz1lj7J5QZ7kxxNbfiZuXKVvJvBRrobgT9rXq632cFH3lpRMczYkHYpAfyJGmEEPLECzzamOWzWYfjx4TEc3PSbtsm72BCiiytZ4RiyUGK3wrIQS1iebBiylT05xdBjm7INNNDd0GQp54xRVkwzCI5ixLTinQhMjyeVjMPQI6ErPPAEtaNwvMStiEguzlE8IHxoSs65i9iYVN77mhIRSG4aMpbAakE7Wv5csDnzrhioKSL3Ok";
                v2.ExtendedFields5 = "34UikGqoJireTVujnHOn29cKveBdg0WmATALpiq63y2voDSEWwgLwQHfyEtnOplU3P3Ug2OI6HdTd1FTFxYBXFoNq0inuUk6uvIePjKy0ynUNOdzTHvqbSHCJLw02wKLYswPGYZ2cs17c1cp44znX8zAlImCVgitMYhZs0SwBMVGxJRERKZSdJbh0BKNFus3y8vw8ZpedCpTh1SA0I2PxCmxmybBAf4kw3x3VPZ4LoGAQ8aUuu7JZAn3zLtQXKoeJNCbCVWZ4EDCA";
                v2.ExtendedFields6 = "xAgMQt6tihsuB0m4aZ3hLOGj2tA8pDTB0BV2Twq2N9QxEweUCAffar8xq1MLUKTMafBufg1Gp7T4BpAe4DyIge7xFoKQjSJDLKAQx5dpImx82aUOdQFDPcMPQjrXVrvA4qDalzrNzWp6Id34PPuUDsfpwd2ZzsGXrZGno0x4vKR9hHxzSw2CItHMiFRHXoSRMEpWGT2TvIqOU9T9egMqDCPc1ixLbOzyNmVdG3J8L2QrzfU1yZZ6p9aYdgQcmC4F98z6FWTeoI26LbJWBQHhFNifyMvuAFHUp";
                v2.ExtendedFields7 = "MDmYssUk3Uv532Jbf3bNFCtQFurlNAeGq9b934WaNWuWfFgcjXW8qWtDXvFt7dOo6Q3YO3Ll4tWuwHJ2silyrZusKhwJG96eNXPrDGfstfLrNOJgaqC2CuKLDOmj8Qh1t2dWPDPOpEyH71LPble1sCD7V8LrKBioqQDcvCWf6lJGMEJofcy2IXHf5Tstl6bRke18dRJ0";
                v2.ExtendedFields8 = "KVX1Op8KRoa0JvxxIyl5TgZTPXHnyHx0lERsZCxD6rvA6tcbJzOEkX94SUP64YF4NHubUQw0k3F6o2yo9Wseilzt7649xuVfJOKyH0KYPDkzEt0FxXuhPeFWwS1Mcoi96tplTi1s7EeLbXvMm4330YA2q41B8N4Tia8SlV7AoB0npr6P5PObCQKPqYsvjxE25n920SbXaK9aeKVdX8jEF38H1SREavUi66p3YzUpaAmUsFgIAorrKP79iJJRXNZgz2kXQPATlOSF5qcXHMGqYBnHu3ezV778zdGgSUcTIk6HVJSAUqgS56idb6jUnLONfjhmLSFGINEZoOnlxJiwGAb3RVgcHOdXWAqWAtQ";
                v2.ExtendedFields9 = "1yzMXeNSz2lMR5RjpDDbQP6almkDwbrCJc4m27q5MwYeeUbi5XYoXuqfHwKJv81Z3eeTQCBfetj8RwOXRm5Skt0xRdBIsfQQLUQBsn18dInNUm34lN2qQfNgNulAdx1znqh4Pad5bM74YzL0bc2Ltl0rSOFu0BnYxzxKIfDAIGT5U39G2uUFgMlgN3Cw4ai42dIYSf8jAy0dSN5u6bBnuUM9jBt6VuhEI5HFM6lDHbCdUfR9fSYKIFpTnQcVUVOp3bSOsGS0kOjUsG10n6NKvEZe29SW8nHN";
                v2.ExtendedFields10 = "2tVoceMeUlJCMDmt";
                v2.ExtendedFields11 = "zAXgLX8c4yp42igT4VntFqTTQAwlOFXYsSEQ75fcQFqSoSPZz3XfDyYtMgdfAQ4NshLbRhIrySm2Pe2Or0pSj1aJsFj3LONG49945cV0DpaN2pM669i1sVfLakEVOYIbHvZbU3gr0A8uln7bXj6qCJ2VGGE7sy2ZevDgKEoBjTBfi2lJC3PlNt7ti7ApvXQ6WwLxvmOZwKA9dAq5bHHl8fhvXu78pL7avQLTntj6y0XHdc0VNcHq9j4tgJxV7emVrNFnIpLV4KLVig6J4wodzi37IdFY27do0YBc7oz6Zix745DizuVUf6LfLadQanAZAMfiibIR5lABbdvv986JYH70VdefzhHHQjlNSRGCmUPnyPU29cEKrlrKkesgoqeBiZB7iSMsGQoYLJlo9W1TphFtS7PfhC6BlvED4E3z9rSNemDIbH08ByiQlC4ohfaAtj7ukVem6jtNTCHVBrP77jBBK31GzA9FyA84v";
                v2.ExtendedFields12 = "1ff1K2AR42VuVON3SElJJLoJ3dU5srCHYTzjc407Z";
                v2.ExtendedFields13 = "H3OpB3vkQjp3aqoRJA9rTmwauI9sqPhfVS8ghZ40j25ux33QFrsl5pCe1x7M0IAA8XXyDuLzNp6ATL2083yTU1P4yFcN1OlMH5dvnfm6QEKcQXhGa7HgbeFT3BfckXFPgDrd1UB2nNZWBTy0owzuDEZNGHe";
                v2.ExtendedFields14 = "rWrelaGAvJnociYQf5xf9iRVeXcDFUmo5oWVXTXKUxPGX0WxNc4qVM2ijiluGSfm6";
                v2.ExtendedFields15 = "VxcQ8s9ubs7ffm8j7eI0JR2kUed9bLCZRyIdC6XZLld9tsK0SVdeWR7ACBD1XlJwHUj5Y0MfmVhVnPUBKiQQHM3eY2nZHpuD6j6f5TJ65p9NoVQwz5ICgXUbTKSdt0xBa1BygdNc64xc0z7gVyxzZEocycEbseq7xPV4LtUl6z53t2CcRURGryoBDcjapflC0n22dEakTl7ChbWn7";
                context.Set<BaseBarCode>().Add(v1);
                context.Set<BaseBarCode>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<BaseBarCode>().Find(v1.ID);
                var data2 = context.Set<BaseBarCode>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }

        private Guid AddBaseOrganization()
        {
            BaseOrganization v = new BaseOrganization();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.IsProduction = false;
                v.IsSale = false;
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "Q2OPJPqytGIFOScaMNJjGQDw9nIUXSMOb416zxfZKEy7I4RANBdlmzNggi7M9XTG2MNX48XGaAXuDCKmLKtRhwYgWiBes30B9UEBek206v1ijuFcEpYbXv4a5Z22X5e2zx5m06RHeNf0yzxpUfDiaMP8JYyI4z2xmR85fTlHPv7Y0XYZlmHRPD5XXtDc8mc85KJMQPKAt2b1iuIYKRiejXRE5ZWUEWF9CuwCFSKb5wkPGERfc1AXhajd58EERVq63W4oD3bUcMdgYh29lMSJCbj7WKF8oAt5NILGGr3O7qLSGfWT4oWVRlGOCnF4C275qtkj8txc0T1bphNLkv9ibu8dIK7HYFG8fS";
                v.Code = "0SeyhPLHTuUGWE30ePOtlLHwKCTMOxRlnY9LedBdU";
                v.Name = "yGwz4";
                v.SourceSystemId = "oaxO12UdytvksjP1Ny4qHr";
                v.LastUpdateTime = DateTime.Parse("2024-11-22 12:56:04");
                context.Set<BaseOrganization>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseAnalysisType()
        {
            BaseAnalysisType v = new BaseAnalysisType();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Code = "FfodWam4x35iDnNEWvTU7aZgL5biTSGce0gxnWtpcw1H";
                v.Name = "FALWtoaCZV";
                v.SourceSystemId = "R92zkgVCxyhw";
                v.LastUpdateTime = DateTime.Parse("2025-05-01 12:56:04");
                context.Set<BaseAnalysisType>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseDepartment()
        {
            BaseDepartment v = new BaseDepartment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "noYb19WY573yFQiEt5UjzYUnvZVnAQdLkbtCkFHVs4PaUxkO1zyzxsqzHhOZLxIBV2zr8Ae1flUAn7qZPkzi581LhdqCIFjXO9SCMjGYwuVZu0J0jW5wbCx025wOBCg7qRlM6dXlOzMJVciHAHDWFdwcysyjzci7BcpOnI2L4g0axIlaTes8EQbZuEo5zF4njF9HtRGe8atwgZejEVnoKS2U8mb1MQ1820MISBl9pu497VhjitVicYRP12pO79X7sstZLDr5uMUPTmiWtx1tdqKID7QBnrHFXsdEpQVPCuzYM7wN2ZuUR22NmQ18Nu7Mdfi1I6KzAkgL2K43aYz";
                v.Code = "HFmjouOguFqOQjvNkkCEMhYylMs0oydN6";
                v.Name = "wljYb33Z";
                v.SourceSystemId = "OtSn0jJ7Vr2BJb0";
                v.LastUpdateTime = DateTime.Parse("2024-10-22 12:56:04");
                context.Set<BaseDepartment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseItemCategory()
        {
            BaseItemCategory v = new BaseItemCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.DepartmentId = AddBaseDepartment();
                v.Code = "aS1YpVGKJrWXLd65ttgBi";
                v.Name = "Bn62UDVpzXzOeV1K5DXmrt7b8ZTAPx0vLGv4K9Aw43BgM";
                v.SourceSystemId = "6Zqs2aczSO9YRlIa74qTazQXMDhwLFAIXfYJYw";
                v.LastUpdateTime = DateTime.Parse("2024-07-01 12:56:04");
                context.Set<BaseItemCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseUnit()
        {
            BaseUnit v = new BaseUnit();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Memo = "ey4MjggeyNendYQlxrq6L71OeOeGhH5OezXdc8jHl4EzB09J5waG85pKfDfjXBsPhl2y6aobKkKcccPR1LHwy7i2NBtUmxj52pj0ARMncsvwHlaNyWxHHtG3BHEtgf7E1X5mT3yhGU7KgQwykOJ0P61YmUXQ2irXEuqGpAMPZQKD9KPcmAiiMWfPhuDBiD5p8SWuIIUj0pRBl3PVZfl2sKUCj7RyDkSiFolAHWuA8cH6nwRLWAeMz9SC4pzxO9fEkrW2A5zTJcU4p6McC9vPfLEtdBgI6J02fjia2gqwtSoHLl1V0kg6UVhSYCxs3Z2";
                v.Code = "jCQdDVm7R51ZxAieDfxBNgbx03QQrGzBNiUq8CDpZbO2Nie3e";
                v.Name = "tLWs45PLPoaX7hIn8n0TER9wje6Zin7EJFm8oK9LD4";
                v.SourceSystemId = "p269m2HtpgI8GSYADsr2ze58";
                v.LastUpdateTime = DateTime.Parse("2026-05-21 12:56:04");
                context.Set<BaseUnit>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseWareHouse()
        {
            BaseWareHouse v = new BaseWareHouse();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.IsProduct = null;
                v.ShipType = WMS.Model.WhShipTypeEnum.ToCustomer;
                v.IsStacking = true;
                v.IsEffective = WMS.Model.EffectiveEnum.Effective;
                v.Memo = "13V2fWJcbjoeJjtSY0kFwJA1Vsv8PZn61YnonOEA6jWKY6gwpcVvbkZYD9S5m5ZSkEJw1fI757KEfatWl1cdssJISVRW";
                v.Code = "vvayfnUn4SrnLn";
                v.Name = "d1QH6eUWTtVi9gIKNJGlMRK6X0FRzgUQw8rr";
                v.SourceSystemId = "dbB";
                v.LastUpdateTime = DateTime.Parse("2024-04-25 12:56:04");
                context.Set<BaseWareHouse>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddBaseItemMaster()
        {
            BaseItemMaster v = new BaseItemMaster();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.OrganizationId = AddBaseOrganization();
                v.ItemCategoryId = AddBaseItemCategory();
                v.SPECS = "FZxgTqDBagDmHbOGmxiUXW4ngVCcsYc1wdy02Brdp99rtBSKIMRIGJI2PCzyOWjUyCDcSCDSp3y1ueTiwT2sVMeOIcTm0kY5vUtVkTFCM2Dh8Gil2oJT8AY7kC2IUDJxbfpCuPeQgHBpSQXGoXFUKqKfMQNWGia5mO1vmPmj95EZYe02zUHitYCW3Qgz5CVZ6y1FYbWiNtgLPqXABTmQPFil7Dph";
                v.MateriaModel = "9dWgfkV8j4VQVNlLKV87AcZzIpJpgoU0yiifJAcPaetHJOKPTPxBlUp7hVKOhGzm7IFX5b0Coc8RTjJGUYsLdOFnc7qjECGQB6vBWVkrL6ZrmQUIDihv7OBU9NmRpMweAaGoJdnPFzOjbGoAkkz5NyjiumBYgiZZ7mEN3atr1vnQOkWZIBv20SD6kzwZnN03XoRQ7XQaeXFByO6Wh4jo5SVC9zCQ3HFFqliX6MWPy5uUC7OG2uw4gdKjLMIA4MpzpFePgT7o7YePXJQj2Mg4nbiPyODiEbNeDxTj2Xn9dwatKvMAnTUvVU8Cf1MsI4h9EpBzUgM2WT3Wd28nblo5pv6VmTlX4kZSRQD3Kbej";
                v.Description = "F1Ih2oL0KYRxU";
                v.StockUnitId = AddBaseUnit();
                v.ProductionOrgId = AddBaseOrganization();
                v.ProductionDeptId = AddBaseDepartment();
                v.WhId = AddBaseWareHouse();
                v.FormAttribute = WMS.Model.ItemFormAttributeEnum.PurchasePart;
                v.MateriaAttribute = "douma9BjZggKPb";
                v.GearRatio = 23;
                v.Power = 62;
                v.SafetyStockQty = 18;
                v.FixedLT = 15;
                v.BuildBatch = 39;
                v.NotAnalysisQty = 7;
                v.AnalysisTypeId = AddBaseAnalysisType();
                v.IsEffective = WMS.Model.EffectiveEnum.Ineffective;
                v.Code = "tUNjEGnSNGmGYP80Oot0VwUFB70MobFx1";
                v.Name = "fxuLFMQbpqQDSNwrd7O9UR6jMXJep0vGp6";
                v.SourceSystemId = "0oveHOttdAUtesdkWiefDoK";
                v.LastUpdateTime = DateTime.Parse("2024-11-23 12:56:04");
                context.Set<BaseItemMaster>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
