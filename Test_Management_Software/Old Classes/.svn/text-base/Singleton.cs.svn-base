using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Management_Software.Classes
{
    //for use with the component factory, made singleton for efficiency and proformance
    //source from http://www.codeproject.com/KB/architecture/CSharpClassFactory.aspx
    public class Singleton
    {
        // Static, VOLATILE variable to store single instance
        private static volatile Singleton m_instance;

        // Static synchronization root object, for locking
        private static object m_syncRoot = new object();

        // Property to retrieve the only instance of the Singleton
        public static Singleton Instance
        {
            get
            {
                // Check that the instance is null
                if (m_instance == null)
                {
                    // Lock the object
                    lock (m_syncRoot)
                    {
                        // Check to make sure its null
                        if (m_instance == null)
                        {
                            m_instance = new Singleton();
                        }
                    }
                }

                // Return the non-null instance of Singleton
                return m_instance;
            }
        }
    }      
}
