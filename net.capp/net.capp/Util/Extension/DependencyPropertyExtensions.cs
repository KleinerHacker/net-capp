using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net.capp.Util.Extension
{
    public static class DependencyPropertyExtensions
    {
        private static readonly IDictionary<DependencyProperty, IList<PropertyChangedCallback>> Dict = new Dictionary<DependencyProperty, IList<PropertyChangedCallback>>();

        public static void AddCallback(this DependencyProperty dp, System.Type type, PropertyChangedCallback callback)
        {
            if (!Dict.ContainsKey(dp))
            {
                Dict.Add(dp, new List<PropertyChangedCallback>());
                dp.AddOwner(type, new PropertyMetadata((o, args) =>
                {
                    foreach (var cb in Dict[dp])
                    {
                        cb.Invoke(o, args);
                    }
                }));
            }

            Dict[dp].Add(callback);
        }
    }
}
