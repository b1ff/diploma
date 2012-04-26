using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;

namespace Gamification.Testing.Utils
{
    public abstract class BaseAutoMoqFixture
    {
        private IDictionary<Type, Mock> _registeredMocks;

        [SetUp]
        public void SetUp()
        {
            _registeredMocks = new Dictionary<Type, Mock>();
            OnSetUp();
        }

        [TearDown]
        public void TearDown()
        {
            _registeredMocks = null;
            OnTearDown();
        }

        protected virtual void OnSetUp() { }
        protected virtual void OnTearDown() { }

        public virtual T CreateInctanceWithAutoMockedDependencies<T>() where T : class
        {
            var constructor = typeof(T).GetConstructors()
                .OrderByDescending(x => x.GetParameters().Count()).First();
            var parameters = constructor.GetParameters().Where(x => x.ParameterType.IsAbstract);
            var @paramsForConstructor = new List<object>();
            foreach (var parameterInfo in parameters)
            {
                Mock mock;
                if (_registeredMocks.ContainsKey(parameterInfo.ParameterType))
                    mock = _registeredMocks[parameterInfo.ParameterType];
                else
                    mock = RegisterNewMock(parameterInfo.ParameterType);

                @paramsForConstructor.Add(mock.Object);
                SetMock(parameterInfo.ParameterType, mock);
            }

            var result = (T)constructor.Invoke(@paramsForConstructor.ToArray());
            return result;
        }

        private Mock RegisterNewMock(Type componentType)
        {
            var mockForComponentType =
                typeof(Mock<>).MakeGenericType(componentType);
            var mock = (Mock)Activator.CreateInstance(mockForComponentType, MockBehavior.Loose);
            SetMock(componentType, mock);
            return mock;
        }

        private void SetMock(Type type, Mock mock)
        {
            if (_registeredMocks.ContainsKey(type) == false)
                _registeredMocks.Add(type, mock);

            if (_registeredMocks[type] == null)
                _registeredMocks[type] = mock;
        }

        protected Mock<T> GetMock<T>() where T : class
        {
            return (Mock<T>)_registeredMocks[typeof(T)];
        }

        public ISetup<T> Setup<T>(Expression<Action<T>> expression) where T : class
        {
            return GetMock<T>().Setup(expression);
        }

        public ISetup<T, TResult> Setup<T, TResult>(Expression<Func<T, TResult>> expression) where T : class
        {
            return GetMock<T>().Setup(expression);
        }

        public void Verify<T>(Expression<Action<T>> expression) where T : class
        {
            GetMock<T>().Verify(expression);
        }

        public void Verify<T>(Expression<Action<T>> expression, string failMessage) where T : class
        {
            GetMock<T>().Verify(expression, failMessage);
        }

        public void Verify<T>(Expression<Action<T>> expression, Times times) where T : class
        {
            GetMock<T>().Verify(expression, times);
        }

        public void Verify<T>(Expression<Action<T>> expression, Times times, string failMessage) where T : class
        {
            GetMock<T>().Verify(expression, times, failMessage);
        }
    }
}
