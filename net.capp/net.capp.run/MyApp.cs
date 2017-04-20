using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.capp.run
{
    public class MyApp : Application
    {
        public static void Main(string[] args)
        {
            new MyApp().Startup(args);
        }
    }
}
