using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Threading;

namespace RubricOn.Models
{
    /// <summary>
    /// This class provides several static methods for loading DataContext objects 
    /// in a variety of ways. You can load the data context as normal one new instance
    /// at a time, or you can choose to use one of the scoped factory methods that
    /// can scope the DataContext to a WebRequest or a Thread context (in a WinForm app
    /// for example).
    /// 
    /// Using scoped variants can be more efficient in some scenarios and allows passing
    /// a DataContext across multiple otherwise unrelated components so that the change
    /// context can be shared. 
    /// </summary>
    public class DataContextFactory
    {
        public static bool SubmitChanges(bool ThrowException)
        {
            try
            {
                SaveWebRequestScopedDataContext();
                return true;
            }
            catch (Exception ex)
            {
                SaveError(ex);
                if (ThrowException)
                    throw ex;
                return false;
            }
        }

        private static void SaveError(Exception ex)
        {
    
            var key = "__WRSCERR_" + HttpContext.Current.GetHashCode().ToString("x") + Thread.CurrentContext.ContextID.ToString();
            HttpContext.Current.Items[key] = ex;
        }

        public static Exception GetLastError()
        {

            var key = "__WRSCERR_" + HttpContext.Current.GetHashCode().ToString("x") + Thread.CurrentContext.ContextID.ToString();

            return (Exception)HttpContext.Current.Items[key];
        }


        private static void SaveWebRequestScopedDataContext()
        {
            var key = "__WRSCDC_" + HttpContext.Current.GetHashCode().ToString("x") + Thread.CurrentContext.ContextID.ToString();

            object[] myArray = new object[HttpContext.Current.Items.Keys.Count];
            HttpContext.Current.Items.Keys.CopyTo(myArray, 0);

            foreach (var Key in myArray)
            {
                if (Key.ToString().StartsWith(key))
                {
                    ((DataContext)HttpContext.Current.Items[Key]).SubmitChanges();
                }
            }
        }

        /// <summary>
        /// Creates a new Data Context for a specific DataContext type
        /// 
        /// Provided merely for compleness sake here - same as new YourDataContext()
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <returns></returns>
        public static TDataContext GetDataContext<TDataContext>()
                where TDataContext : DataContext, new()
        {
            return (TDataContext)Activator.CreateInstance<TDataContext>();
        }

        /// <summary>
        /// Creates a new Data Context for a specific DataContext type with a connection string
        /// 
        /// Provided merely for compleness sake here - same as new YourDataContext()
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static TDataContext GetDataContext<TDataContext>(string connectionString)
                where TDataContext : DataContext, new()
        {
            Type t = typeof(TDataContext);
            return (TDataContext)Activator.CreateInstance(t, connectionString);
        }


        /// <summary>
        /// Creates a ASP.NET Context scoped instance of a DataContext. This static
        /// method creates a single instance and reuses it whenever this method is
        /// called.
        /// 
        /// This version creates an internal request specific key shared key that is
        /// shared by each caller of this method from the current Web request.
        /// </summary>
        public static TDataContext GetWebRequestScopedDataContext<TDataContext>()
                where TDataContext : DataContext, new()
        {
            // *** Create a request specific unique key 
            return (TDataContext)GetWebRequestScopedDataContextInternal(typeof(TDataContext), null, null);
        }

