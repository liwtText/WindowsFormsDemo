using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Security.Principal;
using System.Reflection;


namespace WindowsFormsApp8
{
    public class Class1
    {
        public static void test()
        {
            Console.WriteLine("***\r\n Begin program - no logging\r\n");
            //IRepository<Customer> customerRepository =
            //  new Repository<Customer>();
            //IRepository<Customer> customerRepository =
            //  new LoggerRepository<Customer>(new Repository<Customer>());
            Thread.CurrentPrincipal =
            new GenericPrincipal(new GenericIdentity("Administrator"),
            new[] { "ADMIN" });
            IRepository<Customer> customerRepository =
            RepositoryFactory.Create<Customer>();
            var customer = new Customer
            {
                Id = 1,
                Name = "Customer 1",
                Address = "Address 1"
            };
            customerRepository.Add(customer);
            customerRepository.Update(customer);
            customerRepository.Delete(customer);
            Console.WriteLine("\r\nEnd program - no logging\r\n***");
            Console.ReadLine();
        }
    }
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
    public class Repository<T> : IRepository<T>
    {
        public void Add(T entity)
        {
            Console.WriteLine("Adding {0}", entity);
        }
        public void Delete(T entity)
        {
            Console.WriteLine("Deleting {0}", entity);
        }
        public void Update(T entity)
        {
            Console.WriteLine("Updating {0}", entity);
        }
        public IEnumerable<T> GetAll()
        {
            Console.WriteLine("Getting entities");
            return null;
        }
        public T GetById(int id)
        {
            Console.WriteLine("Getting entity {0}", id);
            return default(T);
        }
    }
    public class LoggerRepository<T> : IRepository<T>
    {
        private readonly IRepository<T> _decorated;
        public LoggerRepository(IRepository<T> decorated)
        {
            _decorated = decorated;
        }
        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
        public void Add(T entity)
        {
            Log("In decorator - Before Adding {0}", entity);
            _decorated.Add(entity);
            Log("In decorator - After Adding {0}", entity);
        }
        public void Delete(T entity)
        {
            Log("In decorator - Before Deleting {0}", entity);
            _decorated.Delete(entity);
            Log("In decorator - After Deleting {0}", entity);
        }
        public void Update(T entity)
        {
            Log("In decorator - Before Updating {0}", entity);
            _decorated.Update(entity);
            Log("In decorator - After Updating {0}", entity);
        }
        public IEnumerable<T> GetAll()
        {
            Log("In decorator - Before Getting Entities");
            var result = _decorated.GetAll();
            Log("In decorator - After Getting Entities");
            return result;
        }
        public T GetById(int id)
        {
            Log("In decorator - Before Getting Entity {0}", id);
            var result = _decorated.GetById(id);
            Log("In decorator - After Getting Entity {0}", id);
            return result;
        }
    }

    public class DynamicProxy<T> : RealProxy
    {
        private readonly T _decorated;
        private Predicate<MethodInfo> _filter;
        public event EventHandler<IMethodCallMessage> BeforeExecute;
        public DynamicProxy(T decorated)
          : base(typeof(T))
        {
            _decorated = decorated;
            Filter = m => true;
        }
        public Predicate<MethodInfo> Filter
        {
            get { return _filter; }
            set
            {
                if (value == null)
                    _filter = m => true;
                else
                    _filter = value;
            }
        }
        private void OnBeforeExecute(IMethodCallMessage methodCall)
        {
            if (BeforeExecute != null)
            {
                var methodInfo = methodCall.MethodBase as MethodInfo;
                if (_filter(methodInfo))
                    BeforeExecute(this, methodCall);
            }
        }
        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as System.Reflection.MethodInfo;
            if (_filter(methodInfo))
                Log("In Dynamic Proxy - Before executing '{0}'",
              methodCall.MethodName);
            try
            {
                var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
                if (_filter(methodInfo))
                    Log("In Dynamic Proxy - After executing '{0}' ",
                  methodCall.MethodName);
                return new ReturnMessage(result, null, 0,
                  methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                if (_filter(methodInfo))
                    Log(string.Format(
                  "In Dynamic Proxy- Exception {0} executing '{1}'", e),
                  methodCall.MethodName);
                return new ReturnMessage(e, methodCall);
            }
        }
    }

    class AuthenticationProxy<T> : RealProxy
    {
        private readonly T _decorated;
        public AuthenticationProxy(T decorated)
          : base(typeof(T))
        {
            _decorated = decorated;
        }
        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as System.Reflection.MethodInfo;
            if (System.Threading.Thread.CurrentPrincipal.IsInRole("ADMIN"))
            {
                try
                {
                    Log("User authenticated - You can execute '{0}' ",
                      methodCall.MethodName);
                    var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
                    return new ReturnMessage(result, null, 0,
                      methodCall.LogicalCallContext, methodCall);
                }
                catch (Exception e)
                {
                    Log(string.Format(
                      "User authenticated - Exception {0} executing '{1}'", e),
                      methodCall.MethodName);
                    return new ReturnMessage(e, methodCall);
                }
            }
            Log("User not authenticated - You can't execute '{0}' ",
              methodCall.MethodName);
            return new ReturnMessage(null, null, 0,
              methodCall.LogicalCallContext, methodCall);
        }
    }
    public class RepositoryFactory
    {
        public static IRepository<T> Create<T>()
        {
            //var repository = new Repository<T>();
            //var dynamicProxy = new DynamicProxy<IRepository<T>>(repository);
            //return dynamicProxy.GetTransparentProxy() as IRepository<T>;

            //var repository = new Repository<T>();
            //var decoratedRepository =
            //  (IRepository<T>)new DynamicProxy<IRepository<T>>(
            //  repository).GetTransparentProxy();
            //// Create a dynamic proxy for the class already decorated
            //decoratedRepository =
            //  (IRepository<T>)new AuthenticationProxy<IRepository<T>>(
            //  decoratedRepository).GetTransparentProxy();
            //return decoratedRepository;

            var repository = new Repository<T>();
            var dynamicProxy = new DynamicProxy<IRepository<T>>(repository)
            {
                Filter = m => !m.Name.StartsWith("Get")
            };
            return dynamicProxy.GetTransparentProxy() as IRepository<T>;
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
