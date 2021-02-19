using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumDemo
{
    public class EnumHelper<TEnum,REntity>
        where TEnum:struct
        where REntity:BaseAttributes
    {
        static IList<REntity> RentityList = new List<REntity>();
        static EnumHelper() {
            if (typeof(TEnum).IsEnum)
                return;

            Type type = typeof(TEnum);
            var arrayList = Enum.GetValues(type);
            foreach (var item in arrayList) {
                foreach (var obj in item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(REntity), true)) {
                    var attribute = obj as REntity;
                    attribute.EnumName = item.ToString();
                    attribute.EnumValue = (int)Enum.Parse(type, item.ToString());
                    RentityList.Add(attribute);
                }
            }
        }

        public static bool Any(Func<REntity, bool> func) => RentityList.Any(func);
    }
}