        /// <summary>
        /// Creates a ASP.NET Context scoped instance of a DataContext. This static
        /// method creates a single instance and reuses it whenever this method is
        /// called.
        /// 
        /// This version lets you specify a specific key so you can create multiple 'shared'
        /// DataContexts explicitly.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetWebRequestScopedDataContext<TDataContext>(string key)
                                   where TDataContext : DataContext, new()
        {
            return (TDataContext)GetWebRequestScopedDataContextInternal(typeof(TDataContext), key, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetWebRequestScopedDataContext<TDataContext>(string key, string connectionString)
                                   where TDataContext : DataContext, new()
        {
            return (TDataContext)GetWebRequestScopedDataContextInternal(typeof(TDataContext), key, connectionString);
        }

        /// <summary>
        /// Internal method that handles creating a context that is scoped to the HttpContext Items collection
        /// by creating and holding the DataContext there.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        static object GetWebRequestScopedDataContextInternal(Type type, string key, string connectionString)
        {
            object context;

            if (HttpContext.Current == null)
            {
                if (connectionString == null)
                    context = Activator.CreateInstance(type);
                else
                    context = Activator.CreateInstance(type, connectionString);

                return context;
            }

            // *** Create a unique Key for the Web Request/Context 
            if (key == null)
                key = "__WRSCDC_" + HttpContext.Current.GetHashCode().ToString("x") + Thread.CurrentContext.ContextID.ToString() + "-" + type.Name;

            context = HttpContext.Current.Items[key];
            if (context == null)
            {
                if (connectionString == null)
                    context = Activator.CreateInstance(type);
                else
                    context = Activator.CreateInstance(type, connectionString);

                if (context != null)
                    HttpContext.Current.Items[key] = context;
            }

            return context;
        }


        /// <summary>
        /// Creates a Thread Scoped DataContext object that can be reused.
        /// The DataContext is stored in Thread local storage.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetThreadScopedDataContext<TDataContext>()
                                   where TDataContext : DataContext, new()
        {
            return (TDataContext)GetThreadScopedDataContextInternal(typeof(TDataContext), null, null);
        }


        /// <summary>
        /// Creates a Thread Scoped DataContext object that can be reused.
        /// The DataContext is stored in Thread local storage.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetThreadScopedDataContext<TDataContext>(string key)
                                   where TDataContext : DataContext, new()
        {
            return (TDataContext)GetThreadScopedDataContextInternal(typeof(TDataContext), key, null);
        }


        /// <summary>
        /// Creates a Thread Scoped DataContext object that can be reused.
        /// The DataContext is stored in Thread local storage.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        static object GetThreadScopedDataContextInternal(Type type, string key, string ConnectionString)
        {
            if (key == null)
                key = "__WRSCDC_" + Thread.CurrentContext.ContextID.ToString();

            LocalDataStoreSlot threadData = Thread.GetNamedDataSlot(key);
            object context = null;
            if (threadData != null)
                context = Thread.GetData(threadData);

            if (context == null)
            {
                if (ConnectionString == null)
                    context = Activator.CreateInstance(type);
                else
                    context = Activator.CreateInstance(type, ConnectionString);

                if (context != null)
                {
                    if (threadData == null)
                        threadData = Thread.AllocateNamedDataSlot(key);

                    Thread.SetData(threadData, context);
                }
            }

            return context;
        }


        /// <summary>
        /// Returns either Web Request scoped DataContext or a Thread scoped
        /// request object if not running a Web request (ie. HttpContext.Current)
        /// is not available.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public static TDataContext GetScopedDataContext<TDataContext>(string key, string connectionString)
        {
            if (HttpContext.Current != null)
                return (TDataContext)GetWebRequestScopedDataContextInternal(typeof(TDataContext), key, connectionString);

            return (TDataContext)GetThreadScopedDataContextInternal(typeof(TDataContext), key, connectionString);
        }

        /// <summary>
        /// Returns either Web Request scoped DataContext or a Thread scoped
        /// request object if not running a Web request (ie. HttpContext.Current)
        /// is not available.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetScopedDataContext<TDataContext>(string key)
        {
            if (HttpContext.Current != null)
                return (TDataContext)GetWebRequestScopedDataContextInternal(typeof(TDataContext), key, null);

            return (TDataContext)GetThreadScopedDataContextInternal(typeof(TDataContext), key, null);
        }

        /// <summary>
        /// Returns either Web Request scoped DataContext or a Thread scoped
        /// request object if not running a Web request (ie. HttpContext.Current)
        /// is not available.
        /// </summary>
        /// <typeparam name="TDataContext"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TDataContext GetScopedDataContext<TDataContext>()
        {
            if (HttpContext.Current != null)
                return (TDataContext)GetWebRequestScopedDataContextInternal(typeof(TDataContext), null, null);

            return (TDataContext)GetThreadScopedDataContextInternal(typeof(TDataContext), null, null);
        }

    }
}
